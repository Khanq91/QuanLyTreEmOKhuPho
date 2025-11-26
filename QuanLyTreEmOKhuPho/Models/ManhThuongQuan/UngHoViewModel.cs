using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.ManhThuongQuan
{
    public class UngHoViewModel
    {
        public int? ManhThuongQuanId { get; set; } 
        public decimal? SoTien { get; set; }
        public DateTime? NgayUngHo { get; set; }
        public string DoiTuong { get; set; }
        public string LoaiUngHo { get; set; }
        public int SoLuongVatPham { get; set; }
        public string TenVatPham { get; set; }
        public string GhiChu { get; set; }
      
    }
    public class PhieuMinhChung
    {
        public string LoaiMinhChung { get; set; }
        public string FilePath { get; set; }
    }
    public class FileUploadDto
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FileData { get; set; } // Base64 string
        public string LoaiMinhChung { get; set; }
    }

}