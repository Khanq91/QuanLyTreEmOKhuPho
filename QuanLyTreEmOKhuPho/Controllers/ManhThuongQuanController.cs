using Antlr.Runtime.Tree;
using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.ManhThuongQuan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class ManhThuongQuanController : Controller
    {
        // GET: ManhThuongQuan

        private readonly HttpClient _client;
        public ManhThuongQuanController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ThongKeManhThuongQuanDTO> GetThongKeManhThuongQuanAsync()
        {
            var response = await _client.GetAsync("ManhThuongQuan/ThongKeManhThuongQuan");

            if (!response.IsSuccessStatusCode)
                return new ThongKeManhThuongQuanDTO();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ThongKeManhThuongQuanDTO>(json);

            return result ?? new ThongKeManhThuongQuanDTO();
        }
        //Thong tin mạnh thường quân
        public async Task<List<ThongTinManhThuongQuanDTO>> ThongTinManhThuongQuan()
        {
            var response = await _client.GetAsync("ManhThuongQuan/ThongTinhManhThuongQuan");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ThongTinManhThuongQuanDTO>();
            }
            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ThongTinManhThuongQuanDTO>>(data);
        }
        public async Task<GhiNhanManhThuongQuan> GhiNhanManhThuongQuan_API(GhiNhanManhThuongQuan mtq)
        {
            var json = JsonConvert.SerializeObject(mtq);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Gọi POST đúng với API
            var response = await _client.PostAsync("ManhThuongQuan/GhiNhanManhThuongQuan", content);

            if (!response.IsSuccessStatusCode)
                return null;

            // Đọc JSON trả về
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Parse dữ liệu
            dynamic result = JsonConvert.DeserializeObject(jsonResponse);
            var dataJson = Convert.ToString(result.data);
            var data = JsonConvert.DeserializeObject<GhiNhanManhThuongQuan>(dataJson);

            return data;
        }
        public async Task<ChiTietManhThuongQuan> ChiTietManhThuongQuan_API(int manhThuongQuanID)
        {
            var response = await _client.GetAsync($"ManhThuongQuan/GetThongTinChiTietManhThuongQuan?ManhThuongQuanID={manhThuongQuanID}");

            if (!response.IsSuccessStatusCode)
            {
                return new ChiTietManhThuongQuan();
            }
            var json = await response.Content.ReadAsStringAsync();
            var ctmtq = JsonConvert.DeserializeObject<ChiTietManhThuongQuan>(json);
            return ctmtq;
        }
        //Sửa thông tin mạnh thường quân
        public async Task<GhiNhanManhThuongQuan> SuaThongTinManhThuongQuan(GhiNhanManhThuongQuan mtq)
        {
            var jsonContent = new StringContent(
                    JsonConvert.SerializeObject(mtq),
                    Encoding.UTF8,
                    "application/json"
                );

            // Gửi PUT tới API (Sửa thông tin Mạnh Thường Quân)
            var response = await _client.PutAsync("ManhThuongQuan/SuaManhThuongQuan", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                // Nếu API trả về lỗi, có thể log hoặc trả về null
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Lỗi API: " + errorResponse);
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize kết quả trả về từ API
            var updatedData = JsonConvert.DeserializeObject<GhiNhanManhThuongQuan>(jsonResponse);

            return updatedData;
        }
        public async Task<GhiNhanManhThuongQuan> SuaManhThuongQuan_API(GhiNhanManhThuongQuan model)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync("ManhThuongQuan/SuaManhThuongQuan", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GhiNhanManhThuongQuan>(jsonResponse);
            }
            else
            {
                // Trả về null hoặc thông báo lỗi nếu không thành công
                return null;
            }
        }
        // Sửa ủng hộ 
        public async Task<SuaThongTinUngHoManhThuongQuan> LayThongTinUngHo(int ungHoId)
        {
            var response = await _client.GetAsync($"ManhThuongQuan/ThongTinUngHoCuaManhThuongQuan?UngHoID={ungHoId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SuaThongTinUngHoManhThuongQuan>(json);
            return data;
        }
        public async Task<SuaThongTinUngHoManhThuongQuan> SuaThongTinUngHo(SuaThongTinUngHoManhThuongQuan mqt)
        {
            // Chuyển object thành JSON
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(mqt),
                Encoding.UTF8,
                "application/json"
            );

            // Gửi POST tới API
            var response = await _client.PostAsync("ManhThuongQuan/SuaUngHoCuaManhThuongQuan", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                // Nếu lỗi, có thể log hoặc trả về null
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize kết quả trả về
            var updatedData = JsonConvert.DeserializeObject<SuaThongTinUngHoManhThuongQuan>(jsonResponse);

            return updatedData;
        }

        //Controller
        public async Task<ActionResult> ManhThuongQuan()
        {
            ViewBag.TongThongKe = await GetThongKeManhThuongQuanAsync();
            ViewBag.ThongTinManhThuongQuan = await ThongTinManhThuongQuan();
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Quản lý thông tin nhà tài trợ";
            return View();
        }
        public async Task<List<UngHo>> LichSuUngHo(int id)
        {
            var response = await _client.GetAsync($"UngHo/GetAll?ManhThuongQuanID={id}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<UngHo>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<UngHo>>(json);
            return data;
        }
        //Ghi nhận ủng hộ của mạnh thường quân
        public async Task<ActionResult> GhiNhanUngHo()
        {
            ViewBag.ThongTinManhThuongQuan = await ThongTinManhThuongQuan();

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận ủng hộ của mạnh thường quân";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GhiNhanUngHo(UngHoViewModel model)
        {
            ViewBag.ThongTinManhThuongQuan = await ThongTinManhThuongQuan();
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận ủng hộ của mạnh thường quân";

            if (model.ManhThuongQuanId == null)
            {
                ViewBag.MessageError = "Vui lòng chọn mạnh thường quân.";
                return View("GhiNhanUngHo", model);
            }

            if (model.SoTien == null || model.SoTien <= 0)
            {
                ViewBag.MessageError = "Vui lòng nhập số tiền hợp lệ.";
                return View("GhiNhanUngHo", model);
            }

            if (model.NgayUngHo == default(DateTime))
            {
                ViewBag.MessageError = "Vui lòng chọn ngày ủng hộ.";
                return View("GhiNhanUngHo", model);
            }

            if (string.IsNullOrWhiteSpace(model.HinhThuc))
            {
                ViewBag.MessageError = "Vui lòng chọn hình thức ủng hộ.";
                return View("GhiNhanUngHo", model);
            }

            try
            {
                // ✅ Tạo anonymous object với format đúng cho API
                var apiDto = new
                {
                    ManhThuongQuanId = model.ManhThuongQuanId,  // Giữ nguyên tên
                    SoTien = model.SoTien,
                    NgayUngHo = model.NgayUngHo.ToString("yyyy-MM-dd"), // ✅ Format thành string "2025-10-22"
                    HinhThuc = model.HinhThuc,
                    GhiChu = model.GhiChu
                };

                var json = JsonConvert.SerializeObject(apiDto);
                System.Diagnostics.Debug.WriteLine("=== JSON SENT ===");
                System.Diagnostics.Debug.WriteLine(json);
                // Sẽ ra: {"ManhThuongQuanId":1,"SoTien":1000000,"NgayUngHo":"2025-10-22","HinhThuc":"tien-mat","GhiChu":"test"}

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("ManhThuongQuan/LuuThongTinUngHo", content);

                var responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("=== RESPONSE ===");
                System.Diagnostics.Debug.WriteLine($"Status: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"Body: {responseBody}");

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.MessageSuccess = "Lưu thông tin ủng hộ thành công!";
                    ModelState.Clear();
                    return View("GhiNhanUngHo", new UngHoViewModel());
                }
                else
                {
                    ViewBag.MessageError = $"Lỗi API: {responseBody}";
                    return View("GhiNhanUngHo", model);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                ViewBag.MessageError = "Đã xảy ra lỗi: " + ex.Message;
                return View("GhiNhanUngHo", model);
            }
        }
        //Ghi nhận ủng hộ của mạnh thường quân
        //Thêm mạnh thường quân
        public ActionResult GhiNhanManhThuongQuan()
        {
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận mạnh thường quân";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GhiNhanManhThuongQuan(GhiNhanManhThuongQuan mtq)
        {
            var result = await GhiNhanManhThuongQuan_API(mtq);

            if (result != null)
            {
                ViewBag.MessageSuccess = $"Đã ghi nhận mạnh thường quân: {result.Ten}";
            }
            else
            {
                ViewBag.MessageError = "Ghi nhận thất bại, vui lòng thử lại!";
            }

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận mạnh thường quân";

            return View();
        }
        private async Task<GhiNhanManhThuongQuan> GetManhThuongQuan(int manhThuongQuanId)
        {
            var response = await _client.GetAsync($"ManhThuongQuan/ThongTinManhThuongQuan?ManhThuongQuanID={manhThuongQuanId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var manhThuongQuan = JsonConvert.DeserializeObject<GhiNhanManhThuongQuan>(jsonResponse);

            return manhThuongQuan;
        }
        public async Task<ActionResult> SuaManhThuongQuan(int ManhThuongQuanId)
        {
            var ThongTinManhThuongQuan = await GetManhThuongQuan(ManhThuongQuanId);

            if (ThongTinManhThuongQuan == null)
            {
                return View(new GhiNhanManhThuongQuan());
            }

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa thông tin mạnh thường quân";

            return View(ThongTinManhThuongQuan);  
        }
        [HttpPost]
        public async Task<ActionResult> SuaManhThuongQuan(GhiNhanManhThuongQuan mtq)
        {
            // Kiểm tra dữ liệu đầu vào
            if (mtq == null || mtq.ManhThuongQuanId <= 0)
            {
                TempData["MessageError"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction("ManhThuongQuan", "ManhThuongQuan");
            }

            // Kiểm tra validation
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Vui lòng điền đầy đủ thông tin bắt buộc.";
                return RedirectToAction("SuaManhThuongQuan", new { ManhThuongQuanId = mtq.ManhThuongQuanId });
            }

            try
            {
                var result = await SuaManhThuongQuan_API(mtq);

                if (result != null)
                {
                    TempData["MessageSuccess"] = "Thông tin mạnh thường quân đã được cập nhật thành công!";
                }
                else
                {
                    TempData["MessageError"] = "Cập nhật thông tin thất bại, vui lòng thử lại!";
                }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToAction("SuaManhThuongQuan", new { ManhThuongQuanId = mtq.ManhThuongQuanId });
        }

        //Thêm mạnh thường quân

        //Thong tin chi tiết 1 mạnh thường quân
        public async Task<ActionResult> ChiTietManhThuongQuan(int ManhThuongQuanId)
        {
            ViewBag.ChiTietManhThuongQuan = await ChiTietManhThuongQuan_API(ManhThuongQuanId);
            ViewBag.LichSuUngHo = await LichSuUngHo(ManhThuongQuanId);

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Chi tiết mạnh thường quân";
            return View();
        }
        public async Task<ActionResult> SuaUngHoManhThuongQuan(int UngHoID)
        {
            var model = await LayThongTinUngHo(UngHoID);

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa ủng hộ mạnh thường quân";
            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> SuaUngHoManhThuongQuan(SuaThongTinUngHoManhThuongQuan ttmtq)
        {
            var relust = await SuaThongTinUngHo(ttmtq);
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa ủng hộ mạnh thường quân";
            return RedirectToAction("SuaUngHoManhThuongQuan", new { UngHoId = ttmtq.UngHoId });
        }
        //Xóa Ủng hộ mạnh thường quân
        [HttpPost]
        public async Task<ActionResult> XoaUngHo(int UngHoId, int ManhThuongQuanId)
        {
            // Gọi API DELETE
            var request = new HttpRequestMessage(HttpMethod.Delete, $"ManhThuongQuan/XoaUngHo?UngHoId={UngHoId}");
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Xóa ủng hộ thành công";
                TempData["NotificationType"] = "success";
            }

            // Chuyển về chi tiết mạnh thường quân với id đúng
            return RedirectToAction("ChiTietManhThuongQuan", new { ManhThuongQuanId });
        }
    }

}
