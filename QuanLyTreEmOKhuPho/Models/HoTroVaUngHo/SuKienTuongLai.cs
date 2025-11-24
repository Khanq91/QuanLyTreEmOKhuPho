using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.HoTroVaUngHo
{
    public class SuKienTuongLai
    {
        public int SuKienId { get; set; }
        public string TenSuKien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string DiaDiem { get; set; }
    }
}