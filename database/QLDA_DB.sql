CREATE DATABASE QLDA;
GO

USE QLDA;
GO

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