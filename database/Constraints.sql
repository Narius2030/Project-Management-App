-- ###Views

-- 1.Xem danh sách nhân viên và nhóm
--a)Tất cả
CREATE OR ALTER VIEW vw_nhanvien_trong_duan
AS
SELECT 
	NV.MaNV, CONCAT(HovaTenDem,' ',Ten) AS HoTen, ChucVu, Levels,
	TM.TenNhom, TM.MaDA, TM.CapPerDay
FROM NHANVIEN NV
JOIN TEAM TM ON TM.MaNV = NV.MaNV
GO

--b)Trưởng nhóm
CREATE OR ALTER VIEW vw_teamleader_trong_duan
AS
SELECT
	NV.MaNV, CONCAT(HovaTenDem,' ',Ten) AS HoTen, ChucVu, Levels,
	TLD.TenNhom, TLD.MaDA
FROM TEAMLEADER TLD
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
	WHERE pm.MaPM = NV.MaNV OR NV.ChucVu IN('CEO', 'TEAM LEADER')
)
GO

CREATE OR ALTER VIEW vw_khongla_teamleader
AS
SELECT *
FROM NHANVIEN NV
WHERE NOT EXISTS(
	SELECT *
	FROM TEAMLEADER tl
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
--c)Những công việc đang sắp trễ tiến độ
CREATE OR ALTER VIEW vw_cvtre
AS
SELECT cv.MaDA, cv.MaCV, cv.TenCV, cv.MaSprint, cv.TenNhom, cv.TrangThai
FROM CONGVIEC cv
JOIN SPRINT spt ON cv.MaSprint = spt.MaSprint
WHERE spt.NgayKT <= DATEADD(day, 4, CONVERT(DATE, GETDATE())) AND spt.NgayKT > CONVERT(DATE, GETDATE()) AND cv.TrangThai != 'Done'
GO

--d)Đếm và show thông tin bao nhiêu nhiệm vụ đang trễ tiến độ trong mỗi công việc của  từng một dự án
CREATE OR ALTER VIEW vw_nvtrehan_cv_da
AS
SELECT nv.MaNhiemVu, nv.TenNhiemVu, nv.TrangThai, cv.MaCV, spt.MaDA, nv.MaNV, GETDATE() as HomNay, spt.NgayKT
FROM NHIEMVU nv
JOIN CONGVIEC cv ON cv.MaCV = nv.MaCV
JOIN SPRINT spt ON cv.MaSprint = spt.MaSprint
WHERE spt.NgayKT <= DATEADD(day, 4, CONVERT(DATE, GETDATE())) AND spt.NgayKT > CONVERT(DATE, GETDATE()) AND nv.TrangThai != 'Done'
GO

--3. Xem thông tin ngày nghỉ của nhân viên 
--a)Thông tin ngày nghỉ của nhân viên trong từng Sprint của dự án
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
	DD.MaNV,
	UL.MaSprint, UL.MaDA, UL.SoNgayNghi, UL.TimeSprint, UL.TimeTasks, 
	SP.NgayBD AS BDSprint, SP.NgayKT AS KTSprint
FROM DIEMDANH DD
JOIN UOCLUONG UL ON UL.MaNV = DD.MaNV
JOIN SPRINT SP ON SP.MaSprint = UL.MaSprint
WHERE DD.Ngay BETWEEN NgayBD AND NgayKT
go
--4)Xem Thông Tin Tài Nguyên ĐƯỢC CẤP CHO TỪNG DỰ ÁN
Create OR ALTER VIEW V_TAINGUYEN
AS 
SELECT *FROM TAINGUYEN,CAP,DUAN
WHERE TAINGUYEN.MaTN=CAP.MaTN AND DUAN.MaDA=CAP.MaDA
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
--1.Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án
create trigger tr_addUocLuong on TEAM
AFTER INSERT AS
BEGIN
   insert into UOCLUONG
   select i.MaNV, i.MaDA, SPRINT.MaSprint, NULL, NULL, NULL 
   from inserted AS i
	 join SPRINT on i.MaDA= SPRINT.MaDA
   where SPRINT.NgayKT >= GETDATE()
END;

Go

--2.Kiểm tra dự án đang ở trạng thái “trì hoãn”, “hoàn thành” hay không, nếu có thì được xóa (delete) và ngược lại
CREATE TRIGGER tr_DeleteDuAn
ON DUAN
AFTER DELETE
AS
BEGIN
    IF EXISTS (SELECT * FROM deleted WHERE deleted.GiaiDoan NOT in ('Done', 'Delay'))
    BEGIN
        RAISERROR('Không thể xóa dự án',16,2);
        ROLLBACK TRAN;
    END;
    
END;

Go

--3.Cập nhật trạng thái dự án (update) sau khi cập nhật tiến độ (%)
CREATE TRIGGER tr_Update_Trangthai
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
        SET GiaiDoan = 'Done'
        FROM DUAN
        WHERE DUAN.MaDA=@MADA
    END
	ELSE
	 BEGIN
        RAISERROR('Không thể cập nhật dự án',16,2);
        ROLLBACK TRAN;
    END;
END;
go
--4.Kiểm tra tính hợp lệ khi thiệt lập giai đoạn mới (update) cho dự án dựa trên trạng thái
CREATE TRIGGER tr_CheckGiaiDoan
ON DUAN
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT *
        FROM deleted as i
        WHERE i.GiaiDoan <> 'Done' and i.TienDo <> 100
    )
    BEGIN
        RAISERROR('Không thể thiết lập giai đoạn mới có thể do nhiệm vụ vẫn chưa được hoàn thành')
        ROLLBACK TRAN;
    END
END;
GO
--5 Xóa NhiemVu trước khi xóa CongViec
CREATE TRIGGER deleteCongViec on CONGVIEC
AFTER DELETE AS
BEGIN
    IF exists (SELECT *FROM NHIEMVU as nv join deleted on deleted.MaCV = nv.MaCV 
	                   WHERE nv.TrangThai not in ('Done'))
	BEGIN
	      PRINT('Không thể xóa công việc vì nhiệm vụ chưa được hoàn thành!')
          ROLLBACK TRAN
    END
END
go
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
CREATE OR ALTER TRIGGER tr_chidinh_teamleader ON TEAMLEADER
AFTER INSERT, UPDATE
AS
DECLARE @tl INT, @mada int=0, @madaNew int
	--Kiểm tra Team Leader mới cập nhật có tồn tại trong TEAMLEADER hay chưa
SELECT @tl = soluong FROM (
	SELECT COUNT(new.MaNV) as soluong
	FROM inserted new JOIN TEAMLEADER
	ON new.MaDA = TEAMLEADER.MaDA AND new.MaNV = TEAMLEADER.MaNV
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
WHERE new.MaNV = ul.MaNV AND new.MaDA = ul.MaDA AND new.MaSprint = ul.MaSprint
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
	DELETE FROM TEAM WHERE MaDA = @mada
	DELETE FROM TEAMLEADER WHERE MaDA = @mada
	DELETE FROM CAP WHERE MaDA = @mada
	DELETE FROM UOCLUONG WHERE MaDA = @mada
	--Xóa DUAN
	DELETE FROM DUAN WHERE MaDA = @mada
END
GO

--12)Thiết lập lại thời gian timesprint khi có nhân viên xin nghỉ
CREATE TRIGGER UpdateTimeSprint
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;
	DECLARE @MaSprint VARCHAR(15);
	DECLARE @CapPerDay INT;
	DECLARE @MaDA INT;

	--Lấy ngày nghỉ, mã nhân viên
	SELECT @NgayNghi = DIEMDANH.Ngay, @MaNV = MaNV
	FROM DIEMDANH;

	--Lấy mã sprint và mã DA có ngày nghỉ thuộc sprint
	SELECT @MaSprint = MaSprint, @MaDA = SPRINT.MaDA
	FROM SPRINT
	WHERE @NgayNghi <= SPRINT.NgayKT AND @NgayNghi >= SPRINT.NgayBD;

	--Lấy CapPerDay theo mã NV
	SELECT @CapPerDay = TEAM.CapPerDay
	FROM TEAM
	WHERE @MaNV = TEAM.MaNV AND @MaDA = TEAM.MaDA;

	IF @MaSprint IS NOT NULL
	BEGIN
		UPDATE UOCLUONG
		SET TimeSprint = TimeSprint - @CapPerDay
		WHERE @MaNV = UOCLUONG.MaNV AND @MaSprint = UOCLUONG.MaSprint AND @MaDA = UOCLUONG.MaDA;
	END
END;

go
--13.Thiết lập lại thời gian Time Tasks khi có nhiệm vụ được hoàn thành xong
CREATE TRIGGER UpdateTimeTasks
ON NHIEMVU
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo biến
    DECLARE @ThoiGianUocTinh INT
	DECLARE @MANHANVIEN VARCHAR(10)
	DECLARE @MASPRINT VARCHAR(10)
	DECLARE @MADA VARCHAR(10)
    -- tìm thời gian hoàn thành  nhiệm vụ Của  NHÂN VIÊN mới thêm hoặc mới cập nhật
	SELECT @MANHANVIEN=NHANVIEN.MaNV,@MASPRINT=CONGVIEC.MaSprint, @MADA=CONGVIEC.MaDA,@ThoiGianUocTinh=inserted.ThoiGianUocTinh FROM  inserted ,NHANVIEN,CONGVIEC
	WHERE inserted.MaNV=NHANVIEN.MaNV AND CONGVIEC.MaCV=inserted.MaCV AND inserted.TrangThai='done'
	--Cập nhật timetasks
    UPDATE UOCLUONG
    SET TimeTasks =  TimeTasks- @ThoiGianUocTinh
    WHERE MaNV = @MaNhanVien AND MaDA=@MADA AND MaSprint=@MASPRINT;
       
END

Go


--14)Kiểm tra tài nguyên có trong kho hay không trước khi cấp cho dự án
CREATE TRIGGER KTTaiNguyen
ON CAP
FOR INSERT, UPDATE
AS
BEGIN 
	DECLARE @MaTaiNguyenCap VARCHAR(50);
	DECLARE @TaiNguyenCount VARCHAR(50);

	SELECT @MaTaiNguyenCap = inserted.MaTN
	FROM inserted;

	SELECT @TaiNguyenCount = Count(*)
	FROM TAINGUYEN
	WHERE MaTN = @MaTaiNguyenCap;

	IF @TaiNguyenCount = 0
	BEGIN
		ROLLBACK;
    END
END;

GO

--15.Trigger kiểm tra nếu nhân viên nghỉ đúng thời gian Sprint nào thì cộng SoNgayNghi Sprint của nhân viên đó lên 1

CREATE TRIGGER KTNgayNghiTrongSprint
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;
	DECLARE @MaSprint VARCHAR(15);

	SELECT @NgayNghi = DIEMDANH.NgayNghi, @MaNV = MaNV
	FROM DIEMDANH;

	SELECT @MaSprint = MaSprint
	FROM SPRINT
	WHERE @NgayNghi <= SPRINT.NgayKT AND @NgayNghi >= SPRINT.NgayBD;

	IF @MaSprint IS NOT NULL
	BEGIN
		UPDATE UOCLUONG
		SET SoNgayNghi = SoNgayNghi + 1
		WHERE @MaNV = UOCLUONG.MaNV AND @MaSprint = UOCLUONG.MaSprint;
	END
END;

GO

