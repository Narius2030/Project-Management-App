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
	--Nếu kiểm tra nvtq chưa Done thì trả về giá trị cũ
	UPDATE NHIEMVU SET ThoiGianLamThucTe=@tgThucTe, TrangThai=@trangthaiOld
		WHERE MaNhiemVu=@newNV
	RAISERROR('Nhiệm vụ tiên quyết chưa hoàn thành',16,1)
END
GO

--2) Kiểm tra nếu nhân viên được chỉ định làm PM nhưng đang làm PM cho dự án khác thì hủy chỉ định
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

--3) Xử lý ràng buộc trước khi xóa DUAN
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

--4) Kiểm tra nếu nhân viên được chỉ định làm Team Leader nhưng đang làm Team Leader cho nhóm/dự án khác thì hủy chỉ định
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

--Time Task > Time Sprint thì hủy phân công
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