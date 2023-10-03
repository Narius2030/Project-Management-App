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

CREATE TABLE NHANVIEN (
	MaNV varchar(10) PRIMARY KEY,
	HovaTenDem nvarchar(25) NOT NULL,
	Ten nvarchar(25) NOT NULL,
	Email varchar(25),
	Levels varchar(10),
	DiaChi nvarchar(50),
	SDT varchar(20) NOT NULL,
);
GO

CREATE TABLE TAIKHOAN (
	UserNames varchar(25) NOT NULL,
	Passwords varchar(25) NOT NULL,
	MaNV varchar(10)
);
GO

CREATE TABLE TEAMLEADER (
	TenNhom nvarchar(20) NOT NULL,
	MaDA varchar(10),
	MaNV varchar(10)
);
GO

CREATE TABLE TEAM (
	MaNV varchar(10),
	TenNhom varchar(20),
	MaDA varchar(10),
	CapPerDay int NOT NULL
);
GO

-- Adding constraint
