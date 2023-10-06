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
GO
--###Constraints






















--###Triggers
--1) Kiểm tra thứ tự nhiệm vụ tiên quyết, nếu chưa hoàn thành nhiệm vụ tiên quyết và công việc tiên quyết trước đó thì không được làm nhiệm vụ hiện tại
CREATE OR ALTER TRIGGER tr_kiemtra_tienquyet ON NHIEMVU
AFTER UPDATE
AS
DECLARE @newNV varchar(10), @trangthaiOld varchar(30), @trangthaiTQ varchar(30), @tgUocTinh INT, @tgThucTe INT
SELECT @newNV=n.MaNhiemVu, @trangthaiOld=o.TrangThai, @tgThucTe=o.ThoiGianLamThucTe
FROM inserted n, deleted o, NHIEMVU NV
WHERE NV.MaNhiemVu = n.MaNhiemVu AND n.MaNhiemVu = o.MaNhiemVu

--Lấy trạng thái nhiệm vụ tiên quyết
SELECT @trangthaiTQ=NVTQ.TrangThai
FROM (SELECT * FROM NHIEMVU WHERE MaNhiemVu = @newNV) NV
JOIN NHIEMVU NVTQ ON NV.MaTienQuyet = NVTQ.MaNhiemVu

IF(@trangthaiTQ != 'Done')
BEGIN
	UPDATE NHIEMVU SET ThoiGianLamThucTe=@tgThucTe, TrangThai=@trangthaiOld
		WHERE MaNhiemVu=@newNV
	RAISERROR('Nhiệm vụ tiên quyết chưa hoàn thành',16,1)
END
GO
--2) Kiểm tra nếu nhân viên được chỉ định làm PM nhưng đang làm PM cho dự án khác thì hủy chỉ định
CREATE OR ALTER TRIGGER tr_chidinh_PM ON DUAN
INSTEAD OF INSERT, UPDATE
AS
DECLARE @pm varchar(10) = NULL, @mada int=0, @madaNew int
--Kiểm tra MaPM mới cập nhật có tồn tại trong DUAN hay chưa
SELECT @pm=new.MaPM, @madaNew=new.MaDA
FROM inserted new, DUAN
WHERE new.MaPM = DUAN.MaPM
print @pm
IF (@pm IS NULL)
BEGIN
	INSERT INTO DUAN (TenDA, TienDo, NgayKT, NgayBD, ChiPhi, GiaiDoan, MaPM)
	SELECT new.TenDA, new.TienDo, new.NgayKT, NgayBD, new.ChiPhi, new.GiaiDoan, new.MaPM
	FROM inserted new
END
ELSE
BEGIN
	RAISERROR('Người này đang quản lý dự án khác', 16, 1)
END
GO
--3) Xóa TeamLeader và Team trước khi xóa DUAN
CREATE OR ALTER TRIGGER tr_rangbuoc_xoaDA ON DUAN
INSTEAD OF DELETE
AS
DECLARE @mada INT
SELECT @mada=ol.MaDA
FROM deleted ol
JOIN DUAN ON DUAN.MaDA = ol.MaDA
BEGIN
	DELETE FROM TEAM WHERE MaDA = @mada
	DELETE FROM TEAMLEADER WHERE MaDA = @mada
	DELETE FROM DUAN WHERE MaDA = @mada
END
GO