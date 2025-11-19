using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.ManhThuongQuan
{
    public class UngHoViewModel
    {
        public int? ManhThuongQuanId { get; set; } 
        public decimal? SoTien { get; set; }
        public DateTime? NgayUngHo { get; set; }
        public string DoiTuong { get; set; }
        public string LoaiUngHo { get; set; }
        public int SoLuongVatPham { get; set; }
        public string TenVatPham { get; set; }
        public string GhiChu { get; set; }
    }
}