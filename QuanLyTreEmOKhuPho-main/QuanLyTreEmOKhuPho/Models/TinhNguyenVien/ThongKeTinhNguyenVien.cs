using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class ThongKeTinhNguyenVien
    {
        public int KhuPhoID { get; set; }
        public int UserID { get; set; }
        public int TinhNguyenVienID { get; set; }
        public string TenTinhNguyenVien { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Anh { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
        public string TenKhuPho { get; set; }
        public string SDT { get; set; }
        public string ChucVu { get; set; }
        public int SoLuongCaRanh { get; set; }
        public int TongSuKienThamGia { get; set; }
    }
}