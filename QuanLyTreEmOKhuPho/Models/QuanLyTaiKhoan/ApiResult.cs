using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan
{
    public class ApiResult
    {
        public List<ChiTietLoi> chiTiet { get; set; }
        public class ChiTietLoi
        {
            public int index { get; set; }
            public List<string> errors { get; set; }
        }
    }
}