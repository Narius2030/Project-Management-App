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
	MaNhiemVu, NHV.TrangThai AS TTNHiemvu, TenNhiemVu, ThoiGianLamThucTe, ThoiGianUocTinh, MaTienQuyet, MaNV,
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
--c)Những công việc đang trễ tiến độ

--3. Đếm và show thông tin bao nhiêu nhiệm vụ đang trễ tiến độ trong mỗi công 
--a)Thông tin ngày nghỉ của nhân viên trong từng Sprint của dự án
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
	DD.*,
	UL.MaSprint, UL.MaDA, UL.SoNgayNghi, UL.TimeSprint, UL.TimeTasks, 
	SP.NgayBD AS BDSprint, SP.NgayKT AS KTSprint
FROM DIEMDANH DD
JOIN UOCLUONG UL ON UL.MaNV = DD.MaNV
JOIN SPRINT SP ON SP.MaSprint = UL.MaSprint
WHERE DD.NgayNghi BETWEEN NgayBD AND NgayKT
drop database QLDA
use QLDA
--###Constraints
--check tiến độ công việc và tiến độ dự án
ALTER TABLE CONGVIEC ADD CONSTRAINT CHECK_TIENDOCV CHECK (TienDo<=100 and TienDo>=0)
ALTER TABLE DUAN ADD CONSTRAINT CHECK_TIENDODA CHECK (TienDo <=100 and TienDo>=0)
--check Tên nhân viên và levels không chứa ký tự đặc biệt và số; SDT không chứa ký tự chữ cái
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_TENNV CHECK(Ten NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT  CHECK_LEVELS CHECK(levels NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_SDT CHECK(SDT not LIKE '[a-zA-Z_!@#$%^&*()<>?/|}{~:]%]');
--Mã nhân viên viết theo công thức: 2 ký tự đầu là “NV” + 3 ký tự số nguyên dương
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_MANV CHECK (MANV LIKE 'NV%' AND CAST(SUBSTRING(MANV, 3, 3) AS INT) > 0 AND CAST(SUBSTRING(MANV, 3, 3) AS INT) <= 999);
--Trong UOCLUONG, Time Sprint >= Time Tasks
Alter Table UocLuong add constraint CHECK_TIMESP_TIMETASK CHECK(TimeSprint >=TimeTasks)
--###Triggers