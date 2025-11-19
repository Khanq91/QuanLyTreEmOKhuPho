using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.HoTroVaUngHo
{
    public class PhanPhatQuaUngHoDTO
    {
        public int UngHoId { get; set; }

        // 2. THÔNG TIN ĐỢT PHÁT (QuaTangUngHo)
        public int? QuaTangUngHoId { get; set; } // Nếu null = tạo đợt phát mới
        public int? SoLuongTong { get; set; }    // Số lượng của đợt phát (nếu tạo mới)
        public int? SuKienId { get; set; }
        public string TenQua { get; set; }
        public string MoTaQua { get; set; }
        public decimal? DonGia { get; set; }
        public string DoiTuongNhan { get; set; }
        public string AnhQua { get; set; }

        // 3. THÔNG TIN HỖ TRỢ PHÚC LỢI
        public string LoaiHoTro { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCap { get; set; }
        public string NguoiChiuTrachNhiemHoTro { get; set; }
        public string TrangThaiPhat { get; set; } = "Chưa phát";
        public DateTime? NgayHenLai { get; set; }
        public string GhiChuTNV { get; set; }
        public int NguoiDungID { get; set; }

        // 4. DANH SÁCH TRẺ EM NHẬN
        public List<TreEmNhanDTO> DanhSachTreEmNhan { get; set; }

        // 5. THÔNG TIN PHÂN PHÁT
        public DateTime NgayPhanPhat { get; set; }
        public string NguoiPhanPhat { get; set; }
        public string GhiChuPhanPhat { get; set; }
    }

    public class TreEmNhanQuaDTO
    {
        public int TreEmId { get; set; }
        public int SoLuongNhan { get; set; }
        public string GhiChu { get; set; }
    }

    // DTO cho response
    public class TaoHoTroVaPhanPhatResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int UngHoId { get; set; }
        public int QuaTangUngHoId { get; set; }
        public int SoTreEmDaNhan { get; set; }
        public int TongSoLuongPhat { get; set; }
        public int SoLuongConLaiQuaTang { get; set; }
        public int SoLuongConLaiUngHo { get; set; }
        public List<HoTroPhucLoiInfo> DanhSachHoTroPhucLoi { get; set; }
        public List<PhanPhatQuaInfo> DanhSachPhanPhat { get; set; }
    }

    public class HoTroPhucLoiInfo
    {
        public int HoTroId { get; set; }
        public int TreEmId { get; set; }
        public string HoTenTreEm { get; set; }
        public string LoaiHoTro { get; set; }
        public string TrangThaiPhat { get; set; }
    }

    public class PhanPhatQuaInfo
    {
        public int PhanPhatId { get; set; }
        public int TreEmId { get; set; }
        public string HoTenTreEm { get; set; }
        public int SoLuongNhan { get; set; }
        public string TrangThai { get; set; }
    }
}