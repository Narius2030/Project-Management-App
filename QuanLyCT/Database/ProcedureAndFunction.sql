--Procedure Dang Nhap co tham số đầu vào trả ra 1 giá trị cụ thể
CREATE OR ALTER PROCEDURE sp_ktrDangNhap
@matk VARCHAR(20), @matkhau VARCHAR(20), @check INT OUTPUT
AS
BEGIN
	SELECT @check=COUNT(*) FROM NHANVIEN
	WHERE MaTaiKhoan = @matk AND MatKhau = @matkhau
END
GO
--Kiểm Tra  Giai đoạn đã hoàn thành chưa  trước khi tạo cái khác
CREATE or alter PROCEDURE sp_KiemTraGiaiDoan
    @maduan int,
    @MaGiaiDoan VARCHAR(255)
AS
BEGIN
    SELECT DA.MaDA,GD.MaGiaiDoan ,COUNT(CV.MaCV) as[ số lượng công việc]
    FROM CongViec CV
    INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
    INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
    WHERE CV.TrangThai != 'Done'
      AND CV.MaGiaiDoan = @MaGiaiDoan
      AND DA.MaDA = @maduan
	 group by DA.MaDA,GD.MaGiaiDoan
END
GO
--Kiểm Tra  Giai đoạn trước đã có công việc trước khi tạo giai đoạn mới
CREATE OR ALTER PROCEDURE sp_KiemTraGiaiDoanTruoc
    @MaDuAn INT,
    @MaGiaiDoan VARCHAR(255)
AS
BEGIN
    DECLARE @Count INT

    SELECT @Count = COUNT(*)
    FROM CongViec CV
    INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
    INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
    WHERE CV.MaGiaiDoan = @MaGiaiDoan
        AND DA.MaDA = @MaDuAn

    -- Trả về kết quả
    IF @Count > 0
        BEGIN
            SELECT 'true' AS Result
        END
    ELSE
        BEGIN
            SELECT 'false' AS Result
        END
END
GO
