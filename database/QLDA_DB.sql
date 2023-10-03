CREATE DATABASE QLDA;
GO

USE QLDA;
GO
Create Table NHIEMVU
( 
  MaNhiemVu  varchar(10) primary key,
  MaTienQuyet varchar(10) ,
  TrangThai nvarchar(30) not null,
  ThoiGianLamThucTe time not null,
  TenNhiemVu nvarchar not null,
  ThoiGianUocTinh time not null,
  MaNV varchar(10),
  MaCV varchar(10),
)
Create TABLE SPRINT 
(
	MaSprint varchar(10) primary key,
	NoiDung nvarchar(30),
	NgayBD date ,
	NgayKT date,
	MaDA varchar(10)
)
Create Table TAINGUYEN 
(
	MaTN varchar(10) primary key,
	TenTN nvarchar(20) not null,
	LoaiTaiNguyen nvarchar(20) not null,
)
CREATE TABLE CAP
(
	MaDA varchar(10),
	MaTN varchar(10),
	primary key(MaDA,MaTN)
)