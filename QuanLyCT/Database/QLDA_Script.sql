USE [master]
GO
/****** Object:  Database [QLDA]    Script Date: 11/23/2023 8:48:54 AM ******/
CREATE DATABASE [QLDA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLDA', FILENAME = N'C:\Users\buidu\QLDA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLDA_log', FILENAME = N'C:\Users\buidu\QLDA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLDA] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLDA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLDA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLDA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLDA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLDA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLDA] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLDA] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLDA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLDA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLDA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLDA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLDA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLDA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLDA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLDA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLDA] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLDA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLDA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLDA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLDA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLDA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLDA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLDA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLDA] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLDA] SET  MULTI_USER 
GO
ALTER DATABASE [QLDA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLDA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLDA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLDA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLDA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLDA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLDA] SET QUERY_STORE = OFF
GO
USE [QLDA]
GO
/****** Object:  User [NV010]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV010] FOR LOGIN [NV010] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV009]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV009] FOR LOGIN [NV009] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV008]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV008] FOR LOGIN [NV008] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV007]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV007] FOR LOGIN [NV007] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV006]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV006] FOR LOGIN [NV006] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV005]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV005] FOR LOGIN [NV005] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV004]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV004] FOR LOGIN [NV004] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV003]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV003] FOR LOGIN [NV003] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NV002]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE USER [NV002] FOR LOGIN [NV002] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [TEAMLEAD]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE ROLE [TEAMLEAD]
GO
/****** Object:  DatabaseRole [PM]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE ROLE [PM]
GO
ALTER ROLE [PM] ADD MEMBER [NV010]
GO
ALTER ROLE [PM] ADD MEMBER [NV007]
GO
ALTER ROLE [PM] ADD MEMBER [NV003]
GO
/****** Object:  UserDefinedDataType [dbo].[Ma]    Script Date: 11/23/2023 8:48:55 AM ******/
CREATE TYPE [dbo].[Ma] FROM [varchar](10) NULL
GO
/****** Object:  UserDefinedFunction [dbo].[CheckTonTaiNhomTruong]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--####Function####
--Kiểm Tra Tồn tại nhóm trưởng function trả ra 1 giá trị*
CREATE   FUNCTION [dbo].[CheckTonTaiNhomTruong](@TenNhom VARCHAR(100), @MaDA INT)
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
/****** Object:  UserDefinedFunction [dbo].[sfn_CapNhatTimeSprint]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Cập nhật TimeSprint
CREATE   FUNCTION [dbo].[sfn_CapNhatTimeSprint] (@magiaidoan VARCHAR(20), @maDA INT, @soGioNg INT)
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
/****** Object:  UserDefinedFunction [dbo].[sfn_CapNhatTimeTask]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Cập nhật timetask dựa trên thời gian ước tính của tất cả nhiệm vụ đã hoàn thành *
CREATE   FUNCTION [dbo].[sfn_CapNhatTimeTask] (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
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
/****** Object:  UserDefinedFunction [dbo].[sfn_KiemTraGiaiDoan]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Function Kiểm tra giai đoạn đã hoàn thành hay chưa ****
CREATE   FUNCTION [dbo].[sfn_KiemTraGiaiDoan](@mada int, @MaGiaiDoan VARCHAR(255))
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
/****** Object:  UserDefinedFunction [dbo].[sfn_SumTimeTask]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Tính tổng time task dựa trên thời gian ước tính của  tất cả nhiệm vụ được giao *
CREATE   FUNCTION [dbo].[sfn_SumTimeTask] (@manhanvien varchar(10),@maduan int ,@magiaidoan varchar(10))
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
/****** Object:  UserDefinedFunction [dbo].[sfn_TimThoiGianNghi]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Tìm số giờ nghỉ trong một giai đoạn của dự án
CREATE   FUNCTION [dbo].[sfn_TimThoiGianNghi](@manhanvien varchar(10), @magiaidoan VARCHAR(20))
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
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MaNV] [dbo].[Ma] NOT NULL,
	[HovaTenDem] [nvarchar](25) NULL,
	[Ten] [nvarchar](25) NULL,
	[ChucVu] [nvarchar](20) NULL,
	[Email] [varchar](25) NULL,
	[Levels] [varchar](10) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [varchar](10) NULL,
	[MaTaiKhoan] [varchar](20) NULL,
	[MatKhau] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRUONGNHOM]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRUONGNHOM](
	[TenNhom] [nvarchar](20) NOT NULL,
	[MaDA] [int] NOT NULL,
	[MaNV] [dbo].[Ma] NULL,
PRIMARY KEY CLUSTERED 
(
	[TenNhom] ASC,
	[MaDA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHOM]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHOM](
	[MaNV] [dbo].[Ma] NOT NULL,
	[TenNhom] [nvarchar](20) NOT NULL,
	[MaDA] [int] NOT NULL,
	[SoGioMotNg] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TenNhom] ASC,
	[MaDA] ASC,
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_danhsach_truongnhom]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[vw_danhsach_truongnhom]
AS
SELECT 
	TN.*, CONCAT(NV.HovaTenDem, ' ', NV.Ten) HoTen, NV.ChucVu, NV.Levels, N.SoGioMotNg
FROM TRUONGNHOM TN
INNER JOIN NHOM N ON N.MaNV = TN.MaNV AND N.TenNhom=TN.TenNhom and N.MaDA=TN.MaDA
INNER JOIN NHANVIEN NV ON NV.MaNV = TN.MaNV
GO
/****** Object:  UserDefinedFunction [dbo].[sfn_TimTruongNhom]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Tìm Trưởng Nhóm trả ra 1 bảng  và có tham số đầu vào****
CREATE   FUNCTION [dbo].[sfn_TimTruongNhom](@tennhom nvarchar(20), @mada int)
RETURNS TABLE
AS
RETURN (
	SELECT 
		MaNV, TenNhom, MaDA, HoTen, ChucVu, Levels, SoGioMotNg
	FROM vw_danhsach_truongnhom
	WHERE TenNhom=@tennhom AND MaDA=@mada
)
GO
/****** Object:  Table [dbo].[DUAN]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DUAN](
	[MaDA] [int] IDENTITY(1,1) NOT NULL,
	[TenDA] [nvarchar](50) NULL,
	[TienDo] [real] NULL,
	[NgayKT] [date] NULL,
	[NgayBD] [date] NULL,
	[ChiPhi] [varchar](30) NULL,
	[TrangThai] [nvarchar](30) NULL,
	[MaPM] [dbo].[Ma] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIAIDOAN]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIAIDOAN](
	[MaGiaiDoan] [varchar](20) NOT NULL,
	[NoiDung] [nvarchar](30) NULL,
	[NgayBD] [date] NULL,
	[NgayKT] [date] NULL,
	[MaDA] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGiaiDoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CONGVIEC]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONGVIEC](
	[MaCV] [int] IDENTITY(1,1) NOT NULL,
	[TrangThai] [nvarchar](30) NULL,
	[CVTienQuyet] [int] NULL,
	[TenCV] [nvarchar](30) NULL,
	[TienDo] [real] NULL,
	[TenNhom] [nvarchar](20) NULL,
	[MaDA] [int] NULL,
	[MaGiaiDoan] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_congviec_chuahoanthanh]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Xem danh sách số lượng công việc chưa hoàn thành
CREATE   VIEW [dbo].[vw_congviec_chuahoanthanh]
AS
SELECT DA.MaDA, GD.MaGiaiDoan, COUNT(CV.MaCV) AS [số lượng công việc]
FROM CongViec CV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
WHERE CV.TrangThai != 'Done'
GROUP BY DA.MaDA,GD.MaGiaiDoan
GO
/****** Object:  Table [dbo].[NHIEMVU]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHIEMVU](
	[MaNhiemVu] [varchar](30) NOT NULL,
	[MaTienQuyet] [varchar](30) NULL,
	[TrangThai] [nvarchar](30) NULL,
	[ThoiGianLamThucTe] [int] NULL,
	[TenNhiemVu] [nvarchar](30) NULL,
	[ThoiGianUocTinh] [int] NULL,
	[MaNV] [dbo].[Ma] NULL,
	[MaCV] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhiemVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_nhiemvu_giaidoan_duan]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Xem danh sách nhiệm vụ trong một giai đoạn dự án
CREATE   VIEW [dbo].[vw_nhiemvu_giaidoan_duan]
AS
SELECT nvu.MaNV, nvu.MaNhiemVu, cv.MaCV, gd.MaDA, gd.MaGiaiDoan, nvu.ThoiGianUocTinh, nvu.TrangThai
FROM NHIEMVU nvu
join CONGVIEC cv on cv.MaCV=nvu.MaCV
--join DUAN da on cv.MaDA=da.MaDA
join GIAIDOAN gd on gd.magiaidoan=cv.MaGiaiDoan
GO
/****** Object:  Table [dbo].[DIEMDANH]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIEMDANH](
	[Ngay] [date] NOT NULL,
	[MaNV] [dbo].[Ma] NOT NULL,
	[NoiDung] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Ngay] ASC,
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_ngaynghi_trong_duan]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Xem danh sách ngày nghỉ*
CREATE   VIEW [dbo].[vw_ngaynghi_trong_duan]
AS
SELECT 
		dd.*, n.TenNhom, n.SoGioMotNg, gd.MaDA, gd.MaGiaiDoan, gd.NgayBD, gd.NgayKT
FROM DIEMDANH dd
JOIN NHOM n ON n.MaNV = dd.MaNV
JOIN GIAIDOAN gd ON gd.MaDA = n.MaDA
GO
/****** Object:  View [dbo].[v_DanhSachNhiemVuNhom]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--View liên quan đến nhiệm vụ của nhóm*
Create   View [dbo].[v_DanhSachNhiemVuNhom] as 
SELECT 
	NV.MaNV, CV.MaDA,GD.MaGiaiDoan,CV.MaCV,N.TenNhom,NV.MaNhiemVu , TenNhiemVu , NV.TrangThai , MaTienQuyet, NV.ThoiGianUocTinh, NV.ThoiGianLamThucTe 
FROM NHIEMVU NV
INNER JOIN NHOM N ON N.MaNV = NV.MaNV 
INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan and GD.MaDA=CV.MaDA
and GD.MaDA=N.MaDA
GO
/****** Object:  View [dbo].[v_DSDuAn_PM_LEAD]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--Hướng Làm Của Quang Huy

--Danh sách dự án cho pm
Create   View [dbo].[v_DSDuAn_PM_LEAD]
AS
SELECT da.MaDA, da.TenDA, da.TienDo, da.NgayBD, da.NgayKT, da.ChiPhi, da.TrangThai, da.MaPM, tn.MaNV as MaLead
FROM DUAN da
LEFT JOIN TRUONGNHOM tn
ON da.MaDA = tn.MaDA 
GO
/****** Object:  View [dbo].[v_DSDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Danh sách toàn bộ dự án cho CEO phân công
Create   View [dbo].[v_DSDuAn]
as select *From DuAn
GO
/****** Object:  Table [dbo].[CAP]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAP](
	[MaDA] [int] NOT NULL,
	[MaTN] [dbo].[Ma] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDA] ASC,
	[MaTN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuonsach]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuonsach](
	[isbn] [varchar](13) NOT NULL,
	[ma_cuonsach] [int] NOT NULL,
	[tinhtrang] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ma_cuonsach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DangKy]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DangKy](
	[isbn] [varchar](13) NOT NULL,
	[ma_DocGia] [int] NOT NULL,
	[ngay_dk] [date] NOT NULL,
	[ghichu] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dausach]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dausach](
	[isbn] [varchar](13) NOT NULL,
	[ma_tuasach] [int] NOT NULL,
	[ngonngu] [varchar](20) NOT NULL,
	[bia] [varchar](255) NOT NULL,
	[trangthai] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocGia]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocGia](
	[ma_DocGia] [int] IDENTITY(1,1) NOT NULL,
	[ho] [varchar](255) NOT NULL,
	[tenlot] [varchar](255) NOT NULL,
	[ten] [varchar](255) NOT NULL,
	[ngaysinh] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ma_DocGia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Muon]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Muon](
	[isbn] [varchar](13) NOT NULL,
	[ma_cuonsach] [int] NOT NULL,
	[ma_DocGia] [int] NOT NULL,
	[ngay_muon] [date] NOT NULL,
	[ngay_hethan] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiLon]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiLon](
	[ma_DocGia] [int] NOT NULL,
	[sonha] [varchar](255) NOT NULL,
	[duong] [varchar](255) NOT NULL,
	[quan] [varchar](255) NOT NULL,
	[dienthoai] [varchar](255) NOT NULL,
	[han_sd] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuaTrinhMuon]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuaTrinhMuon](
	[isbn] [varchar](13) NOT NULL,
	[ma_cuonsach] [int] NOT NULL,
	[ngay_muon] [date] NOT NULL,
	[ma_DocGia] [int] NOT NULL,
	[ngay_hethan] [date] NOT NULL,
	[ngay_tra] [date] NOT NULL,
	[tien_muon] [int] NOT NULL,
	[tien_datra] [int] NOT NULL,
	[tien_datcoc] [int] NOT NULL,
	[ghichu] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAINGUYEN]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAINGUYEN](
	[MaTN] [dbo].[Ma] NOT NULL,
	[TenTN] [nvarchar](20) NULL,
	[LoaiTaiNguyen] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Treem]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Treem](
	[ma_DocGia] [int] NOT NULL,
	[ma_DocGia_nguoilon] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tuasach]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tuasach](
	[ma_tuasach] [int] IDENTITY(1,1) NOT NULL,
	[tuasach] [varchar](255) NOT NULL,
	[tacgia] [varchar](255) NOT NULL,
	[tomtat] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ma_tuasach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UOCLUONG]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UOCLUONG](
	[MaNV] [dbo].[Ma] NOT NULL,
	[MaDA] [int] NOT NULL,
	[MaGiaiDoan] [varchar](20) NOT NULL,
	[SoNgayNghi] [int] NULL,
	[TimeSprint] [int] NULL,
	[TimeTasks] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC,
	[MaDA] ASC,
	[MaGiaiDoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CONGVIEC] ADD  DEFAULT (NULL) FOR [CVTienQuyet]
GO
ALTER TABLE [dbo].[NHANVIEN] ADD  DEFAULT ('Member') FOR [ChucVu]
GO
ALTER TABLE [dbo].[NHIEMVU] ADD  DEFAULT (NULL) FOR [MaTienQuyet]
GO
ALTER TABLE [dbo].[CAP]  WITH CHECK ADD  CONSTRAINT [FK_CAP_DUAN] FOREIGN KEY([MaDA])
REFERENCES [dbo].[DUAN] ([MaDA])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAP] CHECK CONSTRAINT [FK_CAP_DUAN]
GO
ALTER TABLE [dbo].[CAP]  WITH CHECK ADD  CONSTRAINT [FK_CAP_TAINGUYEN] FOREIGN KEY([MaTN])
REFERENCES [dbo].[TAINGUYEN] ([MaTN])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAP] CHECK CONSTRAINT [FK_CAP_TAINGUYEN]
GO
ALTER TABLE [dbo].[CONGVIEC]  WITH CHECK ADD  CONSTRAINT [FK_CONGVIEC_GIAIDOAN] FOREIGN KEY([MaGiaiDoan])
REFERENCES [dbo].[GIAIDOAN] ([MaGiaiDoan])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CONGVIEC] CHECK CONSTRAINT [FK_CONGVIEC_GIAIDOAN]
GO
ALTER TABLE [dbo].[CONGVIEC]  WITH CHECK ADD  CONSTRAINT [FK_CONGVIEC_TRUONGNHOM] FOREIGN KEY([TenNhom], [MaDA])
REFERENCES [dbo].[TRUONGNHOM] ([TenNhom], [MaDA])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CONGVIEC] CHECK CONSTRAINT [FK_CONGVIEC_TRUONGNHOM]
GO
ALTER TABLE [dbo].[CONGVIEC]  WITH CHECK ADD  CONSTRAINT [FK_TIENQUYET_CONGVIEC] FOREIGN KEY([CVTienQuyet])
REFERENCES [dbo].[CONGVIEC] ([MaCV])
GO
ALTER TABLE [dbo].[CONGVIEC] CHECK CONSTRAINT [FK_TIENQUYET_CONGVIEC]
GO
ALTER TABLE [dbo].[Cuonsach]  WITH CHECK ADD FOREIGN KEY([isbn])
REFERENCES [dbo].[Dausach] ([isbn])
GO
ALTER TABLE [dbo].[DangKy]  WITH CHECK ADD FOREIGN KEY([isbn])
REFERENCES [dbo].[Dausach] ([isbn])
GO
ALTER TABLE [dbo].[DangKy]  WITH CHECK ADD FOREIGN KEY([ma_DocGia])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[Dausach]  WITH CHECK ADD FOREIGN KEY([ma_tuasach])
REFERENCES [dbo].[Tuasach] ([ma_tuasach])
GO
ALTER TABLE [dbo].[DIEMDANH]  WITH CHECK ADD  CONSTRAINT [FK_DIEMDANH_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[DIEMDANH] CHECK CONSTRAINT [FK_DIEMDANH_NHANVIEN]
GO
ALTER TABLE [dbo].[DUAN]  WITH CHECK ADD  CONSTRAINT [FK_DUAN_NHANVIEN] FOREIGN KEY([MaPM])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[DUAN] CHECK CONSTRAINT [FK_DUAN_NHANVIEN]
GO
ALTER TABLE [dbo].[GIAIDOAN]  WITH CHECK ADD  CONSTRAINT [FK_GIAIDOAN_DUAN] FOREIGN KEY([MaDA])
REFERENCES [dbo].[DUAN] ([MaDA])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[GIAIDOAN] CHECK CONSTRAINT [FK_GIAIDOAN_DUAN]
GO
ALTER TABLE [dbo].[Muon]  WITH CHECK ADD FOREIGN KEY([isbn])
REFERENCES [dbo].[Dausach] ([isbn])
GO
ALTER TABLE [dbo].[Muon]  WITH CHECK ADD FOREIGN KEY([ma_cuonsach])
REFERENCES [dbo].[Cuonsach] ([ma_cuonsach])
GO
ALTER TABLE [dbo].[Muon]  WITH CHECK ADD FOREIGN KEY([ma_DocGia])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[NguoiLon]  WITH CHECK ADD FOREIGN KEY([ma_DocGia])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[NHIEMVU]  WITH CHECK ADD  CONSTRAINT [FK_NHIEMVU_CONGVIEC] FOREIGN KEY([MaCV])
REFERENCES [dbo].[CONGVIEC] ([MaCV])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[NHIEMVU] CHECK CONSTRAINT [FK_NHIEMVU_CONGVIEC]
GO
ALTER TABLE [dbo].[NHIEMVU]  WITH CHECK ADD  CONSTRAINT [FK_NHIEMVU_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[NHIEMVU] CHECK CONSTRAINT [FK_NHIEMVU_NHANVIEN]
GO
ALTER TABLE [dbo].[NHIEMVU]  WITH CHECK ADD  CONSTRAINT [FK_TIENQUYET_NHIEMVU] FOREIGN KEY([MaTienQuyet])
REFERENCES [dbo].[NHIEMVU] ([MaNhiemVu])
GO
ALTER TABLE [dbo].[NHIEMVU] CHECK CONSTRAINT [FK_TIENQUYET_NHIEMVU]
GO
ALTER TABLE [dbo].[NHOM]  WITH CHECK ADD  CONSTRAINT [FK_NHOM_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
GO
ALTER TABLE [dbo].[NHOM] CHECK CONSTRAINT [FK_NHOM_NHANVIEN]
GO
ALTER TABLE [dbo].[NHOM]  WITH CHECK ADD  CONSTRAINT [FK_NHOM_TRUONGNHOM] FOREIGN KEY([TenNhom], [MaDA])
REFERENCES [dbo].[TRUONGNHOM] ([TenNhom], [MaDA])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[NHOM] CHECK CONSTRAINT [FK_NHOM_TRUONGNHOM]
GO
ALTER TABLE [dbo].[QuaTrinhMuon]  WITH CHECK ADD FOREIGN KEY([ma_cuonsach])
REFERENCES [dbo].[Cuonsach] ([ma_cuonsach])
GO
ALTER TABLE [dbo].[QuaTrinhMuon]  WITH CHECK ADD FOREIGN KEY([ma_DocGia])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[QuaTrinhMuon]  WITH CHECK ADD FOREIGN KEY([isbn])
REFERENCES [dbo].[Dausach] ([isbn])
GO
ALTER TABLE [dbo].[Treem]  WITH CHECK ADD FOREIGN KEY([ma_DocGia])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[Treem]  WITH CHECK ADD FOREIGN KEY([ma_DocGia_nguoilon])
REFERENCES [dbo].[DocGia] ([ma_DocGia])
GO
ALTER TABLE [dbo].[TRUONGNHOM]  WITH CHECK ADD  CONSTRAINT [FK_TRUONGNHOM_DUAN] FOREIGN KEY([MaDA])
REFERENCES [dbo].[DUAN] ([MaDA])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[TRUONGNHOM] CHECK CONSTRAINT [FK_TRUONGNHOM_DUAN]
GO
ALTER TABLE [dbo].[TRUONGNHOM]  WITH CHECK ADD  CONSTRAINT [FK_TRUONGNHOM_NHANVIEN] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NHANVIEN] ([MaNV])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[TRUONGNHOM] CHECK CONSTRAINT [FK_TRUONGNHOM_NHANVIEN]
GO
ALTER TABLE [dbo].[CONGVIEC]  WITH CHECK ADD  CONSTRAINT [CHECK_TIENDOCV] CHECK  (([TienDo]<=(100) AND [TienDo]>=(0)))
GO
ALTER TABLE [dbo].[CONGVIEC] CHECK CONSTRAINT [CHECK_TIENDOCV]
GO
ALTER TABLE [dbo].[DUAN]  WITH CHECK ADD  CONSTRAINT [CHECK_TIENDODA] CHECK  (([TienDo]<=(100) AND [TienDo]>=(0)))
GO
ALTER TABLE [dbo].[DUAN] CHECK CONSTRAINT [CHECK_TIENDODA]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [CHECK_LEVELS] CHECK  ((NOT [levels] like '%[0-9_!@#$%^&*()<>?/|}{~:]%'))
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [CHECK_LEVELS]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [CHECK_MANV] CHECK  (([MANV] like 'NV%' AND CONVERT([int],substring([MANV],(3),(3)))>(0) AND CONVERT([int],substring([MANV],(3),(3)))<=(999)))
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [CHECK_MANV]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [CHECK_SDT] CHECK  ((NOT [SDT] like '[a-zA-Z_!@#$%^&*()<>?/|}{~:]%]'))
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [CHECK_SDT]
GO
ALTER TABLE [dbo].[NHANVIEN]  WITH CHECK ADD  CONSTRAINT [CHECK_TENNV] CHECK  ((NOT [Ten] like '%[0-9_!@#$%^&*()<>?/|}{~:]%'))
GO
ALTER TABLE [dbo].[NHANVIEN] CHECK CONSTRAINT [CHECK_TENNV]
GO
/****** Object:  StoredProcedure [dbo].[sp_addRoleUser]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROC ADD MEMBERS ROLE
CREATE   PROCEDURE [dbo].[sp_addRoleUser] @role VARCHAR(20), @user VARCHAR(20)
AS
BEGIN
  DECLARE @sqlString VARCHAR(1000)
  SET @sqlString = 'ALTER ROLE ' + @role +' ADD MEMBER ' + @user; 
  EXEC (@sqlString)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_capnhatDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Update thông tin dự án mới
CREATE   PROCEDURE [dbo].[sp_capnhatDuAn]
@mada INT, @tenda NVARCHAR(50), @tiendo REAL, @ngaykt DATE, @ngaybd DATE, @chiphi NVARCHAR(30), @trangthai NVARCHAR(30), @mapm VARCHAR(10)
AS
BEGIN
	UPDATE DUAN SET TenDA=@tenda, TienDo=@tiendo, NgayKT=@ngaykt, NgayBD=@ngaybd, ChiPhi=@chiphi, TrangThai=@trangthai, MaPM=@mapm WHERE MaDA=@mada
END
GO
/****** Object:  StoredProcedure [dbo].[sp_capnhatTimeTask]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Update Thời gian TimeTask
CREATE   PROCEDURE [dbo].[sp_capnhatTimeTask]
@mada INT, @giaidoan VARCHAR(20), @manv VARCHAR(10), @timetask INT
AS
BEGIN
	UPDATE UOCLUONG SET TimeTasks = @timetask  WHERE MaNV = @manv AND MaDA = @mada AND MaGiaiDoan = @giaidoan
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Check_PM_TeamLead_CEO]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Check temaleadd,PM,CEO
CREATE   PROCEDURE [dbo].[sp_Check_PM_TeamLead_CEO]
    @mataikhoan VARCHAR(20)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM NhanVien AS nv WHERE nv.MaNV = @mataikhoan and nv.ChucVu = 'CEO')
    BEGIN
        SELECT *
        FROM v_DSDuAn;
    END;
    ELSE
    BEGIN
        SELECT *
        FROM v_DSDuAn_PM_LEAD
        WHERE MaPM = @mataikhoan or MaLead = @mataikhoan;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_doiTruongNhomDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--Đổi trưởng nhóm trong nhóm
CREATE   PROCEDURE [dbo].[sp_doiTruongNhomDuAn]
@mada INT, @tennhom NVARCHAR(20), @truongnhommoi VARCHAR(10)
AS
BEGIN
	UPDATE TRUONGNHOM SET MaNV=@truongnhommoi WHERE MaDA = @mada AND TenNhom = @tennhom
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dropRoleUser]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--####Procedure####
--PROC DROP MEMBERS ROLE
CREATE   PROCEDURE [dbo].[sp_dropRoleUser] @role VARCHAR(20), @user VARCHAR(20)
AS
BEGIN
  DECLARE @sqlString VARCHAR(1000)
  SET @sqlString = 'ALTER ROLE ' + @role +' DROP MEMBER ' + @user; 
  EXEC (@sqlString)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dstvmotnhomtrongmotduan]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Xem danh sách thành viên trong 1 dự án trong 1 nhóm *
CREATE   PROCEDURE [dbo].[sp_dstvmotnhomtrongmotduan]
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
/****** Object:  StoredProcedure [dbo].[sp_KiemTraCongViec]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Kiểm Tra Công Việc Tiên Quyết để xoá đi nó cần phải cập nhật công việc có
--mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu*
CREATE   PROCEDURE [dbo].[sp_KiemTraCongViec]
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
/****** Object:  StoredProcedure [dbo].[sp_KiemTraGiaiDoanTruoc]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Kiểm Tra giai đoạn trước đã có công việc trước khi tạo giai đoạn mới*
CREATE   PROCEDURE [dbo].[sp_KiemTraGiaiDoanTruoc]
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
/****** Object:  StoredProcedure [dbo].[sp_KiemTraNhiemVu]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Kiểm Tra Nhiệm Vụ Tiên Quyết để xoá đi nó cần phải cập nhật Nhiệm Vụ
--có mã tiên quyết tham chiếu dến nó và set null cho mã tham chiếu *
CREATE   PROCEDURE [dbo].[sp_KiemTraNhiemVu]
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
/****** Object:  StoredProcedure [dbo].[sp_KiemTraNhiemVuTienQuyet]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Kiểm Tra Trạng Thái Nhiệm Vụ Tiên Quyết đã hoàn thành chưa thì mới làm nhiệm vụ hiện tại*
Create   Procedure [dbo].[sp_KiemTraNhiemVuTienQuyet]
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
/****** Object:  StoredProcedure [dbo].[sp_ktrDangNhap]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Procedure Đăng Nhập kiểm tra có tồn tại tài khoản không *
CREATE   PROCEDURE [dbo].[sp_ktrDangNhap]
@matk VARCHAR(20), @matkhau VARCHAR(20), @check INT OUTPUT
AS
BEGIN
	SELECT @check=COUNT(*) FROM NHANVIEN
	WHERE MaTaiKhoan LIKE @matk AND MatKhau LIKE @matkhau
END
GO
/****** Object:  StoredProcedure [dbo].[sp_themDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert dự án
CREATE   PROCEDURE [dbo].[sp_themDuAn]
@tenda NVARCHAR(50), @tiendo REAL, @ngaykt DATE, @ngaybd DATE, @chiphi NVARCHAR(30), @trangthai NVARCHAR(30), @mapm VARCHAR(10)
AS
BEGIN
	INSERT INTO DUAN(TenDA, TienDo, NgayKT, NgayBD, ChiPhi, TrangThai, MaPM)
	VALUES(@tenda, @tiendo, @ngaykt, @ngaybd, @chiphi, @trangthai, @mapm)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_themNgayNghi]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert thông tin điểm danh nghỉ
CREATE   PROCEDURE [dbo].[sp_themNgayNghi]
@ngay DATE, @manv VARCHAR(10), @noidungnghi NVARCHAR(20)
AS
BEGIN
	INSERT INTO DIEMDANH VALUES(@ngay, @manv, @noidungnghi)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_themTaiNguyen]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert tài nguyên cần thiết vào dự án
CREATE   PROCEDURE [dbo].[sp_themTaiNguyen]
@mada INT, @matn VARCHAR(10)
AS
BEGIN
	INSERT INTO CAP VALUES (@mada, @matn)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TinhTienDoCV]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDURE CẬP nhật tiến độ công việc dựa trên (tổng số lượng nhiệm vụ hoàn thành x100)/tổng số lượng
--nhiệm vụ trong 1 giai đoạn *
CREATE   PROCEDURE [dbo].[sp_TinhTienDoCV]
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
/****** Object:  StoredProcedure [dbo].[sp_TinhTienDoDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Tính Tiến Độ Dự Án trong 1 sprint bằng tổng số  công việc thuộc 1 sprint trong 1 dự án x100 /tổng số công việc trong 1 dự án thuộc spirnt đó 
 Create   Procedure [dbo].[sp_TinhTienDoDuAn]
 @mada int,@magiaidoan varchar(10),@ketqua REAL OUTPUT
 as
 begin
	--Tìm tổng số công việc trong 1 sprint
	declare @tongsocongviec int 
	select @tongsocongviec=COUNT(CONGVIEC.MaCV) 
	From DUAN join CONGVIEC on CONGVIEC.MaDA=DUAN.MaDA
	join GIAIDOAN on GIAIDOAN.MaGiaiDoan=CONGVIEC.MaGiaiDoan
	where DUAN.MaDA=@mada and GIAIDOAN.MaGiaiDoan=@magiaidoan

	--Tìm tổng số công việc hoàn thành trong 1 sprint
	declare @tongsocvhoanthanh int 
	select @tongsocvhoanthanh=COUNT(CONGVIEC.MaCV) 
	From DUAN join CONGVIEC on CONGVIEC.MaDA=DUAN.MaDA
	join GIAIDOAN on GIAIDOAN.MaGiaiDoan=CONGVIEC.MaGiaiDoan
	where DUAN.MaDA=@mada and GIAIDOAN.MaGiaiDoan=@magiaidoan
	and CONGVIEC.TrangThai='Done'
	if @tongsocvhoanthanh >0
		set @ketqua=(@tongsocvhoanthanh*100)/(@tongsocongviec)
	else
		set @ketqua=0
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePass]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Cập nhật mật khẩu mới
CREATE   PROC [dbo].[sp_UpdatePass]
@user VARCHAR(20), @newpass VARCHAR(20), @oldpass VARCHAR(20)
AS
BEGIN
	DECLARE @sqlStr VARCHAR(255)
	SET @sqlStr = 'ALTER LOGIN ' + @user + ' WITH PASSWORD = ''' + @newpass + ''' OLD_PASSWORD = ''' + @oldpass + ''''
	EXEC(@sqlStr)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTimeSprintTheoNgay]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Cập nhật timesprint theo từng ngày
CREATE   PROCEDURE [dbo].[sp_UpdateTimeSprintTheoNgay]
AS
BEGIN
    UPDATE UOCLUONG
    SET timesprint = timesprint - (SELECT SoGioMotNg FROM NHOM WHERE UOCLUONG.MaNV = NHOM.MaNV AND UOCLUONG.MaDA = NHOM.MaDA)
	WHERE UOCLUONG.MaGiaiDoan IN (SELECT MaGiaiDoan FROM GIAIDOAN 
                                   WHERE GETDATE() BETWEEN GIAIDOAN.NgayBD AND GIAIDOAN.NgayKT)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTrangThai]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Procedure Cập nhật trạng thái dựa trên tiến độ vừa được cập nhật ở trên*

CREATE   Procedure [dbo].[sp_UpdateTrangThai]
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
/****** Object:  StoredProcedure [dbo].[sp_xoaDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Delete dự án
CREATE   PROCEDURE [dbo].[sp_xoaDuAn]
@mada INT
AS
BEGIN
	DELETE FROM DUAN WHERE MaDA=@mada
END
GO
/****** Object:  StoredProcedure [dbo].[sp_xoaNhanVienDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Delete nhân viên khỏi dự án
CREATE   PROCEDURE [dbo].[sp_xoaNhanVienDuAn]
@mada INT, @tennhom NVARCHAR(20), @manv VARCHAR(10)
AS
BEGIN
	DELETE FROM NHOM WHERE MaNV=@manv AND MaDA=@mada AND TenNhom=@tennhom
END
GO
/****** Object:  StoredProcedure [dbo].[sp_xoaNhomDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Delete nhóm trong dự án
CREATE   PROCEDURE [dbo].[sp_xoaNhomDuAn]
@mada INT, @tennhom NVARCHAR(20)
AS
BEGIN
	DELETE FROM TRUONGNHOM WHERE MaDA=@mada AND TenNhom=@tennhom
END
GO
/****** Object:  StoredProcedure [dbo].[sp_xoaTK]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Xóa tài khoản login và user
CREATE   PROC [dbo].[sp_xoaTK]
@mataikhoan VARCHAR(20)
AS
BEGIN
	DECLARE @sqlStr NVARCHAR(100)
    SET @sqlStr = 'DROP USER [' + @mataikhoan + ']'
    EXEC (@sqlStr)

	SET @sqlStr = 'DROP LOGIN [' + @mataikhoan + ']'
    EXEC (@sqlStr)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_xoaTruongNhomDuAn]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Delete trưởng nhóm trong nhóm
CREATE   PROCEDURE [dbo].[sp_xoaTruongNhomDuAn]
@mada INT, @tennhom NVARCHAR(20), @matn VARCHAR(20)
AS
BEGIN
	UPDATE TRUONGNHOM SET MaNV=NULL WHERE MaDA = @mada AND TenNhom = @tennhom
END
GO
/****** Object:  StoredProcedure [dbo].[sp_XoaUocLuong_GD_DA]    Script Date: 11/23/2023 8:48:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--﻿Procedure khi xoá giai đoạn buộc phải xoá tất cả nhân vẫn đang làm tại dự án và giai đoạn đó ở bảng ước lượng
Create   Procedure [dbo].[sp_XoaUocLuong_GD_DA]
@magd varchar(10),@mada int
as
begin
	Delete From UocLuong
	where UOCLUONG.MaGiaiDoan=@magd and UOCLUONG.MaDA=@mada
end
GO
USE [master]
GO
ALTER DATABASE [QLDA] SET  READ_WRITE 
GO
