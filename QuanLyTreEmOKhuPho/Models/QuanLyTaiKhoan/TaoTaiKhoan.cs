using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan
{
    public class TaoTaiKhoan
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 số và bắt đầu bằng 0")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public string VaiTro { get; set; }

        public string MatKhau { get; set; } = "140letrongtan";

        public string TrangThai { get; set; } = "Đang hoạt động";

        public string Anh { get; set; } = "/Anh/NguoiDung/macdinh.jpg";

        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}