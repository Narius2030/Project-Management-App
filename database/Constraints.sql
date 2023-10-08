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

--2.Xem nội dung công việc và nhiệm vụ
--a)Tất cả công việc
CREATE OR ALTER VIEW vw_congviec_nhiemvu
AS
SELECT 
	MaNhiemVu, NHV.TrangThai AS TTNhiemvu, TenNhiemVu, ThoiGianLamThucTe, ThoiGianUocTinh, MaTienQuyet, MaNV,
	CV.*
FROM CONGVIEC CV
JOIN NHIEMVU NHV ON NHV.MaCV = CV.MaCV
GO

SELECT * FROM vw_congviec_nhiemvu

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
--c)Những công việc đang trễ tiến độ
CREATE OR ALTER VIEW vw_cvtre
AS
SELECT cv.MaDA, cv.MaCV, cv.TenCV, cv.MaSprint, cv.TenNhom, cv.TrangThai
FROM CONGVIEC cv
JOIN SPRINT spt ON cv.MaSprint = spt.MaSprint
WHERE spt.NgayKT <= DATEADD(day, 4, CONVERT(DATE, GETDATE())) AND spt.NgayKT > CONVERT(DATE, GETDATE()) AND cv.TrangThai != 'Done'
GO

--d)Đếm và show thông tin bao nhiêu nhiệm vụ đang trễ tiến độ trong mỗi công việc của một từng một dự án
CREATE OR ALTER VIEW vw_nvtrehan_cv_da
AS
SELECT nv.MaNhiemVu, nv.TenNhiemVu, nv.TrangThai, cv.MaCV, spt.MaDA, nv.MaNV, GETDATE() as HomNay, spt.NgayKT
FROM NHIEMVU nv
JOIN CONGVIEC cv ON cv.MaCV = nv.MaCV
JOIN SPRINT spt ON cv.MaSprint = spt.MaSprint
WHERE spt.NgayKT <= DATEADD(day, 4, CONVERT(DATE, GETDATE())) AND spt.NgayKT > CONVERT(DATE, GETDATE()) AND nv.TrangThai != 'Done'
GO

SELECT * FROM vw_nvtrehan_cv_da


--3. Đếm và show thông tin bao nhiêu nhiệm vụ đang trễ tiến độ trong mỗi công 
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
WHERE DD.NgayNghi BETWEEN NgayBD AND NgayKT
drop database QLDA
use QLDA
WHERE DD.Ngay BETWEEN NgayBD AND NgayKT

GO

SELECT * FROM vw_ngaynghi_trong_duan


--###Constraints
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
--###Triggers

GO

--Trigger kiểm tra tài nguyen
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

--Trigger kiểm tra nếu nhân viên nghỉ đúng thời gian Sprint nào thì cộng SoNgayNghi Sprint của nhân viên đó lên 1

CREATE TRIGGER KTNgayNghiTrongSprint
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;
	DECLARE @MaSprint VARCHAR(15);

	SELECT @NgayNghi = DIEMDANH.Ngay, @MaNV = MaNV
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
--Trigger kiểm tra ngày nghỉ để trừ timesprint
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