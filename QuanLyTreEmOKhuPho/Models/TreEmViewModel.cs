using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QuanLyTreEmOKhuPho.Models
{
    public class TreEmViewModel
    {
        public int TreEmId { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string QuocTich { get; set; } // ✅ Thêm
        public string Truong { get; set; }
        public string KhuPho { get; set; }
        public string Anh { get; set; }
        public string TinhTrang { get; set; }

        // ✅ Sửa: Từ List<string> thành List<HoanCanhViewModel>
        public List<HoanCanhViewModel> HoanCanh { get; set; }
        public List<PhuHuynhViewModel> PhuHuynh { get; set; }
        public List<HocTapViewModel> HocTap { get; set; }
        public List<HoTroViewModel> HoTro { get; set; }
        public List<VanDongViewModel> VanDong { get; set; }
    }

    // ✅ Thêm class mới
    public class HoanCanhViewModel
    {
        public int HoanCanhId { get; set; }
        public string LoaiHoanCanh { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }

    public class HocTapViewModel
    {
        public int PhieuHocTapId { get; set; } // ✅ Thêm
        public DateTime? NamHoc { get; set; } // ✅ Sửa: Từ NgayCapNhat thành NamHoc
        public double? DiemTrungBinh { get; set; } // ✅ Sửa: Thêm nullable
        public string XepLoai { get; set; }
        public string HanhKiem { get; set; }
        public string GhiChu { get; set; }
        public string TenLop { get; set; } // ✅ Thêm
        public string TenTruong { get; set; } // ✅ Thêm
    }

    public class HoTroViewModel
    {
        public int HoTroId { get; set; }
        public string LoaiHoTro { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayCap { get; set; }
        public string TrangThaiPhat { get; set; } // ✅ Thêm
        public string NguoiChiuTrachNhiemHoTro { get; set; } // ✅ Thêm
        public DateTime? NgayHenLai { get; set; } // ✅ Thêm
        public string GhiChu { get; set; } // ✅ Thêm

        // ✅ Sửa: Từ List<string> thành List<FileViewModel>
        public List<FileViewModel> Files { get; set; }
    }

    // ✅ Thêm class mới
    public class FileViewModel
    {
        public int MinhChungId { get; set; }
        public string LoaiMinhChung { get; set; }
        public DateTime? NgayCap { get; set; }
        public string Url { get; set; }
    }

    public class VanDongViewModel
    {
        public int VanDongId { get; set; } // ✅ Thêm
        public DateTime? NgayVanDong { get; set; }
        public string LyDo { get; set; }
        public string KetQua { get; set; }
        public int? SoLan { get; set; } // ✅ Sửa: Thêm nullable
        public string TinhTrangCapNhat { get; set; } // ✅ Thêm
        public string GhiChuChiTiet { get; set; } // ✅ Thêm
        public DateTime? NgayCapNhat { get; set; } // ✅ Thêm
        public HoanCanhSimpleViewModel HoanCanh { get; set; } // ✅ Thêm
        public NguoiDungSimpleViewModel NguoiVanDong { get; set; } // ✅ Thêm
        public string AnhMinhChung { get; set; } // ✅ Thêm
    }

    // ✅ Thêm class mới
    public class HoanCanhSimpleViewModel
    {
        public int HoanCanhId { get; set; }
        public string LoaiHoanCanh { get; set; }
    }

    // ✅ Thêm class mới
    public class NguoiDungSimpleViewModel
    {
        public int UserId { get; set; }
        public string HoTen { get; set; }
    }

    public class PhuHuynhViewModel
    {
        public int PhuHuynhId { get; set; } // ✅ Thêm
        public string HoTen { get; set; }

        // ✅ Sửa: Thêm JsonProperty để map từ "SDT" sang "Sdt"
        [JsonProperty("SDT")]
        public string Sdt { get; set; }

        public string NgheNghiep { get; set; }
        public string DiaChi { get; set; } // ✅ Thêm
        public DateTime? NgaySinh { get; set; } // ✅ Thêm
        public string TonGiao { get; set; } // ✅ Thêm
        public string DanToc { get; set; } // ✅ Thêm
        public string QuocTich { get; set; } // ✅ Thêm
        public string MoiQuanHe { get; set; }
    }
}