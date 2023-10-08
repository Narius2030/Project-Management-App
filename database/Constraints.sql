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
WHERE DD.Ngay BETWEEN NgayBD AND NgayKT

GO

SELECT * FROM vw_ngaynghi_trong_duan
--###Constraints


Go

--###Triggers
--	Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án
create trigger tr_addUocLuong on TEAMLEADER
AFTER INSERT AS
BEGIN
   insert into UOCLUONG
   select i.MaNV, i.MaDA, SPRINT.MaSprint, NULL, NULL, NULL 
   from inserted AS i
	 join SPRINT on i.MaDA= SPRINT.MaDA
   where SPRINT.NgayKT >= GETDATE()
END;

Go

--	Kiểm tra dự án đang ở trạng thái “trì hoãn”, “hoàn thành” hay không, nếu có thì được xóa (delete) và ngược lại
CREATE TRIGGER tr_DeleteDuAn
ON DUAN
AFTER DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted WHERE deleted.GiaiDoan in ('Done', 'Delay'))
    BEGIN
        print('Không thể xóa dự án');
        ROLLBACK;
    END;
    
END;

Go

--	Cập nhật trạng thái dự án (update) sau khi cập nhật tiến độ (%)
CREATE TRIGGER tr_Update_Trangthai
ON DUAN
AFTER UPDATE
AS
BEGIN
    IF EXISTS ( SELECT 1 FROM inserted WHERE TienDo = 100
    )
    BEGIN
        UPDATE DUAN
        SET GiaiDoan = 'Done'
        FROM DUAN
        JOIN inserted ON DUAN.MaDA = inserted.MaDA;
    END
	ELSE
	 BEGIN
        print('Không thể cập nhật dự án');
        ROLLBACK;
    END;
END;

Go

--	Kiểm tra tính hợp lệ khi thiệt lập giai đoạn mới (update) cho dự án dựa trên trạng thái
CREATE TRIGGER tr_CheckGiaiDoan
ON DUAN
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted as i
        WHERE i.GiaiDoan <> 'Done' and i.TienDo <> 100
    )
    BEGIN
        PRINT('Không thể thiết lập giai đoạn mới có thể do nhiệm vụ vẫn chưa được hoàn thành')
        ROLLBACK;
    END
END;


