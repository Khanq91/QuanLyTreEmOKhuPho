using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.ManhThuongQuan
{
    public class GhiNhanManhThuongQuan
    {
        public int ManhThuongQuanId { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }
    }
}