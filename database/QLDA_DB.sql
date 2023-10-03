CREATE DATABASE QLDA;
GO

USE QLDA;
GO

CREATE TABLE DUAN(
   MaDA VARCHAR(10) PRIMARY KEY,
   TenDA NVARCHAR(30) NOT NULL,
   TienDo REAL,
   NgayKT DATE,
   NgayBD DATE,
   ChiPhi VARCHAR(30),
   GiaiDoan NVARCHAR(30),
   MaNV VARCHAR(10),
);

GO

CREATE TABLE CONGVIEC(
   MaCV VARCHAR(10) PRIMARY KEY,
   TrangThai NVARCHAR(30) NOT NULL,
   CVTienQuyet VARCHAR(10),
   TenCV NVARCHAR(30) NOT NULL,
   TienDo REAL,
   TenNhom NVARCHAR(20),
   MaDA VARCHAR(10),
   MaSprint VARCHAR(10),
);

GO

CREATE TABLE DIEMDANH(
   Ngay Date,
   MaNV VARCHAR(10),
   PRIMARY KEY(Ngay, MaNV),
   NoiDung NVARCHAR(20)
);

GO

CREATE TABLE UOCLUONG(
   MaNV VARCHAR(10),
   MaDA VARCHAR(10),
   MaSprint VARCHAR(10),
   PRIMARY KEY(MaNV, MaDA, MaSprint),
   SoNgayNghi INT,
   TimeSprint INT,
   TimeTasks INT,
);

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

GO

Create TABLE SPRINT 
(
	MaSprint varchar(10) primary key,
	NoiDung nvarchar(30),
	NgayBD date ,
	NgayKT date,
	MaDA varchar(10)
)

GO

Create Table TAINGUYEN 
(
	MaTN varchar(10) primary key,
	TenTN nvarchar(20) not null,
	LoaiTaiNguyen nvarchar(20) not null,
)

GO

CREATE TABLE CAP
(
	MaDA varchar(10),
	MaTN varchar(10),
	primary key(MaDA,MaTN)
)
Go
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

alter table NHIEMVU add constraint PK_HIEMVU_NHANVIEN FOREIGN KEY (MaNV) references NHANVIEN(MaNV)
alter table NHIEMVU add constraint PK_NHIEMVU_CONGVIEC FOREIGN KEY (MaCV) references CONGVIEC(MaCV)
alter table SPRINT add constraint PK_SPRINT_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA)
alter table CAP add constraint PK_CAP_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA)
alter table CAP add constraint PK_CAP_TAINGUYEN FOREIGN KEY (MaTN) references TAINGUYEN(MaTN)