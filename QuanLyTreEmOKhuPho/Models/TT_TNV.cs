using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TT_TNV
    {
        public int ID { get; set; }
        public string TenTinhNguyenVien { get; set; }
        public string ChucVu { get; set; }
        public string SDT { get; set; }
        public string KhuPho { get; set; }
        public int SuKien { get; set; }

        public int CaRanh { get; set; }
        public string TrangThai { get; set; }
    }
}