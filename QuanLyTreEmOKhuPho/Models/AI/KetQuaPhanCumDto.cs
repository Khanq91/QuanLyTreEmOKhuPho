using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.AI
{
    public class KetQuaPhanCumDto
    {
        public int TongSoTreEm { get; set; }
        public List<TreEmDonGianDto> DanhSachTreEm { get; set; } 
        public class TreEmDonGianDto
        {
            public int TreEmId { get; set; }
            public string TenTreEm { get; set; }
            public int? Tuoi { get; set; }
            public string KhuPho { get; set; }
            public string MucDo { get; set; } // Cao, Trung bình, Thấp, Ổn định
            public double DiemCapBach { get; set; }
            public List<string> LyDoChinh { get; set; } 
            public List<string> HuongGiaiQuyet { get; set; } 
        }
    }
}