-- ###Views
--Những PM và Team Leader chưa được phân công
CREATE OR ALTER VIEW vw_khongla_pm
AS
SELECT *
FROM NHANVIEN NV
WHERE NOT EXISTS(
	SELECT *
	FROM DUAN AS pm
	WHERE pm.MaPM = NV.MaNV
) AND NOT EXISTS(
	SELECT *
	FROM TRUONGNHOM AS tn
	WHERE tn.MaNV = NV.MaNV
) AND NV.ChucVu != 'CEO'
GO

CREATE OR ALTER VIEW vw_khongla_truongnhom
AS
SELECT *
FROM NHANVIEN NV
WHERE NOT EXISTS(
	SELECT *
	FROM TRUONGNHOM tl
	WHERE tl.MaNV = NV.MaNV OR NV.ChucVu IN('CEO', 'PM')
)
GO

--Xem danh sách ngày nghỉ
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
		dd.*, n.TenNhom, n.SoGioMotNg, gd.MaDA, gd.MaGiaiDoan, gd.NgayBD, gd.NgayKT
FROM DIEMDANH dd
JOIN NHOM n ON n.MaNV = dd.MaNV
JOIN GIAIDOAN gd ON gd.MaDA = n.MaDA
GO

--###Constraints CHECK
-- câu 1: check tiến độ công việc và tiến độ dự án
ALTER TABLE CONGVIEC ADD CONSTRAINT CHECK_TIENDOCV CHECK (TienDo<=100 and TienDo>=0)
ALTER TABLE DUAN ADD CONSTRAINT CHECK_TIENDODA CHECK (TienDo <=100 and TienDo>=0)

--câu 2 :check Tên nhân viên và levels không chứa ký tự đặc biệt và số; SDT không chứa ký tự chữ cái

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_TENNV CHECK(Ten NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT  CHECK_LEVELS CHECK(levels NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_SDT CHECK(SDT not LIKE '[a-zA-Z_!@#$%^&*()<>?/|}{~:]%]');
--câu 3 :Mã nhân viên viết theo công thức: 2 ký tự đầu là “NV” + 3 ký tự số nguyên dương

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_MANV CHECK (MANV LIKE 'NV%' AND CAST(SUBSTRING(MANV, 3, 3) AS INT) > 0 AND CAST(SUBSTRING(MANV, 3, 3) AS INT) <= 999);

-- câu 4 :Trong UOCLUONG, Time Sprint >= Time Tasks

Alter Table UocLuong add constraint CHECK_TIMESP_TIMETASK CHECK(TimeSprint >=TimeTasks)
go


--###Triggers

--???????????
--1.Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án
create or alter trigger tr_addUocLuong on NHOM
AFTER INSERT AS
DECLARE @manv VARCHAR(10), @magd VARCHAR(10), @mada INT
SELECT @manv=i.MaNV, @mada=i.MaDA
FROM inserted i 
BEGIN
	if not exists(select * from UOCLUONG ul 
		where ul.MaNV = @manv AND ul.MaDA = @mada AND ul.MaGiaiDoan = (SELECT TOP 1 MaGiaiDoan FROM GIAIDOAN WHERE GIAIDOAN.MaDA = @mada ORDER BY MaGiaiDoan DESC))
		--Nếu nhân viên ko tồn tại trong giai đoạn mới nhất (đang làm việc) tại dự án đó thì tạo mới 1 hàng UOCLUONG
		insert into UOCLUONG
		select i.MaNV, i.MaDA, GIAIDOAN.MaGiaiDoan, NULL, NULL, NULL 
		from inserted AS i
			join GIAIDOAN on i.MaDA= GIAIDOAN.MaDA
		where GIAIDOAN.MaGiaiDoan = (SELECT TOP 1 MaGiaiDoan FROM GIAIDOAN WHERE GIAIDOAN.MaDA = i.MaDA ORDER BY MaGiaiDoan DESC)
END;
GO

--2.Kiểm tra dự án đang ở trạng thái “trì hoãn”, “hoàn thành” hay không, nếu có thì được xóa (delete) và ngược lại
CREATE OR ALTER TRIGGER tr_DeleteDuAn
ON DUAN
AFTER DELETE
AS
BEGIN
    IF EXISTS (SELECT * FROM deleted WHERE deleted.TrangThai NOT in ('Done', 'Delay'))
    BEGIN
        RAISERROR('Không thể xóa dự án',16,2)
        ROLLBACK TRAN;
    END
END;
GO

--5 Xóa NhiemVu trước khi xóa CongViec
CREATE OR ALTER TRIGGER deleteCongViec on CONGVIEC
AFTER DELETE AS
DECLARE @macv INT
BEGIN
	--DELETE FROM NHIEMVU WHERE MaCV IS NULL AND (SELECT )
	SELECT * FROM NHIEMVU NV
	JOIN NHIEMVU TQ ON TQ.MaTienQuyet = NV.MaNhiemVu
END
GO

--6 Kiểm tra thứ tự nhiệm vụ tiên quyết, nếu chưa hoàn thành nhiệm vụ tiên quyết trong cùng 1 công việc trước đó thì không được làm nhiệm vụ hiện tại
CREATE OR ALTER TRIGGER tr_kiemtra_tienquyet ON NHIEMVU
AFTER UPDATE
AS
DECLARE @newNV varchar(10), @trangthaiTQ varchar(30)
SELECT @newNV=n.MaNhiemVu
FROM inserted n, deleted o, NHIEMVU NV
WHERE NV.MaNhiemVu = n.MaNhiemVu AND n.MaNhiemVu = o.MaNhiemVu
	--Lấy trạng thái nhiệm vụ tiên quyết
SELECT @trangthaiTQ=NVTQ.TrangThai
FROM (SELECT * FROM NHIEMVU WHERE MaNhiemVu = @newNV) NV
JOIN NHIEMVU NVTQ ON NV.MaTienQuyet = NVTQ.MaNhiemVu
IF(@trangthaiTQ != 'Done')
BEGIN
	--Nếu kiểm tra nvtq chưa Done thì trả về giá trị cũ
	RAISERROR('Nhiệm vụ tiên quyết chưa hoàn thành',16,1)
	ROLLBACK TRAN
END
GO

--9) Time Task > Time Sprint thì hủy phân công
CREATE OR ALTER TRIGGER tr_sosanh_thoigian ON UOCLUONG
FOR UPDATE
AS
DECLARE @timetask INT, @timesprint INT
SELECT @timetask=new.TimeTasks, @timetask=new.TimeSprint
FROM inserted new, UOCLUONG ul
WHERE new.MaNV = ul.MaNV AND new.MaDA = ul.MaDA AND new.MaGiaiDoan = ul.MaGiaiDoan
IF (@timetask > @timesprint)
BEGIN 
	RAISERROR('Lỗi Time Task > Time Sprint', 16, 1)
	ROLLBACK TRAN;
END
GO

--10) Xử lý ràng buộc trước khi xóa DUAN
CREATE OR ALTER TRIGGER tr_rangbuoc_xoaDA ON DUAN
INSTEAD OF DELETE
AS
DECLARE @mada INT
SELECT @mada=old.MaDA
FROM deleted old
JOIN DUAN ON DUAN.MaDA = old.MaDA
--IF (@mada IS NOT NULL)
BEGIN
	--Xóa TEAM, CAP, UOCLUONG và TEAMLEADER có cùn MaDA trước
	DELETE FROM NHOM WHERE MaDA = @mada
	DELETE FROM TRUONGNHOM WHERE MaDA = @mada
	DELETE FROM CAP WHERE MaDA = @mada
	DELETE FROM UOCLUONG WHERE MaDA = @mada
	--Xóa DUAN
	DELETE FROM DUAN WHERE MaDA = @mada
END
GO

--12)Thiết lập lại thời gian timesprint khi có nhân viên xin nghỉ
CREATE OR ALTER TRIGGER tr_update_timesprint
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;
	DECLARE @MaGiaiDoan VARCHAR(15);
	DECLARE @CapPerDay INT;
	DECLARE @MaDA INT;

	--Lấy ngày nghỉ, mã nhân viên
	SELECT @NgayNghi = DIEMDANH.Ngay, @MaNV = MaNV
	FROM DIEMDANH;

	--Lấy mã sprint và mã DA có ngày nghỉ thuộc sprint
	SELECT @MaGiaiDoan = GIAIDOAN.MaGiaiDoan, @MaDA = GIAIDOAN.MaDA
	FROM GIAIDOAN
	WHERE @NgayNghi <= GIAIDOAN.NgayKT AND @NgayNghi >= GIAIDOAN.NgayBD;

	--Lấy CapPerDay theo mã NV
	SELECT @CapPerDay = NHOM.SoGioMotNg
	FROM NHOM
	WHERE @MaNV = NHOM.MaNV AND @MaDA = NHOM.MaDA;

	IF @MaGiaiDoan IS NOT NULL
	BEGIN
		UPDATE UOCLUONG
		SET TimeSprint = TimeSprint - @CapPerDay
		WHERE @MaNV = UOCLUONG.MaNV AND @MaGiaiDoan = UOCLUONG.MaGiaiDoan AND @MaDA = UOCLUONG.MaDA;
	END
END;
GO

--13.Thiết lập lại thời gian Time Tasks khi có nhiệm vụ được hoàn thành xong
CREATE OR ALTER TRIGGER tr_update_timetasks ON NHIEMVU
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo biến
    DECLARE @ThoiGianUocTinh INT
	DECLARE @MANHANVIEN VARCHAR(10)
	DECLARE @MASPRINT VARCHAR(10)
	DECLARE @MADA VARCHAR(10)
    -- tìm thời gian hoàn thành  nhiệm vụ Của  NHÂN VIÊN mới thêm hoặc mới cập nhật
	SELECT @MANHANVIEN=NHANVIEN.MaNV,@MASPRINT=CONGVIEC.MaGiaiDoan, @MADA=CONGVIEC.MaDA, @ThoiGianUocTinh=inserted.ThoiGianUocTinh 
	FROM  inserted, NHANVIEN, CONGVIEC
	WHERE inserted.MaNV=NHANVIEN.MaNV AND CONGVIEC.MaCV=inserted.MaCV AND inserted.TrangThai='done'
	--Cập nhật timetasks
    UPDATE UOCLUONG
    SET TimeTasks =  TimeTasks- @ThoiGianUocTinh
    WHERE MaNV = @MaNhanVien AND MaDA=@MADA AND MaGiaiDoan=@MASPRINT  
END
GO

--14.Trigger kiểm tra nếu nhân viên nghỉ đúng thời gian Sprint nào thì cộng SoNgayNghi Sprint của nhân viên đó lên 1
--NOTE
CREATE OR ALTER TRIGGER tr_ktr_ngaynghi_giaidoan
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;

	SELECT @NgayNghi = DIEMDANH.Ngay, @MaNV = MaNV
	FROM DIEMDANH;
	BEGIN
		UPDATE UOCLUONG
		SET SoNgayNghi = SoNgayNghi + 1
		WHERE @MaNV = UOCLUONG.MaNV AND UOCLUONG.MaGiaiDoan IN (
			SELECT MaGiaiDoan
			FROM GIAIDOAN
			WHERE @NgayNghi <= GIAIDOAN.NgayKT AND @NgayNghi >= GIAIDOAN.NgayBD
		)
	END
END;
GO

--16. Tạo uocluong mới cho từng nhanvien trong duan theo giaidoan mới tạo
CREATE OR ALTER TRIGGER tr_themUocLuong ON GIAIDOAN
AFTER INSERT
AS
DECLARE @manv VARCHAR(10), @magd VARCHAR(10), @mada INT
SELECT @mada=i.MaDA, @magd=i.MaGiaiDoan
FROM inserted i 
BEGIN
	DECLARE cursor_nhomDA CURSOR
	FOR SELECT DISTINCT MaNV FROM NHOM WHERE MaDA=@mada
	
	OPEN cursor_nhomDA
	FETCH NEXT FROM cursor_nhomDA INTO @manv
	WHILE @@FETCH_STATUS = 0
	BEGIN
		insert into UOCLUONG VALUES(@manv, @mada, @magd, 0, 0, 0)
		FETCH NEXT FROM cursor_nhomDA INTO @manv
	END
	CLOSE cursor_nhomDA;
END
GO

--17. Xóa trưởng nhóm trong NHOM và TRUONGNHOM
CREATE OR ALTER TRIGGER tr_xoaTruongNhom ON TRUONGNHOM
INSTEAD OF DELETE
AS
DECLARE @mada INT, @tennhom VARCHAR(20), @countTVNhom INT, @matn VARCHAR(10)
SELECT @mada=d.MaDA, @tennhom=d.TenNhom, @matn=d.MaNV
FROM deleted d
BEGIN
	DELETE FROM NHOM WHERE MaDA=@mada AND TenNhom=@tennhom AND MaNV=@matn
	--Lấy số lượng thành viên của nhóm trong dự án
	SELECT @countTVNhom=COUNT(*) FROM NHOM
	WHERE TenNhom=@tennhom AND MaDA=@mada
	PRINT @countTVNHOM
	--Nếu nhóm ko còn thành viên thì được xóa trưởng nhóm
	IF  @countTVNhom = 0
	BEGIN
		DELETE FROM TRUONGNHOM WHERE MaDA=@mada AND TenNhom=@tennhom
	END
	ELSE
		RAISERROR('Nhóm này còn thành viên nên không được xóa trưởng nhóm', 16, 1)
END
GO