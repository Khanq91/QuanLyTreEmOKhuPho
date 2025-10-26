using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TinhNguyenVienDetail
    {
        public int TinhNguyenVienID { get; set; }
        public string TenTinhNguyenVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TenKhuPho { get; set; }
        public string SDT { get; set; }
        public string ChucVu { get; set; }

        public int ChiTietLichTrongID { get; set; }
        public string Buoi { get; set; }
        public string Thu { get; set; }

        public string CongViec { get; set; }
        public DateTime? NgayPhanCong { get; set; }
        public string GhiChu { get; set; }

        public int? SuKienID { get; set; }
        public string TenSuKien { get; set; }
        public DateTime? NgayBatDau { get; set; }
    }

}