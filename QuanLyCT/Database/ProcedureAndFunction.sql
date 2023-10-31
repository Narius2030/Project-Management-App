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
--PROCEDURE CẬP NHẬT TIẾN ĐỘ CÔNG VIỆC
CREATE OR ALTER PROCEDURE sp_TinhTienDoCV
@MaCV int, @magiaidoan varchar(20),@ketqua REAL OUTPUT
as 
begin
	declare @soluongnvhoanthanh real
	declare @soluongnhiemvu real
	select @soluongnvhoanthanh= count(NHIEMVU.MaNhiemVu)
	From CONGVIEC ,NHIEMVU,GIAIDOAN
	where NHIEMVU.MaCV=CONGVIEC.MaCV
	and CONGVIEC.MaGiaiDoan=GIAIDOAN.MaGiaiDoan
	and NHIEMVU.TrangThai='Done'
	and CongViec.MaCV=@MaCV and GiaiDoan.MaGiaiDoan=@magiaidoan
	select  @soluongnhiemvu= count(NHIEMVU.MaNhiemVu)
	From CONGVIEC ,NHIEMVU,GIAIDOAN
	where NHIEMVU.MaCV=CONGVIEC.MaCV
	and CONGVIEC.MaGiaiDoan=GIAIDOAN.MaGiaiDoan
	and CongViec.MaCV=@MaCV and GiaiDoan.MaGiaiDoan=@magiaidoan
	if(@soluongnvhoanthanh >0)
	begin
		set @ketqua=(@soluongnvhoanthanh*100) / @soluongnhiemvu 
		update CongViec set CongViec.TienDo=@ketqua where CongViec.MaCV =@MaCV 
	end
	else
		set @ketqua=0
		update CongViec set CongViec.TienDo=@ketqua where CongViec.MaCV =@MaCV 
end
--Procedure Cập Nhật Trạng Thái
go
CREATE OR ALTER Procedure sp_UpdateTrangThai
@macongviec int ,@trangthai varchar(20) output
as
begin
	declare @ketqua real
	select @ketqua= TienDo From CONGVIEC
	where CONGVIEC.MaCV=@macongviec
	if @ketqua=0
	  Update CONGVIEC set TrangThai='Pending'
	  where CONGVIEC.MaCV=@macongviec
	else if @ketqua>=0 and  @ketqua <=99
	   Update CONGVIEC set TrangThai='Doing'
	   where CONGVIEC.MaCV=@macongviec
	else
		Update CONGVIEC set TrangThai='Done'
	    where CONGVIEC.MaCV=@macongviec
	select @trangthai=CONGVIEC.TrangThai From CONGVIEC where 
	CONGVIEC.MaCV=@macongviec
end
GO
--Tìm Trưởng Nhóm trả ra 1 bảng có tham số đầu vào
CREATE OR ALTER	PROCEDURE sp_TimTruongNhom
@tennhom nvarchar(20),@mada int
as
begin 
	SELECT TN.MaNV, CONCAT(NV.HovaTenDem, ' ', NV.Ten) HoTen, NV.ChucVu, NV.Levels, N.SoGioMotNg
                                FROM TRUONGNHOM TN
                                INNER JOIN NHOM N
                                ON N.TenNhom=TN.TenNhom and N.MaDA=TN.MaDA
								INNER JOIN NHANVIEN NV
                                 on  NV.MaNV=N.MaNV and TN.MaNV=NV.MaNV 
								 WHERE TN.TenNhom=@tennhom and TN.MaDA=@mada
end

go

--xem danh sách thành viên trong 1 dự án trong 1 nhóm
CREATE OR ALTER PROCEDURE sp_dstvmotnhomtrongmotduan
@mada int, @tennhom nvarchar(20)
as
begin
SELECT N.MaNV, CONCAT(NV.HovaTenDem, ' ', NV.Ten) HoTen, NV.ChucVu, NV.Levels, N.SoGioMotNg
                                FROM NHOM N
                                INNER JOIN NHANVIEN NV
                                ON N.MaNV = NV.MaNV
                                WHERE N.MaDA = @mada AND N.TenNhom = @tennhom
end

go
--Kiểm Tra Tồn tại nhóm trưởng function trả ra 1 giá trịS
CREATE OR ALTER FUNCTION CheckTonTaiNhomTruong(@TenNhom VARCHAR(100), @MaDA INT)
RETURNS INT
AS
BEGIN
    DECLARE @Result INT

    IF EXISTS (SELECT 1 FROM TRUONGNHOM WHERE TenNhom = @TenNhom AND MaDA = @MaDA)
        SET @Result = 1
    ELSE
        SET @Result = 0
    RETURN @Result
END;
GO

--Kiểm Tra Nhiệm Vụ Tiên Quyết để xoá đi nó cần phải cập nhật Nhiệm Vụ có mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu
CREATE OR ALTER PROCEDURE sp_KiemTraNhiemVu
    @manhiemvu varchar(10)
AS
BEGIN
	declare @matienquyet varchar(10)
    select @matienquyet=nvtq.MaNhiemVu From NHIEMVU as nv,NHIEMVU as nvtq
	where nv.MaNhiemVu=nvtq.MaTienQuyet and nv.MaNhiemVu= @manhiemvu
    BEGIN
        UPDATE NHIEMVU
        SET  MaTienQuyet = NULL
        WHERE NHIEMVU.MaNhiemVu =@matienquyet
    END
END
go
--Kiểm Tra Công Việc Tiên Quyết để xoá đi nó cần phải cập nhật công việc có mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu
CREATE OR ALTER PROCEDURE sp_KiemTraCongViec
    @macongviec INT
AS
BEGIN
	declare @matienquyet int
    select @matienquyet=cvtq.MaCV From CONGVIEC as cv,CONGVIEC as cvtq
	where cv.MaCV=cvtq.CVTienQuyet and cv.MaCV= @macongviec
    BEGIN
        UPDATE CONGVIEC
        SET CVTienQuyet = NULL
        WHERE CONGVIEC.MaCV =@matienquyet
    END
END
GO
--Cập nhật TimeSprint
CREATE OR ALTER FUNCTION sfn_CapNhatTimeSprint (@magiaidoan VARCHAR(20), @maDA INT, @soGioNg INT)
RETURNS DECIMAL
AS
BEGIN
	DECLARE @sumDays INT, @capPerDay DECIMAL

	--TÍnh số ngày trong giai đoạn đang chọn
	SELECT 
		@sumDays=DATEDIFF(DAY, NgayBD, NgayKT)
	FROM GIAIDOAN
	WHERE MaGiaiDoan=@magiaidoan AND MaDA =@maDA
	GROUP BY MaGiaiDoan, NgayBD, NgayKT

	RETURN @sumDays * @soGioNg
END
GO

--SELECT dbo.sfn_CapNhatTimeSprint('01DA03', 3, 8)

--Tìm số giờ nghỉ trong một giai đoạn của dự án
CREATE OR ALTER FUNCTION sfn_TimThoiGianNghi(@manhanvien varchar(10), @magiaidoan VARCHAR(20),  @soGioNg INT)
RETURNS DECIMAL
AS
BEGIN
	DECLARE @sumThoiGianNghi DECIMAL

	-- Tìm số ngày nghỉ trong giaidoan của duan
	SELECT 
		@sumThoiGianNghi=(COUNT(dd.MaNV)*@soGioNg)
	FROM DIEMDANH dd
	JOIN NHOM n ON n.MaNV = dd.MaNV
	JOIN GIAIDOAN gd ON gd.MaDA = n.MaDA
	WHERE gd.MaGiaiDoan=@magiaidoan AND dd.MaNV=@manhanvien AND (dd.Ngay BETWEEN gd.NgayBD AND gd.NgayKT)
	GROUP BY dd.MaNV

	IF @sumThoiGianNghi IS NULL
		SET @sumThoiGianNghi = 0

	RETURN @sumThoiGianNghi
END
GO

--SELECT dbo.sfn_TimThoiGianNghi('NV002', '01DA03')

-- Kiểm tra nhiệm vụ tiên quyết trước khi xóa
CREATE OR ALTER PROCEDURE sp_setNullNVTienQuyet
@manv VARCHAR(10)
AS
BEGIN
	UPDATE NHIEMVU SET MaTienQuyet=NULL WHERE MaNhiemVu IN (SELECT MaNhiemVu FROM NHIEMVU NV WHERE EXISTS (
		SELECT * FROM NHIEMVU TQ
		WHERE TQ.MaNhiemVu=@manv AND TQ.MaNhiemVu = NV.MaTienQuyet
	))
END
GO
--Kiểm Tra Trạng Thái Nhiệm Vụ Tiên Quyết đã hoàn thành chưa thì mới làm nhiệm vụ hiện tại
Create or Alter Procedure sp_KiemTraNhiemVuTienQuyet
@manv varchar(10), @check int output 
as
begin
	if exists (select nvtq.MaTienQuyet From NHIEMVU as nv ,NHIEMVU as nvtq
		where nv.MaNhiemVu=nvtq.MaTienQuyet
		and nvtq.MaNhiemVu=@manv and nv.TrangThai='Done')
	 begin
		 set @check=1
	 end
	else
	begin
		 set @check=0
	end
end
GO
--CẬp nhật timetask
CREATE OR ALTER FUNCTION sfn_CapNhatTimeTask (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
RETURNS INT
AS
BEGIN

	declare @timetask  varchar(10)
	select @timetask=sum(NHIEMVU.ThoiGianUocTinh) 
	From NHIEMVU
	join CONGVIEC on CONGVIEC.MaCV=NHIEMVU.MaCV
	join DUAN on CONGVIEC.MaDA=DUAN.MaDA
	join NHANVIEN on NHANVIEN.MaNV=NHIEMVU.MaNV
	join GiaiDoan on GiaiDoan.magiaidoan=CONGVIEC.MaGiaiDoan
	where NHANVIEN.MaNV=@manhanvien and DUAN.MaDA=@maduan
			and GIAIDOAN.MaGiaiDoan=@magiaidoan
			and NHIEMVU.TrangThai='Done'
	return @timetask
END
Go
--Tính Tổng Time Task
CREATE OR ALTER FUNCTION sfn_SumTimeTask (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
RETURNS INT
AS
BEGIN

	declare @timetask  varchar(10)
	select @timetask=sum(NHIEMVU.ThoiGianUocTinh) 
	From NHIEMVU
	join CONGVIEC on CONGVIEC.MaCV=NHIEMVU.MaCV
	join DUAN on CONGVIEC.MaDA=DUAN.MaDA
	join NHANVIEN on NHANVIEN.MaNV=NHIEMVU.MaNV
	join GiaiDoan on GiaiDoan.magiaidoan=CONGVIEC.MaGiaiDoan
	where NHANVIEN.MaNV=@manhanvien and DUAN.MaDA=@maduan
			and GIAIDOAN.MaGiaiDoan=@magiaidoan
	return @timetask
END