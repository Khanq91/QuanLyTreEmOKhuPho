using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.ManhThuongQuan
{
    public class ThongKeManhThuongQuanDTO
    {
        public int TongSoManhThuongQuan { get; set; }
        public int SoToChuc { get; set; }
        public int SoCaNhan { get; set; }
        public decimal TongTienUngHo { get; set; }
        public decimal TongTienUngHoThangNay { get; set; }
        public int TongNhaTaiTroThuongXuyen { get; set; }
    }
}