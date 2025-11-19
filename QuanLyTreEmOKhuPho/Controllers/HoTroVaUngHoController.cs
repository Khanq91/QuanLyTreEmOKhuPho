using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.HoTroVaUngHo;
using QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan;
using QuanLyTreEmOKhuPho.Models.UngHoVaPhucLoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class HoTroVaUngHoController : Controller
    {
        private readonly HttpClient _client;
        public HoTroVaUngHoController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        // GET: HoTroVaUngHo
        public async Task<ThongKeHoTroThangDTO> ThongKe()
        {
            var response = await _client.GetAsync("HoTroVaUngHo/ThongKe");

            if (!response.IsSuccessStatusCode)
                return new ThongKeHoTroThangDTO();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ThongKeHoTroThangDTO>(json);

            return result ?? new ThongKeHoTroThangDTO();
        }
        public async Task<List<DanhSachUngHo>> DSHoTroPhucLoi()
        {
            var response = await _client.GetAsync("HoTroVaUngHo/DanhSachHoTro");

            if (!response.IsSuccessStatusCode)
                return new List<DanhSachUngHo>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DanhSachUngHo>>(json);
            return result ?? new List<DanhSachUngHo>();
        }
        public async Task<ChiTietUngHoDTO> GetChiTietUngHo(int id)
        {
            var response = await _client.GetAsync($"HoTroVaUngHo/ChiTiet/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChiTietUngHoDTO>(json);

            return result;
        }
        public async Task<List<UngHoListDTO>> DsUngHo()
        {
            var response = await _client.GetAsync("HoTroVaUngHo/DanhSachUngHo");

            if (!response.IsSuccessStatusCode)
                return new List<UngHoListDTO>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UngHoListDTO>>(json);
            return result ?? new List<UngHoListDTO>();
        }

        private async Task<List<DsTreEm>> Lst_TreEm()
        {
            var response = await _client.GetAsync("HoTroVaUngHo/DanhSachTreEm");

            if (!response.IsSuccessStatusCode)
                return new List<DsTreEm>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DsTreEm>>(json);
            return result ?? new List<DsTreEm>();
        }
        public async Task<ActionResult> HoTroVaUngHo()
        {
            ViewBag.ThongKe = await ThongKe();
            ViewBag.DSHoTroPhucLoi = await DSHoTroPhucLoi();
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return View();
        }
        public async Task<ActionResult> ChiTietUngHo(int id)
        {
            ViewBag.ChiTietUngHo = await GetChiTietUngHo(id);
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return View();
        }
        public async Task<ActionResult> ThemUngHoPhucLoi()
        {
            ViewBag.DsUngHo = await DsUngHo();
            ViewBag.Lst_TreEm = await Lst_TreEm();
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Thêm Hỗ Trợ Phúc Lợi";
            ViewBag.PageDescription = "Tạo hỗ trợ phúc lợi cho trẻ em từ đợt ủng hộ";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ThemUngHoPhucLoi(FormCollection form)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== BẮT ĐẦU XỬ LÝ ===");

                // ============================================
                // PARSE AN TOÀN
                // ============================================

                // 1. UngHoId
                if (!int.TryParse(form["ungHoId"], out int ungHoId))
                {
                    throw new Exception("UngHoId không hợp lệ");
                }
                System.Diagnostics.Debug.WriteLine($"UngHoId: {ungHoId}");

                // 2. Thông tin cơ bản
                var loaiHoTro = form["loaiHoTro"];
                var nguoiChiuTrachNhiem = form["nguoiChiuTrachNhiemHoTro"];
                var trangThaiPhat = form["trangThaiPhat"];
                var moTa = form["moTa"] ?? "";
                var ghiChuTNV = form["ghiChuTNV"] ?? "";

                System.Diagnostics.Debug.WriteLine($"LoaiHoTro: {loaiHoTro}");
                System.Diagnostics.Debug.WriteLine($"Người chịu trách nhiệm: {nguoiChiuTrachNhiem}");

                // 3. Parse NgayCap
                System.Diagnostics.Debug.WriteLine($"NgayCap raw: '{form["ngayCap"]}'");
                DateTime ngayCap;
                if (!DateTime.TryParse(form["ngayCap"], out ngayCap))
                {
                    throw new Exception($"Ngày cấp không hợp lệ: {form["ngayCap"]}");
                }
                System.Diagnostics.Debug.WriteLine($"NgayCap parsed: {ngayCap:yyyy-MM-dd}");

                // 4. Parse NgayPhanPhat
                System.Diagnostics.Debug.WriteLine($"NgayPhanPhat raw: '{form["ngayPhanPhat"]}'");
                DateTime ngayPhanPhat;
                if (!DateTime.TryParse(form["ngayPhanPhat"], out ngayPhanPhat))
                {
                    throw new Exception($"Ngày phân phát không hợp lệ: {form["ngayPhanPhat"]}");
                }
                System.Diagnostics.Debug.WriteLine($"NgayPhanPhat parsed: {ngayPhanPhat:yyyy-MM-dd}");

                // 5. Parse NgayHenLai (optional)
                DateTime? ngayHenLai = null;
                if (!string.IsNullOrEmpty(form["ngayHenLai"]))
                {
                    System.Diagnostics.Debug.WriteLine($"NgayHenLai raw: '{form["ngayHenLai"]}'");
                    DateTime tempDate;
                    if (DateTime.TryParse(form["ngayHenLai"], out tempDate))
                    {
                        ngayHenLai = tempDate;
                        System.Diagnostics.Debug.WriteLine($"NgayHenLai parsed: {ngayHenLai.Value:yyyy-MM-dd}");
                    }
                }

                // 6. Thông tin quà tặng
                var tenQua = form["tenQua"] ?? "";

                decimal? donGia = null;
                if (!string.IsNullOrEmpty(form["donGia"]))
                {
                    decimal tempDonGia;
                    if (decimal.TryParse(form["donGia"], out tempDonGia))
                    {
                        donGia = tempDonGia;
                    }
                }

                var doiTuongNhan = form["doiTuongNhan"] ?? "";

                int? suKienId = null;
                if (!string.IsNullOrEmpty(form["suKienId"]) && form["suKienId"] != "0")
                {
                    int tempSuKien;
                    if (int.TryParse(form["suKienId"], out tempSuKien))
                    {
                        suKienId = tempSuKien;
                    }
                }

                var moTaQua = form["moTaQua"] ?? "";
                var anhQua = form["anhQua"] ?? "";

                // 7. Thông tin phân phát
                var nguoiPhanPhat = form["nguoiPhanPhat"];
                var ghiChuPhanPhat = form["ghiChuPhanPhat"] ?? "";

                // 8. Parse danh sách trẻ em
                var danhSachTreEmJson = form["danhSachTreEm"];
                System.Diagnostics.Debug.WriteLine($"DanhSachTreEm JSON: {danhSachTreEmJson}");

                if (string.IsNullOrEmpty(danhSachTreEmJson))
                {
                    throw new Exception("Danh sách trẻ em trống!");
                }

                var danhSachTreEm = JsonConvert.DeserializeObject<List<TreEmNhanDTO>>(danhSachTreEmJson);

                if (danhSachTreEm == null || danhSachTreEm.Count == 0)
                {
                    throw new Exception("Không parse được danh sách trẻ em!");
                }

                System.Diagnostics.Debug.WriteLine($"Số trẻ em: {danhSachTreEm.Count}");

                // ============================================
                // TẠO DTO ĐỂ GỬI API
                // ============================================
                // ✅ SỬ DỤNG ANONYMOUS OBJECT thay vì PhanPhatQuaUngHoDTO
                var apiRequest = new
                {
                    UngHoId = ungHoId,
                    LoaiHoTro = loaiHoTro,
                    MoTa = moTa,
                    NgayCap = ngayCap.ToString("yyyy-MM-dd"),  // ✅ Chuyển sang string format DateOnly
                    NguoiChiuTrachNhiemHoTro = nguoiChiuTrachNhiem,
                    TrangThaiPhat = trangThaiPhat,
                    NgayHenLai = ngayHenLai?.ToString("yyyy-MM-dd"),  // ✅ Chuyển sang string format DateOnly
                    GhiChuTNV = ghiChuTNV,
                    NguoiDungID = GetCurrentUserId(),

                    // Thông tin quà tặng
                    TenQua = tenQua,
                    DonGia = donGia,
                    DoiTuongNhan = doiTuongNhan,
                    SuKienId = suKienId,
                    MoTaQua = moTaQua,
                    AnhQua = anhQua,

                    // Thông tin phân phát
                    NgayPhanPhat = ngayPhanPhat.ToString("yyyy-MM-dd"),  // ✅ Chuyển sang string format DateOnly
                    NguoiPhanPhat = nguoiPhanPhat,
                    GhiChuPhanPhat = ghiChuPhanPhat,

                    // Danh sách trẻ nhận
                    DanhSachTreEmNhan = danhSachTreEm
                };

                System.Diagnostics.Debug.WriteLine("✅ DTO đã tạo xong");

                // ============================================
                // GỌI API
                // ============================================
                var json = JsonConvert.SerializeObject(apiRequest, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                    // ✅ KHÔNG dùng DateFormatString vì đã convert thủ công
                });

                System.Diagnostics.Debug.WriteLine("=== JSON GỬI ĐI ===");
                System.Diagnostics.Debug.WriteLine(json);
                System.Diagnostics.Debug.WriteLine("===================");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(
                    "https://localhost:44362/api/HoTroVaUngHo/TaoHoTroVaPhanPhat",
                    content
                );

                System.Diagnostics.Debug.WriteLine($"Status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"✅ SUCCESS Response: {responseJson}");

                    var result = JsonConvert.DeserializeObject<TaoHoTroVaPhanPhatResponseDTO>(responseJson);

                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction("ThemUngHoPhucLoi", "HoTroVaUngHo");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"❌ ERROR Response: {errorContent}");

                    TempData["ErrorMessage"] = $"API Error: {errorContent}";

                    ViewBag.DsUngHo = await DsUngHo();
                    ViewBag.Lst_TreEm = await Lst_TreEm();
                    return View();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ EXCEPTION: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                TempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                ViewBag.DsUngHo = await DsUngHo();
                ViewBag.Lst_TreEm = await Lst_TreEm();
                return View();
            }
        }

        // Helper method
        private int GetCurrentUserId()
        {
            // Lấy từ Session
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }
            return 1; // Default nếu chưa login
        }
        [HttpPost]
        public async Task<ActionResult> SuaTrangThai(int UngHoID, int id, string TrangThai)
        {
            var response = await _client.PostAsync(
     $"HoTroVaUngHo/DoiTrangThai?id={id}&TrangThai={TrangThai}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Cập nhật trạng thái thành công!";
                TempData["NotificationType"] = "success";
                return RedirectToAction("ChiTietUngHo", new { id = UngHoID });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return RedirectToAction("ChiTietUngHo", new { id = UngHoID });
            }
        }
        [HttpPost]
        public async Task<ActionResult> XoaHoTro(int UngHoID, int id)
        {
            var response = await _client.DeleteAsync($"HoTroVaUngHo/XoaTreKhoiDanhSachPhatQua/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Cập nhật trạng thái thành công!";
                TempData["NotificationType"] = "success";
                return RedirectToAction("ChiTietUngHo", new { id = UngHoID });
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return RedirectToAction("ChiTietUngHo", new { id = UngHoID });
            }
        }
    }
}