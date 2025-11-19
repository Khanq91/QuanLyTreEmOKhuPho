using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.ManhThuongQuan
{
    public class SuaThongTinUngHoManhThuongQuan
    {
        public int UngHoId { get; set; }
        public decimal SoTien { get; set; }
        public string LoaiUngHo { get; set; }
        public string NgayUngHo { get; set; }
        public int SoLuongVatPham { get; set; }

        public string GhiChu { get; set; }
        public string TenManhThuongQuan { get; set; }
        public int ManhThuongQuanId { get; set; }

    }
}