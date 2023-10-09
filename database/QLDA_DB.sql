CREATE DATABASE QLDA;
GO

USE QLDA;
GO

CREATE TABLE UOCLUONG(
   MaNV VARCHAR(10),
   MaDA INT,
   MaSprint Varchar(20),
   PRIMARY KEY(MaNV, MaDA, MaSprint),
   SoNgayNghi INT,
   TimeSprint INT,
   TimeTasks INT,
);

GO

CREATE TABLE TAINGUYEN (
	MaTN VARCHAR(10) PRIMARY KEY,
	TenTN NVARCHAR(20),
	LoaiTaiNguyen NVARCHAR(20),
)

GO

CREATE TABLE NHANVIEN (
	MaNV varchar(10) PRIMARY KEY,
	HovaTenDem nvarchar(25) ,
	Ten nvarchar(25),
	ChucVu nvarchar(20) DEFAULT 'Member',
	Email varchar(25),
	Levels varchar(10),
	DiaChi nvarchar(50),
	SDT varchar(10),
);

GO
CREATE TABLE DUAN(
   MaDA INT IDENTITY PRIMARY KEY,
   TenDA NVARCHAR(50),
   TienDo REAL,
   NgayKT DATE,
   NgayBD DATE,
   ChiPhi VARCHAR(30),
   GiaiDoan NVARCHAR(30),
   MaPM VARCHAR(10),
   CONSTRAINT FK_DUAN_NHIEMVIEN FOREIGN KEY(MaPM) REFERENCES NHANVIEN(MaNV)
);

GO

CREATE TABLE CAP (
	MaDA INT,
	MaTN VARCHAR(10),
	PRIMARY KEY(MaDA,MaTN),
	constraint FK_CAP_DUAN FOREIGN KEY(MaDA) references DUAN(MaDA) ON UPDATE CASCADE,
	constraint FK_CAP_TAINGUYEN FOREIGN KEY (MaTN) references TAINGUYEN(MaTN) ON UPDATE CASCADE
)
GO

CREATE TABLE SPRINT (
	MaSprint Varchar(20) PRIMARY KEY,
	NoiDung NVARCHAR(30),
	NgayBD DATE ,
	NgayKT DATE,
	MaDA INT,
	constraint FK_SPRINT_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA) ON DELETE SET NULL ON UPDATE CASCADE
)
GO

CREATE TABLE DIEMDANH(
   Ngay Date,
   MaNV VARCHAR(10),
   PRIMARY KEY(Ngay, MaNV),
   NoiDung NVARCHAR(20),
   CONSTRAINT FK_DIEMDANH_NHIEMVIEN FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)  ON UPDATE CASCADE
);

GO

CREATE TABLE TAIKHOAN (
	UserNames varchar(25) ,
	Passwords varchar(25) ,
	MaNV varchar(10),
	CONSTRAINT FK_TAIKHOAN_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV)  ON DELETE SET NULL ON UPDATE CASCADE
);
GO

CREATE TABLE TEAMLEADER (
	TenNhom nvarchar(20) ,
	MaDA INT,
	MaNV varchar(10),
	PRIMARY KEY(TenNhom, MaDA),
	CONSTRAINT FK_TEAM_DUAN FOREIGN KEY(MaDA) REFERENCES DUAN(MaDA)  ON UPDATE CASCADE,
	CONSTRAINT FK_TEAMLEADER_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV) ON DELETE set null ON UPDATE CASCADE
);

GO

CREATE TABLE CONGVIEC(
   MaCV INT IDENTITY PRIMARY KEY,
   TrangThai NVARCHAR(30) ,
   CVTienQuyet INT DEFAULT NULL,
   TenCV NVARCHAR(30) ,
   TienDo REAL,
   TenNhom NVARCHAR(20),
   MaDA INT,
   MaSprint Varchar(20),
   CONSTRAINT FK_CONGVIEC_TEAMLEADER FOREIGN KEY (TenNhom, MaDA) REFERENCES TEAMLEADER(TenNhom,MaDA)  ON DELETE SET NULL,
   CONSTRAINT FK_CONGVIEC_SPRINT FOREIGN KEY (MaSprint) REFERENCES SPRINT(MaSprint) ON DELETE SET NULL ON UPDATE CASCADE,
   CONSTRAINT FK_TIENQUYET_CONGVIEC FOREIGN KEY (CVTienQuyet) REFERENCES CONGVIEC(MaCV) ON DELETE SET NULL
);
GO

GO

CREATE TABLE NHIEMVU
( 
  MaNhiemVu VARCHAR(10) PRIMARY KEY,
  MaTienQuyet VARCHAR(10) DEFAULT NULL,
  TrangThai NVARCHAR(30),
  ThoiGianLamThucTe INT,
  TenNhiemVu NVARCHAR(30),
  ThoiGianUocTinh int,
  MaNV VARCHAR(10),
  MaCV INT,
  constraint FK_NHIEMVU_NHANVIEN FOREIGN KEY (MaNV) references NHANVIEN(MaNV)  ON DELETE SET NULL ON UPDATE CASCADE,
  constraint FK_NHIEMVU_CONGVIEC FOREIGN KEY (MaCV) references CONGVIEC(MaCV) ON DELETE SET NULL ON UPDATE CASCADE,
  constraint FK_TIENQUYET_NHIEMVU FOREIGN KEY (MaTienQuyet) references NHIEMVU(MaNhiemVu) ON DELETE SET NULL
)

GO
CREATE TABLE TEAM (
	MaNV varchar(10),
	TenNhom nvarchar(20),
	MaDA INT,
	CapPerDay INT,
	PRIMARY KEY(TenNhom, MaDA, MaNV),
	CONSTRAINT FK_TEAM_TEAMLEADER FOREIGN KEY(TenNhom, MaDA) REFERENCES TEAMLEADER(TenNhom, MaDA)  ON UPDATE CASCADE,
	CONSTRAINT FK_TEAM_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV)
);
GO

-- Adding constraint

--ALTER TABLE TEAMLEADER ADD CONSTRAINT FK_TEAM_DUAN FOREIGN KEY(MaDA) REFERENCES DUAN(MaDA) ON UPDATE CASCADE ;
--ALTER TABLE TEAMLEADER ADD CONSTRAINT FK_TEAMLEADER_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV) ON DELETE set null ON UPDATE CASCADE ; 
--ALTER TABLE TEAM ADD CONSTRAINT FK_TEAM_TEAMLEADER FOREIGN KEY(TenNhom, MaDA) REFERENCES TEAMLEADER(TenNhom, MaDA)  ON UPDATE CASCADE ;
--ALTER TABLE TEAM ADD CONSTRAINT FK_TEAM_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV)   ; 
--ALTER TABLE TAIKHOAN ADD CONSTRAINT FK_TAIKHOAN_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV)  ON DELETE SET NULL ON UPDATE CASCADE ;

--alter table NHIEMVU add constraint PK_NHIEMVU_NHANVIEN FOREIGN KEY (MaNV) references NHANVIEN(MaNV)  ON DELETE SET NULL ON UPDATE CASCADE ;
--alter table NHIEMVU add constraint PK_NHIEMVU_CONGVIEC FOREIGN KEY (MaCV) references CONGVIEC(MaCV) ON DELETE SET NULL ON UPDATE CASCADE ;
--alter table SPRINT add constraint PK_SPRINT_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA)  ON DELETE SET NULL ON UPDATE CASCADE ;
--alter table CAP add constraint PK_CAP_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA) ON UPDATE CASCADE ;
--alter table CAP add constraint PK_CAP_TAINGUYEN FOREIGN KEY (MaTN) references TAINGUYEN(MaTN)   ON UPDATE CASCADE ;

--RÀNG BUỘC VỚI BẢNG NHÂN VIÊN
--ALTER TABLE DUAN ADD CONSTRAINT FK_DUAN_NHIEMVIEN FOREIGN KEY (MaPM) REFERENCES NHANVIEN(MaNV)  ;
--ALTER TABLE DIEMDANH ADD CONSTRAINT FK_DIEMDANH_NHIEMVIEN FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)  ON UPDATE CASCADE ;

--RÀNG BUỘC VỚI BẢNG TEAM
--ALTER TABLE CONGVIEC ADD CONSTRAINT FK_CONGVIEC_TEAMLEADER FOREIGN KEY (TenNhom, MaDA) REFERENCES TEAMLEADER(TenNhom,MaDA)  ON DELETE SET NULL ;

--RÀNG BUỘC VỚI SPRINT
--ALTER TABLE CONGVIEC ADD CONSTRAINT FK_CONGVIEC_SPRINT FOREIGN KEY (MaSprint) REFERENCES SPRINT(MaSprint) ON DELETE SET NULL ON UPDATE CASCADE ;


GO
--INSERT DATA

INSERT INTO NHANVIEN VALUES 
('NV001', 'Nguyen Van', 'A', 'CEO', 'nva@gmail.com', 'Junier', N'1, VVN, Thủ Đức', '0164785658'),
('NV002', 'Nguyen Van', 'B', DEFAULT , 'nvb@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164476589'),
('NV003', 'Nguyen Thu', 'C', DEFAULT , 'ntc@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164348564'),
('NV004', 'Tran Van', 'D', DEFAULT , 'tvd@gmail.com', 'Intern', N'1, VVN, Thủ Đức', '0164714242'),
('NV005', 'Mai Van', 'E', DEFAULT , 'mve@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164111111'),
('NV006', 'Phan Thi', 'F', DEFAULT , 'ptf@gmail.com', 'Junier', N'1, VVN, Thủ Đức', '0164142142'),
('NV007', 'Trinh Van', 'G', DEFAULT , 'tvg@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164888645'),
('NV008', 'Phung Van', 'H', DEFAULT , 'pvh@gmail.com', 'Junier', N'1, VVN, Thủ Đức', '0164143231'),
('NV009', 'Nguyen Thanh', 'I', DEFAULT , 'nti@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164143257'),
('NV010', 'Nguyen Thanh', 'K', DEFAULT , 'ntk@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164953438')

GO
INSERT INTO TAIKHOAN VALUES
('NV001', 'NV001', 'NV001'),
('NV002', 'NV002', 'NV002'),
('NV003', 'NV003', 'NV003'),
('NV004', 'NV004', 'NV004'),
('NV005', 'NV005', 'NV005'),
('NV006', 'NV006', 'NV006'),
('NV007', 'NV007', 'NV007'),
('NV008', 'NV008', 'NV008'),
('NV009', 'NV009', 'NV009'),
('NV010', 'NV010', 'NV010');

GO
INSERT INTO DUAN (TenDA, TienDo, NgayKT, NgayBD, ChiPhi, GiaiDoan, MaPM) VALUES 
(N'Phần mềm dạy học số', 0, '2023-12-30', '2023-10-15', '150000', 'Planning', 'NV002'),
(N'Phần mềm đặt vé tàu', 0, '2023-12-30', '2023-10-01', '150000', 'Requirement Analysis', 'NV010'),
(N'Phần mềm xử lý ảnh', 30, '2023-12-30', '2023-09-01', '150000', 'Implementation', 'NV007'),
(N'Phần mềm bán nước', 60, '2023-12-30', '2023-09-01', '150000', 'Testing', 'NV002'),
(N'Phần mềm kiểm soát dân cư', 80, '2023-12-30', '2023-08-01', '150000', 'Deployment', 'NV010'),
(N'Phần mềm ql giao thông đô thị', 90, '2023-12-30', '2023-08-15', '150000', 'Maintenance', 'NV007'),
(N'Phần mềm thanh toán trực tuyến', 95, '2023-12-30', '2023-08-15', '150000', 'Documentation', 'NV002'),
(N'Phần mềm chuyển đổi văn bản', 100, '2023-12-30', '2023-10-15', '150000', 'Done', 'NV002'),
(N'Phần mềm học tiếng anh', 0, '2023-12-30', '2023-10-15', '150000', 'Planning', 'NV010'),
(N'Phần mềm ql số liệu khí hậu', 0, '2023-12-30', '2023-10-15', '150000', 'Planning', 'NV007');
GO


INSERT INTO SPRINT (MaSprint, NoiDung, NgayKT, NgayBD, MaDA) VALUES
('01DA03', N'Database', '2023-10-30', '2023-09-01', 3),
('01DA04', N'Database', '2023-09-30', '2023-09-01', 4),
('02DA04', N'UI', '2023-10-30', '2023-10-01', 4),
('01DA05', N'Database', '2023-12-30', '2023-08-01', 5),
('01DA06', N'Database', '2023-09-30', '2023-09-01', 6),
('02DA06', N'Front-Back-End', '2023-12-30', '2023-10-01', 6),
('01DA07', N'Front-End', '2023-09-30', '2023-09-01', 7),
('02DA07', N'Back-End', '2023-12-30', '2023-10-01', 7),
('01DA08', N'Front-End', '2023-11-30', '2023-10-15', 8),
('02DA08', N'Back-End', '2023-12-30', '2023-12-01', 8);
GO

INSERT INTO UOCLUONG VALUES
('NV002', 8, '01DA08', 3, 24, 20),
('NV003', 8, '01DA08', 3, 24, 20),
('NV004', 8, '01DA08', 3, 24, 20),
('NV005', 8, '01DA08', 3, 24, 20),
('NV006', 7, '01DA07', 3, 24, 20),
('NV007', 7, '01DA07', 3, 24, 20),
('NV008', 7, '01DA07', 3, 24, 20),
('NV005', 6, '01DA06', 3, 24, 20),
('NV003', 6, '01DA06', 3, 24, 20),
('NV006', 6, '01DA06', 3, 24, 20),
('NV010', 5, '01DA05', 3, 24, 20),
('NV002', 5, '01DA05', 3, 24, 20),
('NV005', 5, '01DA05', 3, 24, 20),
('NV007', 4, '01DA04', 3, 24, 20),
('NV004', 4, '01DA04', 3, 24, 20),
('NV006', 4, '01DA04', 3, 24, 20),
('NV010', 3, '01DA03', 3, 24, 20),
('NV002', 3, '01DA03', 3, 24, 20),
('NV005', 3, '01DA03', 3, 24, 20);

GO

INSERT INTO TAINGUYEN VALUES
('1', 'IDE', 'Software'),
('2', 'Microsoft Office', 'Software'),
('3', 'Operating System', 'Software'),
('4', N'Dữ liệu khách hàng', 'Data'),
('5', N'Dữ liệu sản phẩm', 'Data'),
('6', N'Máy tính', 'Hardware'),
('7', N'Bàn làm việc', 'Hardware'),
('8', N'Máy in', 'Hardware'),
('9', N'Laptop', 'Hardware'),
('10', N'Cơ sở vật chất khác', 'Hardware');

GO

INSERT INTO CAP VALUES
(7, '1'),
(7, '2'),
(7, '3'),
(7, '4'),
(7, '5'),
(7, '6'),
(7, '7'),
(7, '8'),
(7, '10'),
(6, '1'),
(6, '2'),
(6, '5'),
(6, '8');

GO

INSERT INTO DIEMDANH VALUES
('2023-10-01', 'NV004', 'Nghi phep'),
('2023-10-02', 'NV002', 'Nghi phep'),
('2023-10-03', 'NV007', 'Nghi phep'),
('2023-10-04', 'NV006', 'Nghi phep'),
('2023-10-01', 'NV005', 'Nghi phep'),
('2023-10-08', 'NV005', 'Nghi phep'),
('2023-10-05', 'NV004', 'Nghi phep'),
('2023-10-01', 'NV009', 'Nghi phep'),
('2023-10-03', 'NV005', 'Nghi phep'),
('2023-10-01', 'NV008', 'Nghi phep');

GO

INSERT INTO TEAMLEADER VALUES
('Front-End', 8, 'NV002'),
('Back-End', 8, 'NV003'),
('Front-End', 7, 'NV006'),
('Back-End', 7, 'NV007'),
('Front-End', 6, 'NV003'),
('Back-End', 6, 'NV006'),
('Front-End', 5, 'NV010'),
('Back-End', 5, 'NV002'),
('Front-End', 4, 'NV007'),
('Back-End', 4, 'NV006'),
('Front-End', 3, 'NV010'),
('Back-End', 3, 'NV002');


INSERT INTO TEAM VALUES
('NV002', 'Front-End', 8, 6),
('NV003', 'Back-End', 8, 6),
('NV004', 'Front-End', 8, 8),
('NV005', 'Back-End', 8, 6),

('NV006', 'Front-End', 7, 6),
('NV007', 'Back-End', 7, 6),
('NV008', 'Back-End', 7, 6),

('NV005', 'Back-End', 6, 6),
('NV003', 'Front-End', 6, 6),
('NV006', 'Back-End', 6, 6),

('NV010', 'Front-End', 5, 6),
('NV002', 'Back-End', 5, 6),
('NV005', 'Back-End', 5, 6),

('NV007', 'Front-End', 4, 6),
('NV004', 'Back-End', 4, 8),
('NV006', 'Back-End', 4, 7),

('NV010', 'Front-End', 3, 6),
('NV002', 'Back-End', 3, 7),
('NV005', 'Back-End', 3, 7);


GO


INSERT INTO CONGVIEC (TrangThai, CVTienQuyet, TenCV, TienDo, TenNhom, MaDA, MaSprint) VALUES
('Done', DEFAULT , N'Giao diện đăng nhập', 100, 'Front-End', 8, '01DA08'),
('Done', DEFAULT , N'Chức năng', 100, 'Back-End', 8, '01DA08'),
('Done', DEFAULT , N'Giao diện đăng nhập', 100, 'Front-End', 7, '01DA07'),
('Done', DEFAULT , N'Chức năng', 100, 'Back-End', 7, '01DA07'),
('Doing', DEFAULT , N'Giao diện đăng nhập', 50, 'Front-End', 6, '01DA06'),
('Doing', DEFAULT , N'Chức năng', 30, 'Back-End', 6, '01DA06'),
('Pending', DEFAULT , N'Giao diện đăng nhập', 0, 'Front-End', 5, '01DA05'),
('Pending', DEFAULT , N'Chức năng', 1, 'Back-End', 5, '01DA05');

GO

INSERT INTO NHIEMVU VALUES
('01CV01', DEFAULT, 'Done', 5, N'Giao diện đăng nhập', 8, 'NV002', 1),
('01CV02', DEFAULT, 'Done', 5, N'Chức năng đăng nhập', 6, 'NV003', 2),
('01CV03', DEFAULT, 'Done', 5, N'Giao diện đăng nhập', 8, 'NV006', 3),
('01CV04', '01CV03', 'Pending', 5, N'Chức năng đăng nhập', 8, 'NV007', 4),
('01CV05', '01CV04', 'Pending', 5, N'Giao diện đăng nhập', 8, 'NV003', 5),
('01CV06', DEFAULT, 'Doing', 5, N'Chức năng đăng nhập', 8, 'NV006', 6),
('01CV07', DEFAULT, 'Pending', 5, N'Giao diện đăng nhập', 8, 'NV010', 7),
('01CV08', DEFAULT, 'Pending', 5, N'Chức năng đăng nhập', 8, 'NV002', 8);
GO

