using Antlr.Runtime.Tree;
using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.ManhThuongQuan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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

            var response = await _client.PutAsync("ManhThuongQuan/SuaManhThuongQuan", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Lỗi API: " + errorResponse);
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

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
                return null;
            }
        }
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
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(mqt),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("ManhThuongQuan/SuaUngHoCuaManhThuongQuan", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
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
        public async Task<ActionResult> GhiNhanUngHo()
        {
            ViewBag.ThongTinManhThuongQuan = await ThongTinManhThuongQuan();
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận ủng hộ của mạnh thường quân";

            return View(new UngHoViewModel()); // Trả về model rỗng
        }

        [HttpPost]
        public async Task<ActionResult> GhiNhanUngHo(UngHoViewModel model, HttpPostedFileBase[] Files, FormCollection form)
        {
            ViewBag.ThongTinManhThuongQuan = await ThongTinManhThuongQuan();
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận ủng hộ của mạnh thường quân";

            // Validation
            if (model.ManhThuongQuanId == null)
            {
                ViewBag.MessageError = "Vui lòng chọn mạnh thường quân.";
                return View(model);
            }
            if (model.SoTien == null || model.SoTien <= 0)
            {
                ViewBag.MessageError = "Vui lòng nhập số tiền hợp lệ.";
                return View(model);
            }
            if (model.NgayUngHo == null || model.NgayUngHo == default(DateTime))
            {
                ViewBag.MessageError = "Vui lòng chọn ngày ủng hộ.";
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.LoaiUngHo))
            {
                ViewBag.MessageError = "Vui lòng chọn hình thức ủng hộ.";
                return View(model);
            }

            try
            {
                // Xử lý upload files với loại minh chứng
                List<FileUploadDto> uploadedFiles = new List<FileUploadDto>();

                if (Files != null && Files.Length > 0)
                {
                    for (int i = 0; i < Files.Length; i++)
                    {
                        var file = Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            // Validate file size (10MB)
                            if (file.ContentLength > 10 * 1024 * 1024)
                            {
                                ViewBag.MessageError = $"File '{file.FileName}' vượt quá kích thước cho phép (10MB).";
                                return View(model);
                            }

                            // Validate file extension
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                            var fileExtension = Path.GetExtension(file.FileName).ToLower();

                            if (!allowedExtensions.Contains(fileExtension))
                            {
                                ViewBag.MessageError = $"File '{file.FileName}' có định dạng không được hỗ trợ.";
                                return View(model);
                            }

                            // Convert file to Base64
                            byte[] fileBytes;
                            using (var binaryReader = new BinaryReader(file.InputStream))
                            {
                                fileBytes = binaryReader.ReadBytes(file.ContentLength);
                            }
                            string base64String = Convert.ToBase64String(fileBytes);

                            // Lấy loại minh chứng từ JavaScript (được gửi qua hidden fields hoặc form data)
                            // Cách 1: Thêm hidden fields trong JS
                            string loaiMinhChung = form[$"LoaiMinhChung_{i}"] ?? string.Empty;

                            // Thêm vào danh sách
                            uploadedFiles.Add(new FileUploadDto
                            {
                                FileName = file.FileName,
                                ContentType = file.ContentType,
                                FileData = base64String,
                                LoaiMinhChung = loaiMinhChung
                            });
                        }
                    }
                }

                // Tạo DTO để gửi API
                var apiDto = new
                {
                    ManhThuongQuanId = model.ManhThuongQuanId,
                    SoTien = model.SoTien,
                    LoaiUngHo = model.LoaiUngHo,
                    DoiTuong = model.DoiTuong,
                    SoLuongVatPham = model.SoLuongVatPham,
                    TenVatPham = model.TenVatPham,
                    NgayUngHo = model.NgayUngHo.Value.ToString("yyyy-MM-dd"),
                    GhiChu = model.GhiChu,
                    Files = uploadedFiles // Mỗi file đã có LoaiMinhChung
                };

                var json = JsonConvert.SerializeObject(apiDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("ManhThuongQuan/LuuThongTinUngHo", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.MessageSuccess = "Lưu thông tin ủng hộ thành công!";
                    ModelState.Clear();
                    return View(new UngHoViewModel());
                }
                else
                {
                    ViewBag.MessageError = $"Lỗi API: {responseBody}";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = "Đã xảy ra lỗi: " + ex.Message;
                return View(model);
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
            if (relust!=null)
            {
                TempData["MessageSuccess"] = "Sửa ủng hộ thành công";
            }
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa ủng hộ mạnh thường quân";
            return RedirectToAction("SuaUngHoManhThuongQuan", new { UngHoId = ttmtq.UngHoId });
        }
        //Xóa Ủng hộ mạnh thường quân
        [HttpPost]
        public async Task<ActionResult> XoaUngHo(int UngHoId, int ManhThuongQuanId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"ManhThuongQuan/XoaUngHo?UngHoId={UngHoId}");
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Xóa ủng hộ thành công";
                TempData["NotificationType"] = "success";
            }

            return RedirectToAction("ChiTietManhThuongQuan", new { ManhThuongQuanId });
        }
    }

}
