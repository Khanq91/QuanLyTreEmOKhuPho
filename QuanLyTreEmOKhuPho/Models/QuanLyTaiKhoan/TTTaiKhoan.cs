using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan
{
    public class TTTaiKhoan
    {
        public int UserId { get; set; }

        public string HoTen { get; set; }
        public string SDT { get; set; }

        public string Email { get; set; }

        public string VaiTro { get; set; }
    }
}