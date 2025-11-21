using System;
using System.Collections.Generic;

namespace QuanLyTreEmOKhuPho.Models.HoTroVaUngHo
{
    public class ChiTietSuaUngHo
    {
        // Mạnh Thường Quân
        public string TenManhThuongQuan { get; set; }

        // Thông tin ủng hộ
        public int UngHoId { get; set; }
        public string LoaiUngHo { get; set; }
        public string TenVatPham { get; set; }
        public int SoLuongVatPham { get; set; }
        public int SoLuongConLai { get; set; }
        public DateTime NgayUngHo { get; set; }
        public string DoiTuong { get; set; }
        public string MoTaUngHo { get; set; }

        // Quà tặng ủng hộ
        public int QuaTangUngHoId { get; set; }
        public string TenQua { get; set; }
        public string MoTaQua { get; set; }
        public string DoiTuongNhan { get; set; }
        public int SoLuongConLaiQuaTang { get; set; }
        public string LoaiHoTro { get; set; }

        public string NguoiChiuTrachNhiem { get; set; }
        public string NguoiPhanPhat { get; set; }
        public List<int> PhanPhatQuaIds { get; set; }

    }
}
