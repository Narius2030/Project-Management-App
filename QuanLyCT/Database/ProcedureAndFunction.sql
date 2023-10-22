--Procedure Dang Nhap co tham số đầu vào trả ra 1 giá trị cụ thể
CREATE OR ALTER PROCEDURE sp_ktrDangNhap
@matk VARCHAR(20), @matkhau VARCHAR(20), @check INT OUTPUT
AS
BEGIN
	SELECT @check=COUNT(*) FROM NHANVIEN
	WHERE TaiKhoan = @matk AND MatKhau = @matkhau
END
GO
--Kiểm Tra  Giai đoạn đã hoàn thành chưa  trước khi tạo cái khác
CREATE or alter PROCEDURE sp_KiemTraGiaiDoan
    @maduan int
AS
BEGIN
    SELECT DuAn.MaDA,GIAIDOAN.MaGiaiDoan ,COUNT(CONGVIEC.MaCV) as[ số lượng công việc]
    FROM CongViec,GIAIDOAN,DUAN
    WHERE CONGVIEC.TrangThai = 'Done'
      AND GIAIDOAN.MaGiaiDoan=CONGVIEC.MaGiaiDoan and GIAIDOAN.MaDA=DUAN.MaDA
	  AND DUAN.MaDA=@maduan
	 group by DUAN.MaDA,GIAIDOAN.MaGiaiDoan
END
