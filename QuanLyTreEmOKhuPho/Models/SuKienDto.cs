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
        public string KhuPho { get; set; }

        public List<ChiPhiItemVM> ChiPhi { get; set; } = new List<ChiPhiItemVM>();
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
    }
}