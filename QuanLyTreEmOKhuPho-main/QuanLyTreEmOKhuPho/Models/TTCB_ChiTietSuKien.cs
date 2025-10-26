using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TTCB_ChiTietSuKien
    {
        public string TenSuKien { get; set; }
        public string NguoiChiuTrachNhiem { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string DiaDiem { get; set; }
        public string KhuPho { get; set; }
        public string MoTa { get; set; }
    }
}