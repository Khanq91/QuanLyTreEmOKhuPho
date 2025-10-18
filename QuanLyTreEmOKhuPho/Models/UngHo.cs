using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class UngHo
    {
        public int UngHoId { get; set; }

        public decimal? SoTien { get; set; }

        public string LoaiUngHo { get; set; }

        public DateTime NgayUngHo { get; set; }

        public string GhiChu { get; set; }

        public int? ManhThuongQuanId { get; set; }
    }
}