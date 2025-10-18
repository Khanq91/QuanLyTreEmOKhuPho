using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class SuKien
    {
        public int SuKienId { get; set; }

        public string TenSuKien { get; set; }

        public string NguoiChiuTrachNhiem { get; set; }

        public string MoTa { get; set; }

        public string DiaDiem { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public int? SoLuongTinhNguyenVien { get; set; }

        public int SoLuongTreEm { get; set; }

        public int UserId { get; set; }
        public int khuPhoID { get; set; }
    }
}