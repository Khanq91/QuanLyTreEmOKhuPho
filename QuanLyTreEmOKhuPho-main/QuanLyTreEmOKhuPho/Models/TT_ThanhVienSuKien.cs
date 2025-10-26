using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TT_ThanhVienSuKien
    {
        public int SuKienID { get; set; }
        public int UserID { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string TenTreEm { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
    }
}