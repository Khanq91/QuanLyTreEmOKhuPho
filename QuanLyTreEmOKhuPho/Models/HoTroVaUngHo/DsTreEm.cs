using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.HoTroVaUngHo
{
    public class DsTreEm
    {
        public int TreEmId { get; set; }
        public string TenTreEm { get; set; }
        public DateTime? NgaySinh { get; set; } // Nullable
        public string NgaySinhDisplay { get; set; } // Format sẵn
        public string KhuPho { get; set; }
        public string TinhTrang { get; set; }
    }
}