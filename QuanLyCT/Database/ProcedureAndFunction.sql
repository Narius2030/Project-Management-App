--Procedure khi xoá giai đoạn buộc phải xoá tất cả nhân vẫn đang làm tại dự án và giai đoạn đó ở bảng ước lượng
Create Or Alter Procedure sp_XoaUocLuong_GD_DA
@magd varchar(10),@mada int
as
begin
	Delete From UocLuong
	where UOCLUONG.MaGiaiDoan=@magd and UOCLUONG.MaDA=@mada
end
Go
--####Procedure####
--Procedure Đăng Nhập kiểm tra có tồn tại tài khoản không *
CREATE OR ALTER PROCEDURE sp_ktrDangNhap
@matk VARCHAR(20), @matkhau VARCHAR(20), @check INT OUTPUT
AS
BEGIN
	SELECT @check=COUNT(*) FROM NHANVIEN
	WHERE MaTaiKhoan = @matk AND MatKhau = @matkhau
END
GO

--Xem danh sách thành viên trong 1 dự án trong 1 nhóm *
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
GO

--PROCEDURE CẬP nhật tiến độ công việc dựa trên (tổng số lượng nhiệm vụ hoàn thành x100)/tổng số lượng
--nhiệm vụ trong 1 giai đoạn *
CREATE OR ALTER PROCEDURE sp_TinhTienDoCV
@MaCV int, @magiaidoan varchar(20),@ketqua REAL OUTPUT
as 
begin
	declare @soluongnvhoanthanh real
	declare @soluongnhiemvu real

      --Tìm số lượng nhiệm vụ hoàn thành trong 1 giai đoạn
	select @soluongnvhoanthanh= count(MaNhiemVu)
	FROM vw_nhiemvu_giaidoan_duan
	WHERE TrangThai = 'Done' AND MaCV=@MaCV AND MaGiaiDoan=@magiaidoan

      --Tìm tất cả nhiệm vụ trong 1 giai đoạn
	select  @soluongnhiemvu= count(MaNhiemVu)
	FROM vw_nhiemvu_giaidoan_duan
	WHERE MaCV=@MaCV AND MaGiaiDoan=@magiaidoan
      
      --Nếu số lượng nhiệm vụ hoàn thành lớn hơn 0 thì cập nhật tiên độ công việc ngược lại thì không
	if(@soluongnvhoanthanh >0)
	begin
		set @ketqua=(@soluongnvhoanthanh*100) / @soluongnhiemvu 
		update CongViec set CongViec.TienDo=@ketqua where CongViec.MaCV =@MaCV 
	end
	else
		set @ketqua=0
		update CongViec set CongViec.TienDo=@ketqua where CongViec.MaCV =@MaCV 
end
GO

--Procedure Cập nhật trạng thái dựa trên tiến độ vừa được cập nhật ở trên*

CREATE OR ALTER Procedure sp_UpdateTrangThai
@macongviec int ,@trangthai varchar(20) output
as
begin
	declare @ketqua real
	select @ketqua= TienDo From CONGVIEC
	where CONGVIEC.MaCV=@macongviec
       --Kiểm tra tiến độ rơi vào 3 trường hợp sau
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
End
GO

--Kiểm Tra Công Việc Tiên Quyết để xoá đi nó cần phải cập nhật công việc có
--mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu*
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

--Kiểm Tra Nhiệm Vụ Tiên Quyết để xoá đi nó cần phải cập nhật Nhiệm Vụ
--có mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu *
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
GO

--Kiểm Tra giai đoạn trước đã có công việc trước khi tạo giai đoạn mới*
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

--Kiểm Tra Trạng Thái Nhiệm Vụ Tiên Quyết đã hoàn thành chưa thì mới làm nhiệm vụ hiện tại*
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

--####Function####
--Kiểm Tra Tồn tại nhóm trưởng function trả ra 1 giá trị*
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

--Tìm Trưởng Nhóm trả ra 1 bảng  và có tham số đầu vào****
CREATE OR ALTER FUNCTION sfn_TimTruongNhom(@tennhom nvarchar(20), @mada int)
RETURNS TABLE
AS
RETURN (
	SELECT 
		MaNV, TenNhom, MaDA, HoTen, ChucVu, Levels, SoGioMotNg
	FROM vw_danhsach_truongnhom
	WHERE TenNhom=@tennhom AND MaDA=@mada
)
GO

--Function Kiểm tra giai đoạn đã hoàn thành hay chưa ****
CREATE OR ALTER FUNCTION sfn_KiemTraGiaiDoan(@mada int, @MaGiaiDoan VARCHAR(255))
RETURNS @table TABLE 
(
	MaDA INT,
	MaGiaiDoan VARCHAR(255),
	SoLuongCongViec INT
)
AS
BEGIN
	INSERT INTO @table
	SELECT *
	FROM vw_congviec_chuahoanthanh
	WHERE MaGiaiDoan = @MaGiaiDoan AND MaDA = @mada
	return
END
GO

--Tính tổng time task dựa trên thời gian ước tính của  tất cả nhiệm vụ được giao *
CREATE OR ALTER FUNCTION sfn_SumTimeTask (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
RETURNS INT
AS
BEGIN

	declare @timetask  varchar(10)
	SELECT @timetask=sum(ThoiGianUocTinh) 
	FROM vw_nhiemvu_giaidoan_duan
	WHERE MaNV=@manhanvien AND MaDA=@maduan AND MaGiaiDoan=@magiaidoan
	RETURN @timetask
END
GO

--Cập nhật timetask dựa trên thời gian ước tính của tất cả nhiệm vụ đã hoàn thành *
CREATE OR ALTER FUNCTION sfn_CapNhatTimeTask (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
RETURNS INT
AS
BEGIN

	DECLARE @timetask  varchar(10)
	SELECT @timetask=sum(ThoiGianUocTinh) 
	FROM vw_nhiemvu_giaidoan_duan
	WHERE TrangThai = 'Done' AND MaNV=@manhanvien AND MaDA=@maduan AND MaGiaiDoan=@magiaidoan
	RETURN @timetask
END
GO

--Tìm số giờ nghỉ trong một giai đoạn của dự án
CREATE OR ALTER FUNCTION sfn_TimThoiGianNghi(@manhanvien varchar(10), @magiaidoan VARCHAR(20))
RETURNS DECIMAL
AS
BEGIN
	DECLARE @sumThoiGianNghi DECIMAL

	-- Tìm số ngày nghỉ trong giaidoan của duan
	SELECT 
		@sumThoiGianNghi=(COUNT(MaNV)*SoGioMotNg)
	FROM vw_ngaynghi_trong_duan
	WHERE MaGiaiDoan=@magiaidoan AND MaNV=@manhanvien AND (Ngay BETWEEN NgayBD AND NgayKT)
	GROUP BY MaNV, SoGioMotNg

	IF @sumThoiGianNghi IS NULL
		SET @sumThoiGianNghi = 0

	RETURN @sumThoiGianNghi
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

