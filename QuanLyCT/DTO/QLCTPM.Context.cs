﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCongTy.DTO
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QLDAEntities : DbContext
    {
        public QLDAEntities()
            : base("name=QLDAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONGVIEC> CONGVIECs { get; set; }
        public virtual DbSet<DIEMDANH> DIEMDANHs { get; set; }
        public virtual DbSet<DUAN> DUANs { get; set; }
        public virtual DbSet<GIAIDOAN> GIAIDOANs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NHIEMVU> NHIEMVUs { get; set; }
        public virtual DbSet<NHOM> NHOMs { get; set; }
        public virtual DbSet<TAINGUYEN> TAINGUYENs { get; set; }
        public virtual DbSet<TRUONGNHOM> TRUONGNHOMs { get; set; }
        public virtual DbSet<UOCLUONG> UOCLUONGs { get; set; }
        public virtual DbSet<v_DanhSachNhiemVuNhom> v_DanhSachNhiemVuNhom { get; set; }
        public virtual DbSet<vw_congviec_chuahoanthanh> vw_congviec_chuahoanthanh { get; set; }
        public virtual DbSet<vw_danhsach_truongnhom> vw_danhsach_truongnhom { get; set; }
        public virtual DbSet<vw_ngaynghi_trong_duan> vw_ngaynghi_trong_duan { get; set; }
        public virtual DbSet<vw_nhiemvu_giaidoan_duan> vw_nhiemvu_giaidoan_duan { get; set; }
    
        [DbFunction("QLDAEntities", "sfn_KiemTraGiaiDoan")]
        public virtual IQueryable<sfn_KiemTraGiaiDoan_Result> sfn_KiemTraGiaiDoan(Nullable<int> mada, string maGiaiDoan)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var maGiaiDoanParameter = maGiaiDoan != null ?
                new ObjectParameter("MaGiaiDoan", maGiaiDoan) :
                new ObjectParameter("MaGiaiDoan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<sfn_KiemTraGiaiDoan_Result>("[QLDAEntities].[sfn_KiemTraGiaiDoan](@mada, @MaGiaiDoan)", madaParameter, maGiaiDoanParameter);
        }
    
        [DbFunction("QLDAEntities", "sfn_TimTruongNhom")]
        public virtual IQueryable<sfn_TimTruongNhom_Result> sfn_TimTruongNhom(string tennhom, Nullable<int> mada)
        {
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<sfn_TimTruongNhom_Result>("[QLDAEntities].[sfn_TimTruongNhom](@tennhom, @mada)", tennhomParameter, madaParameter);
        }
    
        public virtual int sp_capnhatDuAn(Nullable<int> mada, string tenda, Nullable<float> tiendo, Nullable<System.DateTime> ngaykt, Nullable<System.DateTime> ngaybd, string chiphi, string trangthai, string mapm)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tendaParameter = tenda != null ?
                new ObjectParameter("tenda", tenda) :
                new ObjectParameter("tenda", typeof(string));
    
            var tiendoParameter = tiendo.HasValue ?
                new ObjectParameter("tiendo", tiendo) :
                new ObjectParameter("tiendo", typeof(float));
    
            var ngayktParameter = ngaykt.HasValue ?
                new ObjectParameter("ngaykt", ngaykt) :
                new ObjectParameter("ngaykt", typeof(System.DateTime));
    
            var ngaybdParameter = ngaybd.HasValue ?
                new ObjectParameter("ngaybd", ngaybd) :
                new ObjectParameter("ngaybd", typeof(System.DateTime));
    
            var chiphiParameter = chiphi != null ?
                new ObjectParameter("chiphi", chiphi) :
                new ObjectParameter("chiphi", typeof(string));
    
            var trangthaiParameter = trangthai != null ?
                new ObjectParameter("trangthai", trangthai) :
                new ObjectParameter("trangthai", typeof(string));
    
            var mapmParameter = mapm != null ?
                new ObjectParameter("mapm", mapm) :
                new ObjectParameter("mapm", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_capnhatDuAn", madaParameter, tendaParameter, tiendoParameter, ngayktParameter, ngaybdParameter, chiphiParameter, trangthaiParameter, mapmParameter);
        }
    
        public virtual int sp_capnhatTimeTask(Nullable<int> mada, string giaidoan, string manv, Nullable<int> timetask)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var giaidoanParameter = giaidoan != null ?
                new ObjectParameter("giaidoan", giaidoan) :
                new ObjectParameter("giaidoan", typeof(string));
    
            var manvParameter = manv != null ?
                new ObjectParameter("manv", manv) :
                new ObjectParameter("manv", typeof(string));
    
            var timetaskParameter = timetask.HasValue ?
                new ObjectParameter("timetask", timetask) :
                new ObjectParameter("timetask", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_capnhatTimeTask", madaParameter, giaidoanParameter, manvParameter, timetaskParameter);
        }
    
        public virtual int sp_doiTruongNhomDuAn(Nullable<int> mada, string tennhom, string truongnhommoi)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            var truongnhommoiParameter = truongnhommoi != null ?
                new ObjectParameter("truongnhommoi", truongnhommoi) :
                new ObjectParameter("truongnhommoi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_doiTruongNhomDuAn", madaParameter, tennhomParameter, truongnhommoiParameter);
        }
    
        public virtual ObjectResult<sp_dstvmotnhomtrongmotduan_Result> sp_dstvmotnhomtrongmotduan(Nullable<int> mada, string tennhom)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_dstvmotnhomtrongmotduan_Result>("sp_dstvmotnhomtrongmotduan", madaParameter, tennhomParameter);
        }
    
        public virtual int sp_KiemTraCongViec(Nullable<int> macongviec)
        {
            var macongviecParameter = macongviec.HasValue ?
                new ObjectParameter("macongviec", macongviec) :
                new ObjectParameter("macongviec", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KiemTraCongViec", macongviecParameter);
        }
    
        public virtual ObjectResult<string> sp_KiemTraGiaiDoanTruoc(Nullable<int> maDuAn, string maGiaiDoan)
        {
            var maDuAnParameter = maDuAn.HasValue ?
                new ObjectParameter("MaDuAn", maDuAn) :
                new ObjectParameter("MaDuAn", typeof(int));
    
            var maGiaiDoanParameter = maGiaiDoan != null ?
                new ObjectParameter("MaGiaiDoan", maGiaiDoan) :
                new ObjectParameter("MaGiaiDoan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_KiemTraGiaiDoanTruoc", maDuAnParameter, maGiaiDoanParameter);
        }
    
        public virtual int sp_KiemTraNhiemVu(string manhiemvu)
        {
            var manhiemvuParameter = manhiemvu != null ?
                new ObjectParameter("manhiemvu", manhiemvu) :
                new ObjectParameter("manhiemvu", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KiemTraNhiemVu", manhiemvuParameter);
        }
    
        public virtual int sp_KiemTraNhiemVuTienQuyet(string manv, ObjectParameter check)
        {
            var manvParameter = manv != null ?
                new ObjectParameter("manv", manv) :
                new ObjectParameter("manv", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_KiemTraNhiemVuTienQuyet", manvParameter, check);
        }
    
        public virtual int sp_ktrDangNhap(string matk, string matkhau, ObjectParameter check)
        {
            var matkParameter = matk != null ?
                new ObjectParameter("matk", matk) :
                new ObjectParameter("matk", typeof(string));
    
            var matkhauParameter = matkhau != null ?
                new ObjectParameter("matkhau", matkhau) :
                new ObjectParameter("matkhau", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_ktrDangNhap", matkParameter, matkhauParameter, check);
        }
    
        public virtual int sp_themDuAn(string tenda, Nullable<float> tiendo, Nullable<System.DateTime> ngaykt, Nullable<System.DateTime> ngaybd, string chiphi, string trangthai, string mapm)
        {
            var tendaParameter = tenda != null ?
                new ObjectParameter("tenda", tenda) :
                new ObjectParameter("tenda", typeof(string));
    
            var tiendoParameter = tiendo.HasValue ?
                new ObjectParameter("tiendo", tiendo) :
                new ObjectParameter("tiendo", typeof(float));
    
            var ngayktParameter = ngaykt.HasValue ?
                new ObjectParameter("ngaykt", ngaykt) :
                new ObjectParameter("ngaykt", typeof(System.DateTime));
    
            var ngaybdParameter = ngaybd.HasValue ?
                new ObjectParameter("ngaybd", ngaybd) :
                new ObjectParameter("ngaybd", typeof(System.DateTime));
    
            var chiphiParameter = chiphi != null ?
                new ObjectParameter("chiphi", chiphi) :
                new ObjectParameter("chiphi", typeof(string));
    
            var trangthaiParameter = trangthai != null ?
                new ObjectParameter("trangthai", trangthai) :
                new ObjectParameter("trangthai", typeof(string));
    
            var mapmParameter = mapm != null ?
                new ObjectParameter("mapm", mapm) :
                new ObjectParameter("mapm", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_themDuAn", tendaParameter, tiendoParameter, ngayktParameter, ngaybdParameter, chiphiParameter, trangthaiParameter, mapmParameter);
        }
    
        public virtual int sp_themNgayNghi(Nullable<System.DateTime> ngay, string manv, string noidungnghi)
        {
            var ngayParameter = ngay.HasValue ?
                new ObjectParameter("ngay", ngay) :
                new ObjectParameter("ngay", typeof(System.DateTime));
    
            var manvParameter = manv != null ?
                new ObjectParameter("manv", manv) :
                new ObjectParameter("manv", typeof(string));
    
            var noidungnghiParameter = noidungnghi != null ?
                new ObjectParameter("noidungnghi", noidungnghi) :
                new ObjectParameter("noidungnghi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_themNgayNghi", ngayParameter, manvParameter, noidungnghiParameter);
        }
    
        public virtual int sp_themTaiNguyen(Nullable<int> mada, string matn)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var matnParameter = matn != null ?
                new ObjectParameter("matn", matn) :
                new ObjectParameter("matn", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_themTaiNguyen", madaParameter, matnParameter);
        }
    
        public virtual int sp_TinhTienDoCV(Nullable<int> maCV, string magiaidoan, ObjectParameter ketqua)
        {
            var maCVParameter = maCV.HasValue ?
                new ObjectParameter("MaCV", maCV) :
                new ObjectParameter("MaCV", typeof(int));
    
            var magiaidoanParameter = magiaidoan != null ?
                new ObjectParameter("magiaidoan", magiaidoan) :
                new ObjectParameter("magiaidoan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_TinhTienDoCV", maCVParameter, magiaidoanParameter, ketqua);
        }
    
        public virtual int sp_TinhTienDoDuAn(Nullable<int> mada, string magiaidoan, ObjectParameter ketqua)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var magiaidoanParameter = magiaidoan != null ?
                new ObjectParameter("magiaidoan", magiaidoan) :
                new ObjectParameter("magiaidoan", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_TinhTienDoDuAn", madaParameter, magiaidoanParameter, ketqua);
        }
    
        public virtual int sp_UpdateTimeSprintTheoNgay()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateTimeSprintTheoNgay");
        }
    
        public virtual int sp_UpdateTrangThai(Nullable<int> macongviec, ObjectParameter trangthai)
        {
            var macongviecParameter = macongviec.HasValue ?
                new ObjectParameter("macongviec", macongviec) :
                new ObjectParameter("macongviec", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_UpdateTrangThai", macongviecParameter, trangthai);
        }
    
        public virtual int sp_xoaDuAn(Nullable<int> mada)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_xoaDuAn", madaParameter);
        }
    
        public virtual int sp_xoaNhanVienDuAn(Nullable<int> mada, string tennhom, string manv)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            var manvParameter = manv != null ?
                new ObjectParameter("manv", manv) :
                new ObjectParameter("manv", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_xoaNhanVienDuAn", madaParameter, tennhomParameter, manvParameter);
        }
    
        public virtual int sp_xoaNhomDuAn(Nullable<int> mada, string tennhom)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_xoaNhomDuAn", madaParameter, tennhomParameter);
        }
    
        public virtual int sp_xoaTruongNhomDuAn(Nullable<int> mada, string tennhom)
        {
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            var tennhomParameter = tennhom != null ?
                new ObjectParameter("tennhom", tennhom) :
                new ObjectParameter("tennhom", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_xoaTruongNhomDuAn", madaParameter, tennhomParameter);
        }
    
        public virtual int sp_XoaUocLuong_GD_DA(string magd, Nullable<int> mada)
        {
            var magdParameter = magd != null ?
                new ObjectParameter("magd", magd) :
                new ObjectParameter("magd", typeof(string));
    
            var madaParameter = mada.HasValue ?
                new ObjectParameter("mada", mada) :
                new ObjectParameter("mada", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_XoaUocLuong_GD_DA", magdParameter, madaParameter);
        }
    }
}
