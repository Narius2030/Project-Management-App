	--###Views
--Xem danh sách nhân viên là trưởng nhóm trong các dự án
CREATE OR ALTER VIEW vw_danhsach_truongnhom
AS
SELECT 
	TN.*, CONCAT(NV.HovaTenDem, ' ', NV.Ten) HoTen, NV.ChucVu, NV.Levels, N.SoGioMotNg
FROM TRUONGNHOM TN
INNER JOIN NHOM N ON N.MaNV = TN.MaNV AND N.TenNhom=TN.TenNhom and N.MaDA=TN.MaDA
INNER JOIN NHANVIEN NV ON NV.MaNV = TN.MaNV
GO

--Xem danh sách số lượng công việc đã chưa hoàn thành
CREATE OR ALTER VIEW vw_congviec_chuahoanthanh
AS
SELECT DA.MaDA, GD.MaGiaiDoan, COUNT(CV.MaCV) AS [số lượng công việc]
FROM CongViec CV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
WHERE CV.TrangThai != 'Done'
GROUP BY DA.MaDA,GD.MaGiaiDoan
GO

--Xem danh sách nhiệm vụ trong một giai đoạn dự án &
CREATE OR ALTER VIEW vw_nhiemvu_giaidoan_duan
AS
SELECT nvu.MaNV, nvu.MaNhiemVu, cv.MaCV, gd.MaDA, gd.MaGiaiDoan, nvu.ThoiGianUocTinh, nvu.TrangThai
FROM NHIEMVU nvu
join CONGVIEC cv on cv.MaCV=nvu.MaCV
join GIAIDOAN gd on gd.magiaidoan=cv.MaGiaiDoan
GO

--Xem danh sách ngày nghỉ*
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
		dd.*, n.TenNhom, n.SoGioMotNg, gd.MaDA, gd.MaGiaiDoan, gd.NgayBD, gd.NgayKT
FROM DIEMDANH dd
JOIN NHOM n ON n.MaNV = dd.MaNV
JOIN GIAIDOAN gd ON gd.MaDA = n.MaDA
GO

--View liên quan đến nhiệm vụ của nhóm*
Create Or ALter View v_DanhSachNhiemVuNhom as 
SELECT 
	NV.MaNV, CV.MaDA,GD.MaGiaiDoan,CV.MaCV,N.TenNhom,NV.MaNhiemVu , TenNhiemVu , NV.TrangThai , MaTienQuyet, NV.ThoiGianUocTinh, NV.ThoiGianLamThucTe 
FROM NHIEMVU NV
INNER JOIN NHOM N ON N.MaNV = NV.MaNV
INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
GO

--###Constraints CHECK
-- câu 1: check tiến độ công việc và tiến độ dự án
ALTER TABLE CONGVIEC ADD CONSTRAINT CHECK_TIENDOCV CHECK (TienDo<=100 and TienDo>=0)
ALTER TABLE DUAN ADD CONSTRAINT CHECK_TIENDODA CHECK (TienDo <=100 and TienDo>=0)

--câu 2: check Tên nhân viên và levels không chứa ký tự đặc biệt và số; SDT không chứa ký tự chữ cái

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_TENNV CHECK(Ten NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT  CHECK_LEVELS CHECK(levels NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_SDT CHECK(SDT not LIKE '[a-zA-Z_!@#$%^&*()<>?/|}{~:]%]');
--câu 3: Mã nhân viên viết theo công thức: 2 ký tự đầu là “NV” + 3 ký tự số nguyên dương

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_MANV CHECK (MANV LIKE 'NV%' AND CAST(SUBSTRING(MANV, 3, 3) AS INT) > 0 AND CAST(SUBSTRING(MANV, 3, 3) AS INT) <= 999);
go


--###Triggers

--1.Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án*
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

--2.Kiểm tra dự án đang ở trạng thái “trì hoãn”, “hoàn thành” hay không, nếu có thì được xóa (delete) và ngược lại*
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

--3 Kiểm tra thứ tự nhiệm vụ tiên quyết, nếu chưa hoàn thành nhiệm vụ tiên quyết trong cùng 1 công việc trước đó thì không được làm nhiệm vụ hiện tại*
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

--4/ Time Task > Time Sprint thì hủy phân công *
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

--5.Trigger kiểm tra nếu nhân viên nghỉ đúng thời gian Sprint nào thì cộng SoNgayNghi Sprint của nhân viên đó lên 1*
--NOTE
CREATE OR ALTER TRIGGER tr_ktr_ngaynghi_giaidoan
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;

	SELECT @NgayNghi = i.Ngay, @MaNV = i.MaNV
	FROM inserted i;
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

--6. Tạo uocluong mới cho từng nhanvien trong duan theo giaidoan mới tạo*
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
	DEALLOCATE cursor_nhomDA
END
GO

--7. Xóa trưởng nhóm trong NHOM và TRUONGNHOM*
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