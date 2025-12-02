using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.HoTroVaUngHo
{
    public class DanhSachUngHo
    {
        public int QuaTangUngHoId { get; set; }
        public string TenQua { get; set; }
        public string MoTa { get; set; }
        public string DoiTuongNhan  {get; set; }
        public int SoLuongTreEmDuocUngHo { get; set; }
        public decimal TongGiaTri { get; set; }
        public DateTime NgayUngHo { get; set; }
        public string TenManhThuongQuan { get; set; }
    }
}