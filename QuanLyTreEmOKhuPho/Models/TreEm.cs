using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TreEm
    {
        public int TreEmId { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string TonGiao { get; set; }

        public string DanToc { get; set; }

        public string QuocTich { get; set; }

        public int TruongId { get; set; }

        public string TinhTrang { get; set; }
    }
}