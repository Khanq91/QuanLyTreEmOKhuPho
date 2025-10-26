using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.TinhNguyenVien
{
    public class HoTroVanDong
    {
        public string TenTinhNguyenVien { get; set; }
        public string TenTreEm { get; set; }
        public string loaiHoanCanh { get; set; }
        public string MoTaHoanCanh { get; set; }
        public int SoLan { get; set; }
        public string LyDo { get; set; }
        public string KetQua { get; set; }
        public DateTime NgayVanDong { get; set; }

    }
}