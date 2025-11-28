using Newtonsoft.Json;
using QuanLyTreEmAPI.DTOs.HoTroVaPhucLoi;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.HoTroVaUngHo;
using QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan;
using QuanLyTreEmOKhuPho.Models.UngHoVaPhucLoi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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
        public async Task<QuaTangDTO> GetChiTietUngHo(int id)
        {
            var response = await _client.GetAsync($"HoTroVaUngHo/ChiTietQuaTang/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QuaTangDTO>(json);

            return result;
        }
        public async Task<ChiTietSuaUngHo> ChiTietSuaUngHo(int id)
        {
            var response = await _client.GetAsync($"HoTroVaUngHo/ChiTietThongTinQuaTang?quaTangUngHoId={id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChiTietSuaUngHo>(json);

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
        private async Task<List<DsTreEm>> Lst_TreEmDaLoc(int quaTangUngHoId)
        {
            var response = await _client.GetAsync(
                $"HoTroVaUngHo/LocDanhSachTreEm?quaTangUngHoId={quaTangUngHoId}"
            );

            if (!response.IsSuccessStatusCode)
                return new List<DsTreEm>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DsTreEm>>(json);
            return result ?? new List<DsTreEm>();
        }
        private async Task<List<SuKienTuongLai>> lst_SuKienTuongLai()
        {
            var response = await _client.GetAsync("HoTroVaUngHo/DanhSachSuKienTuongLai");
            if (!response.IsSuccessStatusCode)
                return new List<SuKienTuongLai>();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SuKienTuongLai>>(json);
            return result ?? new List<SuKienTuongLai>();
        }

        public async Task<ActionResult> HoTroVaUngHo()
        {
            ViewBag.ThongKe = await ThongKe();
            ViewBag.Lst_KhuPho = await ThongTinKhuPho();
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
            ViewBag.lst_SuKienTuongLai = await lst_SuKienTuongLai();
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Thêm Hỗ Trợ Phúc Lợi";
            ViewBag.PageDescription = "Tạo hỗ trợ phúc lợi cho trẻ em từ đợt ủng hộ";
            return View();
        }

        public async Task<List<KhuPho>> ThongTinKhuPho()
        {
            var response = await _client.GetAsync("KhuPho");

            if (!response.IsSuccessStatusCode)
            {
                return new List<KhuPho>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<KhuPho>>(json);

            return data ?? new List<KhuPho>();
        }
        [HttpPost]
        public async Task<ActionResult> ThemUngHoPhucLoi(FormCollection form)
        {
            try
            {

                if (!int.TryParse(form["ungHoId"], out int ungHoId))
                {
                    throw new Exception("UngHoId không hợp lệ");
                }

                var loaiHoTro = form["loaiHoTro"];
                var nguoiChiuTrachNhiem = form["nguoiChiuTrachNhiemHoTro"];
                var trangThaiPhat = form["trangThaiPhat"];
                var moTa = form["moTa"] ?? "";
                var ghiChuTNV = form["ghiChuTNV"] ?? "";

                // Parse dates
                if (!DateTime.TryParse(form["ngayPhanPhat"], out DateTime ngayPhanPhat))
                {
                    throw new Exception($"Ngày phân phát không hợp lệ: {form["ngayPhanPhat"]}");
                }

                DateTime? ngayHenLai = null;
                if (!string.IsNullOrEmpty(form["ngayHenLai"]))
                {
                    if (DateTime.TryParse(form["ngayHenLai"], out DateTime tempDate))
                    {
                        ngayHenLai = tempDate;
                    }
                }

                // Thông tin quà tặng
                var tenQua = form["tenQua"] ?? "";

                decimal? donGia = null;
                if (!string.IsNullOrEmpty(form["donGia"]))
                {
                    if (decimal.TryParse(form["donGia"], out decimal tempDonGia))
                    {
                        donGia = tempDonGia;
                    }
                }

                var doiTuongNhan = form["doiTuongNhan"] ?? "";

                int? suKienId = null;
                if (!string.IsNullOrEmpty(form["suKienId"]) && form["suKienId"] != "0")
                {
                    if (int.TryParse(form["suKienId"], out int tempSuKien))
                    {
                        suKienId = tempSuKien;
                    }
                }

                var moTaQua = form["moTaQua"] ?? "";

                var anhQuaBase64 = form["anhQua"] ?? "";

                if (!string.IsNullOrEmpty(anhQuaBase64))
                {
                    System.Diagnostics.Debug.WriteLine($"🖼️ Độ dài base64: {anhQuaBase64.Length} ký tự");
                    System.Diagnostics.Debug.WriteLine($"🖼️ Prefix: {anhQuaBase64.Substring(0, Math.Min(50, anhQuaBase64.Length))}");
                }

                // Thông tin phân phát
                var nguoiPhanPhat = form["nguoiPhanPhat"];
                var ghiChuPhanPhat = form["ghiChuPhanPhat"] ?? "";

                // Parse danh sách trẻ em
                var danhSachTreEmJson = form["danhSachTreEm"];

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
                // TẠO DTO GỬI API
                // ============================================
                var apiRequest = new
                {
                    UngHoId = ungHoId,
                    QuaTangUngHoId = (int?)null, // Tạo mới quà tặng

                    LoaiHoTro = loaiHoTro,
                    NguoiChiuTrachNhiemHoTro = nguoiChiuTrachNhiem,

                    // Thông tin quà tặng
                    TenQua = tenQua,
                    DonGia = donGia,
                    DoiTuongNhan = doiTuongNhan,
                    SuKienId = suKienId,
                    MoTaQua = moTaQua,

                    // ✅ ẢNH BASE64
                    AnhQua = anhQuaBase64,

                    // Thông tin phân phát
                    NgayPhanPhat = ngayPhanPhat.ToString("yyyy-MM-dd"),
                    NguoiPhanPhat = nguoiPhanPhat,
                    TrangThaiPhat = trangThaiPhat,
                    GhiChuPhanPhat = ghiChuPhanPhat,

                    // Danh sách trẻ nhận
                    DanhSachTreEmNhan = danhSachTreEm
                };

                var json = JsonConvert.SerializeObject(apiRequest, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });

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

                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);

                    TempData["SuccessMessage"] = $"{result.Message ?? "Thêm hỗ trợ thành công!"}";
                    return RedirectToAction("HoTroVaUngHo", "HoTroVaUngHo");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Lỗi: {errorContent}";

                    ViewBag.DsUngHo = await DsUngHo();
                    ViewBag.Lst_TreEm = await Lst_TreEm();
                    ViewBag.lst_SuKienTuongLai = await lst_SuKienTuongLai();
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                ViewBag.DsUngHo = await DsUngHo();
                ViewBag.Lst_TreEm = await Lst_TreEm();
                ViewBag.lst_SuKienTuongLai = await lst_SuKienTuongLai();
                return View();
            }
        }
        public async Task<SoLuongQuaConLai> SoLuongQuaConLai(int quaTangUngHoId)
        {
            var response = await _client.GetAsync($"HoTroVaUngHo/SoLuongQuaConLai?quaTangUngHoId={quaTangUngHoId}");
            if (!response.IsSuccessStatusCode)
            {
                return new SoLuongQuaConLai();
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SoLuongQuaConLai>(json);
            return result ?? new SoLuongQuaConLai();
        }
        [HttpPost]
        public async Task<ActionResult> XoaUngHo(int QuanTangUngHoId)
        {
            var response = await _client.PostAsync($"HoTroVaUngHo/XoaQuaTangUngHo/{QuanTangUngHoId}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Xóa thành công!";
                TempData["NotificationType"] = "success";
                return RedirectToAction("HoTroVaPhucLoi");
            }
            else
            {
                TempData["Notification"] = "Đã có trẻ em nhận quà không thể xóa";
                TempData["NotificationType"] = "error";
                var content = await response.Content.ReadAsStringAsync();
                return RedirectToAction("HoTroVaPhucLoi");
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
        public async Task<ActionResult> XoaHoTro(int UngHoID, int SoLuongNhan, int id)
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
        public async Task<ActionResult> SuaUngHo(int id)
        {
            ViewBag.ChiTietSuaUngHo = await ChiTietSuaUngHo(id);
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SuaUngHo(SuaUngHoDTO suh)
        {
            var json = JsonConvert.SerializeObject(suh);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("HoTroVaUngHo/SuaUngHo", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Cập nhật thành công!";

                return RedirectToAction("SuaUngHo", new { id = suh.QuaTangUngHoId });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Lỗi cập nhật: {error}";

                return RedirectToAction("SuaUngHo", new { id = suh.QuaTangUngHoId });
            }
        }
        public async Task<ActionResult> ThemTreEm(int id)
        {
            ViewBag.ChiTietSuaUngHo = await ChiTietSuaUngHo(id);
            ViewBag.Lst_TreEm = await Lst_TreEmDaLoc(id);
            ViewBag.SoLuongQuaConLai = await SoLuongQuaConLai(id);
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ThemTreEm(ThemTreVaoPhanPhatDTO model)
        {
            var json = JsonConvert.SerializeObject(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Gửi POST API
            var response = await _client.PostAsync("HoTroVaUngHo/ThemTreVaoPhanPhat", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                // Giải mã JSON từ API
                var errorObj = JsonConvert.DeserializeObject<dynamic>(errorContent);

                string message = errorObj?.message ?? "Lỗi không xác định";

                TempData["Notification"] = message;
                TempData["NotificationType"] = "error";
                return RedirectToAction("ThemTreEm", new { id = model.QuaTangUngHoId });
            }
            TempData["Notification"] = "Đã thêm trẻ em thành công";
            TempData["NotificationType"] = "success";

            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return RedirectToAction("ThemTreEm", new { id = model.QuaTangUngHoId });
        }
    }

}