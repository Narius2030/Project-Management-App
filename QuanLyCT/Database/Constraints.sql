-- ###Views

-- 1.Xem danh sách nhân viên và nhóm
--a)Tất cả
CREATE OR ALTER VIEW vw_nhanvien_trong_duan
AS
SELECT 
	NV.MaNV, CONCAT(HovaTenDem,' ',Ten) AS HoTen, ChucVu, Levels,
	TM.TenNhom, TM.MaDA, TM.CapPerDay
FROM NHANVIEN NV
JOIN NHOM TM ON TM.MaNV = NV.MaNV
GO

--b)Trưởng nhóm
CREATE OR ALTER VIEW vw_truongnhom_trong_duan
AS
SELECT
	NV.MaNV, CONCAT(HovaTenDem,' ',Ten) AS HoTen, ChucVu, Levels,
	TLD.TenNhom, TLD.MaDA
FROM TRUONGNHOM TLD
JOIN NHANVIEN NV ON NV.MaNV = TLD.MaNV
GO

--c) Những PM và Team Leader chưa được phân công
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

--2.Xem nội dung nhiệm vụ thuộc 1 công việc 
--a)Tất cả công việc
CREATE OR ALTER VIEW vw_congviec_nhiemvu
AS
SELECT 
	MaNhiemVu, NHV.TrangThai AS TTNhiemvu, TenNhiemVu, ThoiGianLamThucTe, ThoiGianUocTinh, MaTienQuyet, MaNV,
	CV.*
FROM CONGVIEC CV
JOIN NHIEMVU NHV ON NHV.MaCV = CV.MaCV
GO

--b)Nhiệm vụ và công việc tiên quyết của một dự án
CREATE OR ALTER VIEW vw_congviec_tienquyet
AS
SELECT 
	afCV.*,
	bfCV.MaCV AS MaCVTQ, bfCV.TenCV AS TenCVTQ, bfCV.TienDo AS TienDoTQ, bfCV.TrangThai AS TrangThaiTQ
FROM CONGVIEC afCV
JOIN CONGVIEC bfCV ON bfCV.MaCV = afCV.CVTienQuyet
GO

CREATE OR ALTER VIEW vw_nhiemvu_tienquyet
AS
SELECT 
	afNV.*,
	bfNV.MaNV AS MaNVTQ, bfNV.TenNhiemVu AS TenNVTQ, bfNV.TrangThai AS TrangThaiTQ
FROM NHIEMVU afNV
JOIN NHIEMVU bfNV ON bfNV.MaNhiemVu = afNV.MaTienQuyet
GO


--3. Xem thông tin ngày nghỉ của nhân viên 
--a)Thông tin ngày nghỉ của nhân viên trong từng Sprint của dự án
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
	DD.MaNV,
	UL.MaGiaiDoan, UL.MaDA, UL.SoNgayNghi, UL.TimeSprint, UL.TimeTasks, 
	SP.NgayBD AS BDSprint, SP.NgayKT AS KTSprint
FROM DIEMDANH DD
JOIN UOCLUONG UL ON UL.MaNV = DD.MaNV
JOIN GIAIDOAN SP ON SP.MaGiaiDoan = UL.MaGiaiDoan
WHERE DD.Ngay BETWEEN NgayBD AND NgayKT
go

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
--1.Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án
create or alter trigger tr_addUocLuong on NHOM
AFTER INSERT AS
BEGIN
   insert into UOCLUONG
   select i.MaNV, i.MaDA, GIAIDOAN.MaGiaiDoan, NULL, NULL, NULL 
   from inserted AS i
	 join GIAIDOAN on i.MaDA= GIAIDOAN.MaDA
   where GIAIDOAN.NgayKT >= GETDATE()
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

--3.Cập nhật trạng thái dự án (update) sau khi cập nhật tiến độ (%)
CREATE OR ALTER TRIGGER tr_Update_Trangthai
ON DUAN
AFTER UPDATE
AS
BEGIN
	DECLARE @MADA VARCHAR(10)
	SELECT @MADA=inserted.MaDA FROM inserted
    IF EXISTS ( SELECT * FROM inserted WHERE TienDo = 100
    )
    BEGIN
        UPDATE DUAN
        SET TrangThai = 'Done'
        FROM DUAN
        WHERE DUAN.MaDA=@MADA
    END
	ELSE
	BEGIN
        RAISERROR('Không thể cập nhật dự án',16,2)
        ROLLBACK TRAN
    END;
END
GO

--4.Kiểm tra tính hợp lệ khi thiệt lập giai đoạn mới (update) cho dự án dựa trên trạng thái
CREATE OR ALTER TRIGGER tr_CheckGiaiDoan
ON DUAN
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT *
        FROM deleted as i
        WHERE i.TrangThai <> 'Done' and i.TienDo <> 100
    )
    BEGIN
        RAISERROR('Không thể thiết lập giai đoạn mới có thể do nhiệm vụ vẫn chưa được hoàn thành', 16, 1)
        ROLLBACK TRAN
    END
END;
GO

--5 Xóa NhiemVu trước khi xóa CongViec
CREATE OR ALTER TRIGGER deleteCongViec on CONGVIEC
AFTER DELETE AS
BEGIN
    IF exists (SELECT *FROM NHIEMVU as nv join deleted on deleted.MaCV = nv.MaCV 
	                   WHERE nv.TrangThai not in ('Done'))
	BEGIN
	      RAISERROR('Không thể xóa công việc vì nhiệm vụ chưa được hoàn thành!', 16, 1)
          ROLLBACK TRAN
    END
END
GO

--6 Kiểm tra thứ tự nhiệm vụ tiên quyết, nếu chưa hoàn thành nhiệm vụ tiên quyết và công việc tiên quyết trước đó thì không được làm nhiệm vụ hiện tại
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

--7) Kiểm tra nếu nhân viên được chỉ định làm PM nhưng đang làm PM cho dự án khác thì hủy chỉ định
CREATE OR ALTER TRIGGER tr_chidinh_PM ON DUAN
AFTER INSERT, UPDATE
AS
DECLARE @pm INT, @mada int=0, @madaNew int
	--Kiểm tra MaPM mới cập nhật có tồn tại trong DUAN hay chưa
SELECT @pm=soluong FROM (
	SELECT COUNT(new.MaPM) AS soluong
	FROM inserted new, DUAN
	WHERE new.MaPM = DUAN.MaPM
) AS Q
IF (@pm > 1)
BEGIN
	RAISERROR('Người này đang quản lý nhóm khác trong dự án này', 16, 1)
	ROLLBACK TRAN;
END
GO

--8) Kiểm tra nếu nhân viên được chỉ định làm Team Leader nhưng đang làm Team Leader cho nhóm/dự án khác thì hủy chỉ định
CREATE OR ALTER TRIGGER tr_chidinh_teamleader ON TRUONGNHOM
AFTER INSERT, UPDATE
AS
DECLARE @tl INT, @mada int=0, @madaNew int
	--Kiểm tra Team Leader mới cập nhật có tồn tại trong TEAMLEADER hay chưa
SELECT @tl = soluong FROM (
	SELECT COUNT(new.MaNV) as soluong
	FROM inserted new JOIN TRUONGNHOM
	ON new.MaDA = TRUONGNHOM.MaDA AND new.MaNV = TRUONGNHOM.MaNV
) AS Q
IF (@tl > 1)
BEGIN
	RAISERROR('Người này đang quản lý nhóm khác trong dự án này', 16, 1)
	ROLLBACK TRAN;
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
	SELECT @CapPerDay = NHOM.CapPerDay
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
CREATE TRIGGER tr_ktr_ngaynghi_giaidoan
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
