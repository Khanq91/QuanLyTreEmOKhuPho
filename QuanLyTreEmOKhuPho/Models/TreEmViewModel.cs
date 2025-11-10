using System;
using System.Collections.Generic;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TreEmViewModel
    {
        public int TreEmId { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DanToc { get; set; }       // <-- Cần có
        public string TonGiao { get; set; }      // <-- Cần có
        public string Truong { get; set; }       // <-- Cần có
        public string KhuPho { get; set; }       // <-- Cần có
        public string Anh { get; set; }
        public string TinhTrang { get; set; }

        public List<string> HoanCanh { get; set; }
        public List<PhuHuynhViewModel> PhuHuynh { get; set; }
        public List<HocTapViewModel> HocTap { get; set; }
        public List<HoTroViewModel> HoTro { get; set; }
        public List<VanDongViewModel> VanDong { get; set; }
    }
    public class HocTapViewModel
    {
        public DateTime NgayCapNhat { get; set; }
        public double DiemTrungBinh { get; set; }
        public string XepLoai { get; set; }
        public string HanhKiem { get; set; }
        public string GhiChu { get; set; }
    }
    public class HoTroViewModel
    {
        public int HoTroId { get; set; }
        public string LoaiHoTro { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCap { get; set; }
        public List<string> File { get; set; }
    }
    public class VanDongViewModel
    {
        public DateTime NgayVanDong { get; set; }
        public string LyDo { get; set; }
        public string KetQua { get; set; }
        public int SoLan { get; set; }
    }
    public class PhuHuynhViewModel
    {
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public string NgheNghiep { get; set; }
        public string MoiQuanHe { get; set; }
    }
}