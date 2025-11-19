using System;
using System.Collections.Generic;
namespace QuanLyTreEmOKhuPho.Models
{

    public class SuKienDetailVM
    {
        public int SuKienId { get; set; }
        public string TenSuKien { get; set; }
        public string MoTa { get; set; }
        public string DiaDiem { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiChiuTrachNhiem { get; set; }
        public KhuPhoVM KhuPho { get; set; }


        public List<ThoiGianChiTietVM> ThoiGianChiTiet { get; set; } = new List<ThoiGianChiTietVM>();

        public List<ChiPhiItemVM> ChiPhi { get; set; } = new List<ChiPhiItemVM>();
    }
    public class KhuPhoVM
    {
        public int KhuPhoId { get; set; }
        public string TenKhuPho { get; set; }
        public string DiaChi { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
    }
    public class ThoiGianChiTietVM
    {
        public int ThoiGianChiTietSuKienId { get; set; }
        public string MoTa { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public List<TietMucVM> TietMuc { get; set; } = new List<TietMucVM>();

    }

    public class TietMucVM
    {
        public string TenTietMuc { get; set; }
        public string NguoiThucHien { get; set; }
        public decimal? ChiPhiTietMuc { get; set; }

        public DateTime? ThoiGian { get; set; }
    }

    public class ChiPhiItemVM
    {
        public string TenKhoanChi { get; set; }
        public decimal SoTien { get; set; }
        public string GhiChu { get; set; }

        public List<ChiTietChiPhiVM> ChiTiet { get; set; } = new List<ChiTietChiPhiVM>();
    }

    public class ChiTietChiPhiVM
    {
        public string TenPhanQua { get; set; }
        public string NguoiDaiDien { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        // Optional: API computed field. You can set this server-side or compute client-side.
        public decimal ThanhTien { get; set; }
    }
}