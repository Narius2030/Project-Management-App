--DROP DATABASE QLDA

CREATE DATABASE QLDA;
GO

USE QLDA;
GO
--Định nghĩa 1 kiểu dữ liệu Ma dùng chung
EXEC sp_addtype 'Ma','varchar(10)'
go
CREATE TABLE TAINGUYEN (
	MaTN Ma PRIMARY KEY,
	TenTN NVARCHAR(20),
	LoaiTaiNguyen NVARCHAR(20),
);

GO

CREATE TABLE NHANVIEN (
	MaNV Ma  PRIMARY KEY,
	HovaTenDem nvarchar(25) ,
	Ten nvarchar(25),
	ChucVu nvarchar(20) DEFAULT 'Member',
	Email varchar(25),
	Levels varchar(10),
	DiaChi nvarchar(50),
	SDT varchar(10),
	MaTaiKhoan varchar(20) UNIQUE,
	MatKhau varchar(20),
);

GO 
CREATE TABLE DUAN(
   MaDA INT IDENTITY PRIMARY KEY,
   TenDA NVARCHAR(50),
   TienDo REAL,
   NgayKT DATE,
   NgayBD DATE,
   ChiPhi VARCHAR(30),
   TrangThai NVARCHAR(30),
   MaPM Ma ,
   CONSTRAINT FK_DUAN_NHANVIEN FOREIGN KEY(MaPM) REFERENCES NHANVIEN(MaNV)
);

GO

CREATE TABLE CAP (
	MaDA INT,
	MaTN Ma ,
	PRIMARY KEY(MaDA,MaTN),
	constraint FK_CAP_DUAN FOREIGN KEY(MaDA) references DUAN(MaDA) ON UPDATE CASCADE,
	constraint FK_CAP_TAINGUYEN FOREIGN KEY (MaTN) references TAINGUYEN(MaTN) ON UPDATE CASCADE
)
GO

CREATE TABLE GIAIDOAN (
	MaGiaiDoan Varchar(20) PRIMARY KEY,
	NoiDung NVARCHAR(30),
	NgayBD DATE ,
	NgayKT DATE,
	MaDA INT,
	constraint FK_GIAIDOAN_DUAN FOREIGN KEY (MaDA) references DUAN(MaDA) ON DELETE SET NULL ON UPDATE CASCADE
)
GO

CREATE TABLE DIEMDANH(
   Ngay Date,
   MaNV Ma ,
   PRIMARY KEY(Ngay, MaNV),
   NoiDung NVARCHAR(20),
   CONSTRAINT FK_DIEMDANH_NHANVIEN FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)  ON UPDATE CASCADE
);

GO
	
CREATE TABLE TRUONGNHOM (
	TenNhom nvarchar(20) ,
	MaDA INT,
	MaNV Ma ,
	PRIMARY KEY(TenNhom, MaDA),
	CONSTRAINT FK_TRUONGNHOM_DUAN FOREIGN KEY(MaDA) REFERENCES DUAN(MaDA)  ON UPDATE CASCADE,
	CONSTRAINT FK_TRUONGNHOM_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV) ON DELETE set null ON UPDATE CASCADE
);

GO

CREATE TABLE CONGVIEC(
   MaCV INT IDENTITY(1, 1) PRIMARY KEY,
   TrangThai NVARCHAR(30) ,
   CVTienQuyet INT DEFAULT NULL,
   TenCV NVARCHAR(30) ,
   TienDo REAL,
   TenNhom NVARCHAR(20),
   MaDA INT,
   MaGiaiDoan Varchar(20),
   CONSTRAINT FK_CONGVIEC_TRUONGNHOM FOREIGN KEY (TenNhom, MaDA) REFERENCES TRUONGNHOM(TenNhom,MaDA)  ON DELETE SET NULL,
   CONSTRAINT FK_CONGVIEC_GIAIDOAN FOREIGN KEY (MaGiaiDoan) REFERENCES GIAIDOAN(MaGiaiDoan) ON DELETE SET NULL ON UPDATE CASCADE,
   CONSTRAINT FK_TIENQUYET_CONGVIEC FOREIGN KEY (CVTienQuyet) REFERENCES CONGVIEC(MaCV)
);
GO

GO

CREATE TABLE NHIEMVU
( 
  MaNhiemVu VARCHAR(30)  PRIMARY KEY,
  MaTienQuyet VARCHAR(30)  DEFAULT NULL,
  TrangThai NVARCHAR(30),
  ThoiGianLamThucTe INT,
  TenNhiemVu NVARCHAR(30),
  ThoiGianUocTinh int,
  MaNV Ma ,
  MaCV INT,
  constraint FK_NHIEMVU_NHANVIEN FOREIGN KEY (MaNV) references NHANVIEN(MaNV)  ON DELETE SET NULL ON UPDATE CASCADE,
  constraint FK_NHIEMVU_CONGVIEC FOREIGN KEY (MaCV) references CONGVIEC(MaCV) ON DELETE SET NULL ON UPDATE CASCADE,
  constraint FK_TIENQUYET_NHIEMVU FOREIGN KEY (MaTienQuyet) references NHIEMVU(MaNhiemVu) 
)
GO
CREATE TABLE NHOM (
	MaNV Ma ,
	TenNhom nvarchar(20),
	MaDA INT,
	SoGioMotNg INT,
	PRIMARY KEY(TenNhom, MaDA, MaNV),
	CONSTRAINT FK_NHOM_TRUONGNHOM FOREIGN KEY(TenNhom, MaDA) REFERENCES TRUONGNHOM(TenNhom, MaDA)  ON UPDATE CASCADE,
	CONSTRAINT FK_NHOM_NHANVIEN FOREIGN KEY(MaNV) REFERENCES NHANVIEN(MaNV)
);
GO
CREATE TABLE UOCLUONG(
   MaNV Ma  not null ,
   MaDA INT  not null ,
   MaGiaiDoan Varchar(20) not null,
   SoNgayNghi INT,
   TimeSprint INT,
   TimeTasks INT,
    PRIMARY KEY(MaNV, MaDA, MaGiaiDoan),
);

--###Views
--Xem danh sách nhân viên là trưởng nhóm trong các dự án
GO

CREATE OR ALTER VIEW vw_danhsach_truongnhom
AS
SELECT 
	TN.*, CONCAT(NV.HovaTenDem, ' ', NV.Ten) HoTen, NV.ChucVu, NV.Levels, N.SoGioMotNg
FROM TRUONGNHOM TN
INNER JOIN NHOM N ON N.MaNV = TN.MaNV AND N.TenNhom=TN.TenNhom and N.MaDA=TN.MaDA
INNER JOIN NHANVIEN NV ON NV.MaNV = TN.MaNV
GO
--Xem danh sách số lượng công việc chưa hoàn thành
CREATE OR ALTER VIEW vw_congviec_chuahoanthanh
AS
SELECT DA.MaDA, GD.MaGiaiDoan, COUNT(CV.MaCV) AS [số lượng công việc]
FROM CongViec CV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan
INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
WHERE CV.TrangThai != 'Done'
GROUP BY DA.MaDA,GD.MaGiaiDoan
GO

--Xem danh sách nhiệm vụ trong một giai đoạn dự án
CREATE OR ALTER VIEW vw_nhiemvu_giaidoan_duan
AS
SELECT nvu.MaNV, nvu.MaNhiemVu, cv.MaCV, gd.MaDA, gd.MaGiaiDoan, nvu.ThoiGianUocTinh, nvu.TrangThai
FROM NHIEMVU nvu
join CONGVIEC cv on cv.MaCV=nvu.MaCV
--join DUAN da on cv.MaDA=da.MaDA
join GIAIDOAN gd on gd.magiaidoan=cv.MaGiaiDoan
GO

--Xem danh sách ngày nghỉ*
CREATE OR ALTER VIEW vw_ngaynghi_trong_duan
AS
SELECT 
		dd.*, n.TenNhom, n.SoGioMotNg, gd.MaDA, gd.MaGiaiDoan, gd.NgayBD, gd.NgayKT
FROM DIEMDANH dd
JOIN NHOM n ON n.MaNV = dd.MaNV
JOIN GIAIDOAN gd ON gd.MaDA = n.MaDA
GO

--View liên quan đến nhiệm vụ của nhóm*
Create Or ALter View v_DanhSachNhiemVuNhom as 
SELECT 
	NV.MaNV, CV.MaDA,GD.MaGiaiDoan,CV.MaCV,N.TenNhom,NV.MaNhiemVu , TenNhiemVu , NV.TrangThai , MaTienQuyet, NV.ThoiGianUocTinh, NV.ThoiGianLamThucTe 
FROM NHIEMVU NV
INNER JOIN NHOM N ON N.MaNV = NV.MaNV 
INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan and GD.MaDA=CV.MaDA
and GD.MaDA=N.MaDA
GO




--Hướng Làm Của Quang Huy

--Danh sách dự án cho pm
Create or Alter View v_DSDuAn_PM_LEAD
AS
SELECT da.MaDA, da.TenDA, da.TienDo, da.NgayBD, da.NgayKT, da.ChiPhi, da.TrangThai, da.MaPM, tn.MaNV as MaLead
FROM DUAN da
LEFT JOIN TRUONGNHOM tn
ON da.MaDA = tn.MaDA 
GO
--Danh sách toàn bộ dự án cho CEO phân công
Create or Alter View v_DSDuAn
as select *From DuAn
Go

--###Triggers

--1.Thêm mới thông tin trong bảng UOCLUONG (insert) khi thêm một nhân viên mới vào nhóm trong một dự án*
create or alter trigger tr_addUocLuong on NHOM
AFTER INSERT AS
DECLARE @manv VARCHAR(10), @magd VARCHAR(10), @mada INT
SELECT @manv=i.MaNV, @mada=i.MaDA
FROM inserted i 
BEGIN
	if not exists(select * from UOCLUONG ul 
		where ul.MaNV = @manv AND ul.MaDA = @mada AND ul.MaGiaiDoan = (SELECT TOP 1 MaGiaiDoan FROM GIAIDOAN WHERE GIAIDOAN.MaDA = @mada ORDER BY MaGiaiDoan DESC))
		--Nếu nhân viên ko tồn tại trong giai đoạn mới nhất (đang làm việc) tại dự án đó thì tạo mới 1 hàng UOCLUONG
		insert into UOCLUONG
		select i.MaNV, i.MaDA, GIAIDOAN.MaGiaiDoan, 0, 0, 0 
		from inserted AS i
			join GIAIDOAN on i.MaDA= GIAIDOAN.MaDA
		where GIAIDOAN.MaGiaiDoan = (SELECT TOP 1 MaGiaiDoan FROM GIAIDOAN WHERE GIAIDOAN.MaDA = i.MaDA ORDER BY MaGiaiDoan DESC)
END;
GO
--10) Xử lý ràng buộc trước khi xóa DUAN
CREATE OR ALTER TRIGGER tr_rangbuoc_xoaDA ON DUAN
INSTEAD OF DELETE
AS
DECLARE @mada INT
SELECT @mada=old.MaDA
FROM deleted old
JOIN DUAN ON DUAN.MaDA = old.MaDA
--IF (@mada IS NOT NULL)
BEGIN
	--Xóa TEAM, CAP, UOCLUONG và TEAMLEADER có cùn MaDA trước
	DELETE FROM NHOM WHERE MaDA = @mada
	DELETE FROM TRUONGNHOM WHERE MaDA = @mada
	DELETE FROM CAP WHERE MaDA = @mada
	DELETE FROM UOCLUONG WHERE MaDA = @mada
	--Xóa DUAN
	DELETE FROM DUAN WHERE MaDA = @mada
END
GO

----4/ Time Task > Time Sprint thì hủy phân công ***
--CREATE OR ALTER TRIGGER tr_sosanh_thoigian ON UOCLUONG
--FOR UPDATE
--AS
--DECLARE @timetask INT, @timesprint INT
--SELECT @timetask=new.TimeTasks, @timetask=new.TimeSprint
--FROM inserted new, UOCLUONG ul
--WHERE new.MaNV = ul.MaNV AND new.MaDA = ul.MaDA AND new.MaGiaiDoan = ul.MaGiaiDoan
--IF (@timetask > @timesprint)
--BEGIN
--	RAISERROR('Lỗi Time Task > Time Sprint', 16, 1)
--	ROLLBACK TRAN;
--END
--GO

--5.Trigger kiểm tra nếu nhân viên nghỉ đúng thời gian Sprint nào thì cộng SoNgayNghi Sprint của nhân viên đó lên 1*
--NOTE
CREATE OR ALTER TRIGGER tr_ktr_ngaynghi_giaidoan
ON DIEMDANH
AFTER INSERT
AS
BEGIN
	DECLARE @MaNV VARCHAR(10);
	DECLARE @NgayNghi DATE;

	SELECT @NgayNghi = i.Ngay, @MaNV = i.MaNV
	FROM inserted i;
	BEGIN
		UPDATE UOCLUONG
		SET SoNgayNghi = SoNgayNghi + 1
		WHERE @MaNV = UOCLUONG.MaNV AND UOCLUONG.MaGiaiDoan IN (
			SELECT MaGiaiDoan
			FROM GIAIDOAN
			WHERE @NgayNghi <= GIAIDOAN.NgayKT AND @NgayNghi >= GIAIDOAN.NgayBD
		)
	END
END;
GO

--6. Tạo uocluong mới cho từng nhanvien trong duan theo giaidoan mới tạo*
CREATE OR ALTER TRIGGER tr_themUocLuong ON GIAIDOAN
AFTER INSERT
AS
DECLARE @manv VARCHAR(10), @magd VARCHAR(10), @mada INT
SELECT @mada=i.MaDA, @magd=i.MaGiaiDoan
FROM inserted i 
BEGIN
	DECLARE cursor_nhomDA CURSOR
	FOR SELECT DISTINCT MaNV FROM NHOM WHERE MaDA=@mada
	
	OPEN cursor_nhomDA
	FETCH NEXT FROM cursor_nhomDA INTO @manv
	WHILE @@FETCH_STATUS = 0
	BEGIN
		insert into UOCLUONG VALUES(@manv, @mada, @magd, 0, 0, 0)
		FETCH NEXT FROM cursor_nhomDA INTO @manv
	END
	CLOSE cursor_nhomDA;
	DEALLOCATE cursor_nhomDA
END
GO




--8. Kiểm tra nhân viên trong cùng dự án có cùng Số giờ làm một ngày***
CREATE OR ALTER TRIGGER tr_ktr_soGioMotNg ON NHOM
AFTER INSERT
AS
DECLARE @manv VARCHAR(10), @soGioNg DECIMAL, @mada INT, @currentSoGioNg DECIMAL, @check INT
SELECT @manv=i.MaNV, @soGioNg=i.SoGioMotNg, @mada=i.MaDA
FROM inserted i
--Kiểm tra Số giờ làm việc trong một dự án của một nhân viên
SELECT @check=COUNT(*) FROM NHOM
WHERE MaNV=@manv AND MaDA=@mada
IF @check > 0
BEGIN
	DECLARE cursor_nhomDA CURSOR
	FOR SELECT SoGioMotNg FROM NHOM WHERE MaDA=@mada AND MaNV=@manv

	SET @check = 1
	OPEN cursor_nhomDA
	FETCH NEXT FROM cursor_nhomDA INTO @currentSoGioNg
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @currentSoGioNg != @soGioNg
			SET @check = 0
			BREAK;
	END
	CLOSE cursor_nhomDA
	DEALLOCATE cursor_nhomDA
	IF @check = 0
	BEGIN
		RAISERROR('Thời gian làm việc một ngày trong một dự án không hợp lệ', 16, 1)
		ROLLBACK
	END
END
GO

--Tạo tài khoản login và user trong database
CREATE OR ALTER TRIGGER tr_CreateLoginUser ON NHANVIEN
AFTER INSERT
AS
BEGIN
    DECLARE @mataikhoan VARCHAR(20), @matkhau VARCHAR(20), @chucvu VARCHAR(20)

    SELECT @mataikhoan = i.MaNV, @matkhau = i.MatKhau, @chucvu = i.ChucVu
    FROM inserted i

    -- Tạo Login database
    DECLARE @createLogin NVARCHAR(100)
    SET @createLogin = 'CREATE LOGIN [' + @mataikhoan + '] WITH PASSWORD=''' + @matkhau + ''''
    EXEC (@createLogin)

    -- Tạo User
    DECLARE @createUser NVARCHAR(100)
    SET @createUser = 'CREATE USER [' + @mataikhoan + '] FOR LOGIN [' + @mataikhoan + ']'
    EXEC (@createUser)

	--Gan quyen admin cho CEO
	IF (@chucvu = 'CEO') 
	BEGIN
		exec master ..sp_addsrvrolemember @mataikhoan, N'sysadmin'
	END
END
Go

--Xóa tài khoản login và user
CREATE OR ALTER PROC sp_xoaTK
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

CREATE OR ALTER TRIGGER tr_xoaNhanVien ON NHANVIEN
INSTEAD OF DELETE
AS
DECLARE @mataikhoan VARCHAR(20)
SELECT @mataikhoan=d.MaNV
FROM deleted d
BEGIN
	UPDATE DUAN SET MaPM=NULL WHERE MaPM=@mataikhoan
	DELETE FROM NHOM WHERE MaNV=@mataikhoan
	UPDATE TRUONGNHOM SET MaNV=NULL WHERE MaNV=@mataikhoan
	DELETE FROM DIEMDANH WHERE MaNV=@mataikhoan
	DELETE FROM UOCLUONG WHERE MaNV=@mataikhoan
	DELETE FROM NHANVIEN WHERE MaNV=@mataikhoan
	EXEC sp_xoaTK @mataikhoan
END
GO

--###Constraints CHECK
-- câu 1: check tiến độ công việc và tiến độ dự án
ALTER TABLE CONGVIEC ADD CONSTRAINT CHECK_TIENDOCV CHECK (TienDo<=100 and TienDo>=0)
ALTER TABLE DUAN ADD CONSTRAINT CHECK_TIENDODA CHECK (TienDo <=100 and TienDo>=0)

--câu 2: check Tên nhân viên và levels không chứa ký tự đặc biệt và số; SDT không chứa ký tự chữ cái

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_TENNV CHECK(Ten NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT  CHECK_LEVELS CHECK(levels NOT LIKE '%[0-9_!@#$%^&*()<>?/|}{~:]%')
ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_SDT CHECK(SDT not LIKE '[a-zA-Z_!@#$%^&*()<>?/|}{~:]%]');
--câu 3: Mã nhân viên viết theo công thức: 2 ký tự đầu là “NV” + 3 ký tự số nguyên dương

ALTER TABLE NHANVIEN ADD CONSTRAINT CHECK_MANV CHECK (MANV LIKE 'NV%' AND CAST(SUBSTRING(MANV, 3, 3) AS INT) > 0 AND CAST(SUBSTRING(MANV, 3, 3) AS INT) <= 999);
GO

--####Procedure####
--PROC DROP MEMBERS ROLE
CREATE OR ALTER PROCEDURE sp_dropRoleUser @role VARCHAR(20), @user VARCHAR(20)
AS
BEGIN
  DECLARE @sqlString VARCHAR(1000)
  SET @sqlString = 'ALTER ROLE ' + @role +' DROP MEMBER ' + @user; 
  EXEC (@sqlString)
END
GO

--PROC ADD MEMBERS ROLE
CREATE OR ALTER PROCEDURE sp_addRoleUser @role VARCHAR(20), @user VARCHAR(20)
AS
BEGIN
  DECLARE @sqlString VARCHAR(1000)
  SET @sqlString = 'ALTER ROLE ' + @role +' ADD MEMBER ' + @user; 
  EXEC (@sqlString)
END
go
--Tính Tiến Độ Dự Án trong 1 sprint bằng tổng số  công việc thuộc 1 sprint trong 1 dự án x100 /tổng số công việc trong 1 dự án thuộc spirnt đó 
 Create Or Alter Procedure sp_TinhTienDoDuAn
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
Go

--Insert thông tin điểm danh nghỉ
CREATE OR ALTER PROCEDURE sp_themNgayNghi
@ngay DATE, @manv VARCHAR(10), @noidungnghi NVARCHAR(20)
AS
BEGIN
	INSERT INTO DIEMDANH VALUES(@ngay, @manv, @noidungnghi)
END
GO

--Insert dự án
CREATE OR ALTER PROCEDURE sp_themDuAn
@tenda NVARCHAR(50), @tiendo REAL, @ngaykt DATE, @ngaybd DATE, @chiphi NVARCHAR(30), @trangthai NVARCHAR(30), @mapm VARCHAR(10)
AS
BEGIN
	INSERT INTO DUAN(TenDA, TienDo, NgayKT, NgayBD, ChiPhi, TrangThai, MaPM)
	VALUES(@tenda, @tiendo, @ngaykt, @ngaybd, @chiphi, @trangthai, @mapm)
END
GO

--Update thông tin dự án mới
CREATE OR ALTER PROCEDURE sp_capnhatDuAn
@mada INT, @tenda NVARCHAR(50), @tiendo REAL, @ngaykt DATE, @ngaybd DATE, @chiphi NVARCHAR(30), @trangthai NVARCHAR(30), @mapm VARCHAR(10)
AS
BEGIN
	UPDATE DUAN SET TenDA=@tenda, TienDo=@tiendo, NgayKT=@ngaykt, NgayBD=@ngaybd, ChiPhi=@chiphi, TrangThai=@trangthai, MaPM=@mapm WHERE MaDA=@mada
END
GO

--Delete dự án
CREATE OR ALTER PROCEDURE sp_xoaDuAn
@mada INT
AS
BEGIN
	DELETE FROM DUAN WHERE MaDA=@mada
END
GO

--Delete nhân viên khỏi dự án
CREATE OR ALTER PROCEDURE sp_xoaNhanVienDuAn
@mada INT, @tennhom NVARCHAR(20), @manv VARCHAR(10)
AS
BEGIN
	DELETE FROM NHOM WHERE MaNV=@manv AND MaDA=@mada AND TenNhom=@tennhom
END
GO

--Delete nhóm trong dự án
CREATE OR ALTER PROCEDURE sp_xoaNhomDuAn
@mada INT, @tennhom NVARCHAR(20)
AS
BEGIN
	DELETE FROM TRUONGNHOM WHERE MaDA=@mada AND TenNhom=@tennhom
END
GO



--Đổi trưởng nhóm trong nhóm
CREATE OR ALTER PROCEDURE sp_doiTruongNhomDuAn
@mada INT, @tennhom NVARCHAR(20), @truongnhommoi VARCHAR(10)
AS
BEGIN
	UPDATE TRUONGNHOM SET MaNV=@truongnhommoi WHERE MaDA = @mada AND TenNhom = @tennhom
END
GO

--Cập nhật mật khẩu mới
CREATE OR ALTER PROC sp_UpdatePass
@user VARCHAR(20), @newpass VARCHAR(20), @oldpass VARCHAR(20)
AS
BEGIN
	DECLARE @sqlStr VARCHAR(255)
	SET @sqlStr = 'ALTER LOGIN ' + @user + ' WITH PASSWORD = ''' + @newpass + ''' OLD_PASSWORD = ''' + @oldpass + ''''
	EXEC(@sqlStr)
END
GO

--Update Thời gian TimeTask
CREATE OR ALTER PROCEDURE sp_capnhatTimeTask
@mada INT, @giaidoan VARCHAR(20), @manv VARCHAR(10), @timetask INT
AS
BEGIN
	UPDATE UOCLUONG SET TimeTasks = @timetask  WHERE MaNV = @manv AND MaDA = @mada AND MaGiaiDoan = @giaidoan
END
GO

--Insert tài nguyên cần thiết vào dự án
CREATE OR ALTER PROCEDURE sp_themTaiNguyen
@mada INT, @matn VARCHAR(10)
AS
BEGIN
	INSERT INTO CAP VALUES (@mada, @matn)
END
GO
--Check temaleadd,PM,CEO
CREATE OR ALTER PROCEDURE sp_Check_PM_TeamLead_CEO
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
Go
--﻿Procedure khi xoá giai đoạn buộc phải xoá tất cả nhân vẫn đang làm tại dự án và giai đoạn đó ở bảng ước lượng
Create Or Alter Procedure sp_XoaUocLuong_GD_DA
@magd varchar(10),@mada int
as
begin
	Delete From UocLuong
	where UOCLUONG.MaGiaiDoan=@magd and UOCLUONG.MaDA=@mada
end
Go

--Procedure Đăng Nhập kiểm tra có tồn tại tài khoản không *
CREATE OR ALTER PROCEDURE sp_ktrDangNhap
@matk VARCHAR(20), @matkhau VARCHAR(20), @check INT OUTPUT
AS
BEGIN
	SELECT @check=COUNT(*) FROM NHANVIEN
	WHERE MaTaiKhoan LIKE @matk AND MatKhau LIKE @matkhau
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

--Cập nhật timesprint theo từng ngày
CREATE OR ALTER PROCEDURE sp_UpdateTimeSprintTheoNgay
AS
BEGIN
    UPDATE UOCLUONG
    SET timesprint = timesprint - (SELECT SoGioMotNg FROM NHOM WHERE UOCLUONG.MaNV = NHOM.MaNV AND UOCLUONG.MaDA = NHOM.MaDA)
	WHERE UOCLUONG.MaGiaiDoan IN (SELECT MaGiaiDoan FROM GIAIDOAN 
                                   WHERE GETDATE() BETWEEN GIAIDOAN.NgayBD AND GIAIDOAN.NgayKT)
END
Go

--7. Xóa trưởng nhóm trong NHOM và TRUONGNHOM***
CREATE OR ALTER TRIGGER tr_xoaTruongNhom ON TRUONGNHOM
INSTEAD OF DELETE
AS
DECLARE @mada INT, @tennhom VARCHAR(20), @countTVNhom INT, @matn VARCHAR(10)
SELECT @mada=d.MaDA, @tennhom=d.TenNhom, @matn=d.MaNV
FROM deleted d
BEGIN
	DELETE FROM NHOM WHERE MaDA=@mada AND TenNhom=@tennhom AND MaNV=@matn
	--Lấy số lượng thành viên của nhóm trong dự án
	SELECT @countTVNhom=COUNT(*) FROM NHOM
	WHERE TenNhom=@tennhom AND MaDA=@mada
	PRINT @countTVNHOM
	--Nếu nhóm ko còn thành viên thì được xóa trưởng nhóm
	IF  @countTVNhom = 0
	BEGIN
		DELETE FROM TRUONGNHOM WHERE MaDA=@mada AND TenNhom=@tennhom
		--Thu hồi role Trưởng nhóm khi giải tán nhóm
		EXEC sp_dropRoleUser 'TEAMLEAD', @matn
	END
	ELSE
		RAISERROR('Nhóm này còn thành viên nên không được xóa trưởng nhóm', 16, 1)
END
GO

--Delete trưởng nhóm trong nhóm
CREATE OR ALTER PROCEDURE sp_xoaTruongNhomDuAn
@mada INT, @tennhom NVARCHAR(20), @matn VARCHAR(20)
AS
BEGIN
	UPDATE TRUONGNHOM SET MaNV=NULL WHERE MaDA = @mada AND TenNhom = @tennhom
END
GO

CREATE OR ALTER TRIGGER tr_AddMemberRolePM ON DUAN
AFTER INSERT,Update
AS
declare @user varchar(10)
select @user = MaPM FROM inserted
BEGIN
	Exec sp_addRoleUser 'PM',@user
END
GO
--add vai tro cua TeamLead
CREATE OR ALTER TRIGGER tr_AddMemberRoleLead ON TRUONGNHOM
AFTER INSERT,Update
AS
declare @user varchar(10)
select @user = MaNV FROM inserted
BEGIN
	Exec sp_addRoleUser 'TEAMLEAD',@user
END
GO
--Trigger xoa vai tro cua PM 
Create or Alter Trigger tg_RemoveRolePM
on DuAn after delete,Update
as 
begin
	declare @MaPM varchar(20)
	select @MaPM=d.MaPM From deleted  as d
	exec sp_dropRoleUser 'PM', @MAPM
end 
Go

--Thay the truong nhom va drop role truong nhom cu
Create or Alter Trigger tg_RemoveRoleTEAMLEAD
on TRUONGNHOM after update 
as
declare @matnOld varchar(20), @matnNew varchar(20)
select @matnNew=i.MaNV, @matnOld=d.MaNV
from inserted i, deleted d
where i.MaDA = d.MaDA and i.TenNhom = d.TenNhom
if @matnNew != @matnOld or @matnNew is null
begin
	exec sp_dropRoleUser 'TEAMLEAD', @matnOld
end 
Go

CREATE ROLE PM
--Phan quyen tren ROLE PM
GRANT SELECT ON DUAN TO PM
GRANT SELECT, UPDATE ON NHANVIEN TO PM
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON GIAIDOAN TO PM
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON CONGVIEC TO PM
GRANT SELECT, REFERENCES ON NHIEMVU TO PM
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON NHOM TO PM
GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON TRUONGNHOM TO PM
GRANT SELECT, INSERT, REFERENCES ON UOCLUONG TO PM
GRANT SELECT, REFERENCES ON DIEMDANH TO PM
GRANT SELECT ON v_DanhSachNhiemVuNhom TO PM
GRANT SELECT ON vw_congviec_chuahoanthanh TO PM
GRANT SELECT ON vw_danhsach_truongnhom TO PM
GRANT SELECT ON vw_ngaynghi_trong_duan TO PM
Grant SELECT ON v_DSDuAn_PM_LEAD TO PM
--GRANT EXECUTE TO PM
GRANT SELECT TO PM
GRANT EXECUTE ON sp_Check_PM_TeamLead_CEO TO PM
GRANT EXECUTE ON sp_dstvmotnhomtrongmotduan TO PM
GRANT EXECUTE ON sp_KiemTraCongViec TO PM
GRANT EXECUTE ON sp_KiemTraGiaiDoanTruoc TO PM
GRANT EXECUTE ON sp_ktrDangNhap TO PM
GRANT EXECUTE ON sp_TinhTienDoCV TO PM
GRANT EXECUTE ON sp_TinhTienDoDuAn TO PM
GRANT EXECUTE ON sp_UpdateTimeSprintTheoNgay TO PM
GRANT EXECUTE ON sp_UpdateTrangThai TO PM
GRANT EXECUTE ON sp_xoaNhanVienDuAn TO PM
GRANT EXECUTE ON sp_xoaTruongNhomDuAn TO PM
GRANT EXECUTE ON sp_xoaNhomDuAn TO PM
GRANT EXECUTE ON sp_XoaUocLuong_GD_DA TO PM
GRANT EXECUTE ON sp_UpdatePass TO PM
GRANT EXECUTE ON CheckTonTaiNhomTruong TO PM
GRANT EXECUTE ON sfn_CapNhatTimeTask TO PM
GRANT EXECUTE ON sfn_SumTimeTask TO PM

--drop role TEAMLEAD
CREATE ROLE TEAMLEAD
--Gán Quyền xem cho bảng nhóm
GRANT SELECT , REFERENCES  ON  NHOM TO TEAMLEAD
--Gán Quyền  xem,chèn, cập nhật,xoá
GRANT SELECT ,INSERT,UPDATE ,DELETE,REFERENCES ON NHIEMVU TO TEAMLEAD
--Gán Quyền Xem
GRANT SELECT, UPDATE ON NHANVIEN TO TEAMLEAD
--Gán Quyền xem ,cập nhật
GRANT SELECT ,UPDATE, REFERENCES ON CONGVIEC TO TEAMLEAD
GRANT SELECT, REFERENCES ON TRUONGNHOM TO TEAMLEAD
GRANT SELECT, REFERENCES ON GIAIDOAN TO TEAMLEAD
--GÁN  QUYỀN XEM 
GRANT SELECT ON  DUAN TO TEAMLEAD
--GÁN QUYỀN XEM VÀ INSERT
GRANT SELECT ,UPDATE ON DIEMDANH TO TEAMLEAD
--GÁN QUYỀN XEM VÀ CẬP NHẬT
GRANT SELECT ,UPDATE ON UOCLUONG TO TEAMLEAD
--GÁN QUYỀN XEM
GRANT SELECT ON GIAIDOAN TO TEAMLEAD
--GÁN QUYỀN THỰC THI ĐĂNG NHẬP
GRANT EXECUTE ON sp_ktrDangNhap TO TEAMLEAD
--GÁN QUYỀN THỰC THI KIỂM TRA NHIỆM VỤ
GRANT EXECUTE ON sp_KiemTraNhiemVu TO TEAMLEAD
--GÁN THỰC THI  TÍNH TIẾN ĐỘ CÔNG VIỆC 
GRANT EXECUTE ON  sp_TinhTienDoCV TO TEAMLEAD
--GÁN THỰC THI TÍNH TỔNG TIMETASK
GRANT EXECUTE ON  sfn_SumTimeTask TO TEAMLEAD
--GÁN THỰC THI CẬP NHẬT TIMETASK
GRANT EXECUTE ON  sfn_CapNhatTimeTask TO TEAMLEAD
--GÁN THỰC THI KIỂM TRA NHIẸM VỤ TIÊN QUYẾT
GRANT EXECUTE ON sp_KiemTraNhiemVuTienQuyet TO TEAMLEAD
--GÁN TÌM THỜI GIAN NGHỈ
GRANT EXECUTE ON sfn_TimThoiGianNghi TO TEAMLEAD
--GÁN TÌM CẬP NHẬT TIMESPRINT 
GRANT EXECUTE ON sfn_CapNhatTimeSprint TO TEAMLEAD
--GÁN CẬP NHẬT TIME TASK
GRANT EXECUTE ON sp_capnhatTimeTask TO TEAMLEAD
--GÁN CẬP NHẬT TIMESPRINT THEO NGÀY 
GRANT EXECUTE ON sfn_CapNhatTimeSprint TO TEAMLEAD
-- Gán thêm ngày nghỉ
GRANT EXECUTE ON sp_themNgayNghi TO TEAMLEAD
--Gán quyền xem danhsach dự án 
GRANT EXECUTE ON sp_Check_PM_TeamLead_CEO TO TEAMLEAD
--GÁN QUYỀN XEM DANH SÁCH NHÓM
GRANT SELECT ON sfn_TimTruongNhom TO TEAMLEAD
GRANT EXECUTE ON sp_dstvmotnhomtrongmotduan TO TEAMLEAD
GRANT EXECUTE ON sp_UpdatePass TO TEAMLEAD
GRANT SELECT ON  v_DSDuAn_PM_LEAD TO TEAMLEAD
GRANT SELECT ON vw_ngaynghi_trong_duan TO TEAMLEAD 
GRANT SELECT ON v_DanhSachNhiemVuNhom TO TEAMLEAD

--INSERT DATA
INSERT INTO NHANVIEN VALUES ('NV002', 'Nguyen Van', 'B', 'CEO' , 'nvb@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164476589', 'NV002', 'nv002');
INSERT INTO NHANVIEN VALUES ('NV003', 'Nguyen Thu', 'C', DEFAULT , 'ntc@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164348564', 'NV003', 'nv003');
INSERT INTO NHANVIEN VALUES ('NV004', 'Tran Van', 'D', DEFAULT , 'tvd@gmail.com', 'Intern', N'1, VVN, Thủ Đức', '0164714242', 'NV004', 'nv004');
INSERT INTO NHANVIEN VALUES ('NV005', 'Mai Van', 'E', DEFAULT , 'mve@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164111111', 'NV005', 'nv005');
INSERT INTO NHANVIEN VALUES ('NV006', 'Phan Thi', 'F', DEFAULT , 'ptf@gmail.com', 'Junier', N'1, VVN, Thủ Đức', '0164142142', 'NV006', 'nv006')
INSERT INTO NHANVIEN VALUES ('NV007', 'Trinh Van', 'G', DEFAULT , 'tvg@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164888645', 'NV007', 'nv007');
INSERT INTO NHANVIEN VALUES ('NV008', 'Phung Van', 'H', DEFAULT , 'pvh@gmail.com', 'Junier', N'1, VVN, Thủ Đức', '0164143231', 'NV008', 'nv008')
INSERT INTO NHANVIEN VALUES ('NV009', 'Nguyen Thanh', 'I', DEFAULT , 'nti@gmail.com', 'Fresher', N'1, VVN, Thủ Đức', '0164143257', 'NV009', 'nv009')
INSERT INTO NHANVIEN VALUES ('NV010', 'Nguyen Thanh', 'K', DEFAULT , 'ntk@gmail.com', 'Senior', N'1, VVN, Thủ Đức', '0164953438', 'NV010', 'nv010');
GO

INSERT INTO DUAN VALUES  (N'Phần mềm dạy học số', 0, '2023-12-30', '2023-10-15', '150000', 'Planning', 'NV003')
INSERT INTO DUAN VALUES (N'Phần mềm đặt vé tàu', 0, '2023-12-30', '2023-10-01', '150000', 'Requirement Analysis', 'NV010')
INSERT INTO DUAN VALUES  (N'Phần mềm xử lý ảnh', 30, '2023-12-30', '2023-09-01', '150000', 'Implementation', 'NV007')
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

