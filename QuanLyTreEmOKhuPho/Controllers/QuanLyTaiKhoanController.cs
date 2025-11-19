using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.QuanLyTaiKhoan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace QuanLyTreEmOKhuPho.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        private readonly HttpClient _client;
        // GET: QuanLyTaiKhoan
        public QuanLyTaiKhoanController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        //=============================== Tổng hợp tài khoản ===========================
        public async Task<ThongKe> ThongKeTK()
        {
            var response = await _client.GetAsync("QuanLyTaiKhoan/ThongKe");

            if (!response.IsSuccessStatusCode)
                return new ThongKe();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ThongKe>(json);

            return result ?? new ThongKe();
        }
        //============== Danh sách tài khoản ===========================================
        public async Task<List<DanhSachTaiKhoan>> DsTaiKhoan()
        {
            var response = await _client.GetAsync("QuanLyTaiKhoan/DanhSachNguoiDung");

            if (!response.IsSuccessStatusCode)
                return new List<DanhSachTaiKhoan>();

            var json = await response.Content.ReadAsStringAsync();

            // Deserialize đúng kiểu danh sách
            var result = JsonConvert.DeserializeObject<List<DanhSachTaiKhoan>>(json);

            return result ?? new List<DanhSachTaiKhoan>();
        }

        //============== ResetMatKhau ===========================================
        [HttpPost]
        public async Task<ActionResult> ResetMatKhau(int id)
        {
            var response = await _client.PostAsync($"QuanLyTaiKhoan/ResetMatKhau/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Đã reset mật khẩu thành công!";
                TempData["NotificationType"] = "success";
                return RedirectToAction("QuanLyTaiKhoan");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return RedirectToAction("QuanLyTaiKhoan");
            }
        }
        //============== MoKhoaTaiKhoan ===========================================

        [HttpPost]
        public async Task<ActionResult> MoKhoaTaiKhoan(int id)
        {
            var dto = new DoiTrangThaiDto
            {
                TrangThai = "Đang hoạt động" // trạng thái muốn đổi
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"QuanLyTaiKhoan/DoiTrangThai/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Đã mở khóa tài khoản thành công!";
                TempData["NotificationType"] = "success";
            }
            else
            {
                var err = await response.Content.ReadAsStringAsync();
                TempData["Error"] = "Lỗi khi mở khóa: " + err;
            }

            return RedirectToAction("QuanLyTaiKhoan");
        }
        //============== KhoaTiaKhoan ===========================================

        [HttpPost]
        public async Task<ActionResult> KhoaTiaKhoan(int id)
        {
            var dto = new DoiTrangThaiDto
            {
                TrangThai = "Đã bị khóa" // trạng thái muốn đổi
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"QuanLyTaiKhoan/DoiTrangThai/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Đã  khóa tài khoản thành công!";
                TempData["NotificationType"] = "error";
            }
            else
            {
                var err = await response.Content.ReadAsStringAsync();
                TempData["Error"] = "Lỗi khi mở khóa: " + err;
            }

            return RedirectToAction("QuanLyTaiKhoan");
        }
        //============== XoaTaiKhoan ===========================================

        [HttpPost]
        public async Task<ActionResult> XoaTaiKhoan(int id)
        {
            var response = await _client.DeleteAsync($"QuanLyTaiKhoan/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Đã xóa tài khoản thành công!";
                TempData["NotificationType"] = "success";
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                TempData["Notification"] = "Xóa tài khoản thất bại! " + content;
                TempData["NotificationType"] = "error";
            }

            return RedirectToAction("QuanLyTaiKhoan");
        }

        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> QuanLyTaiKhoan()
        {
            ViewBag.ThongKe = await ThongKeTK();
            ViewBag.DsTaiKhoan = await DsTaiKhoan();

            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";
            return View();
        }
        private async Task<(bool IsValid, string Message)> CheckDuLieuClient(string email, string sdt)
        {
            var dto = new
            {
                Email = email,
                SDT = sdt
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("QuanLyTaiKhoan/KiemTraTonTai", content);

            if (!response.IsSuccessStatusCode)
            {
                // Lấy thông báo lỗi từ API
                var errJson = await response.Content.ReadAsStringAsync();
                var errObj = JsonConvert.DeserializeObject<dynamic>(errJson);
                return (false, errObj.message.ToString());
            }

            // Nếu hợp lệ
            return (true, "Dữ liệu hợp lệ, có thể tạo tài khoản.");
        }
        //================== Tạo 1 tài khoản======================================
        public async Task<ActionResult> TaoTaiKhoanThuCong(TaoTaiKhoan tk)
        {
            if (!ModelState.IsValid)
            {
                return View(tk);
            }
            var check = await CheckDuLieuClient(tk.Email, tk.SDT);
            if (!check.IsValid)
            {
                ViewBag.Error = check.Message;
                return View(tk);
            }

            var dto = new
            {
                tk.HoTen,
                tk.Email,
                tk.SDT,
                tk.VaiTro,
                tk.MatKhau,
                tk.TrangThai,
                tk.Anh,
                NgayTao = tk.NgayTao.ToString("yyyy-MM-dd")
            };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Gọi API tạo tài khoản
            var postResponse = await _client.PostAsync("QuanLyTaiKhoan/ThemTaiKhoan", content);

            if (postResponse.IsSuccessStatusCode)
            {
                ViewBag.Message = "Tạo tài khoản thành công!";
                return View();
            }
            else
            {
                var err = await postResponse.Content.ReadAsStringAsync();
                ViewBag.Error = err;
                return View(tk);
            }
        }
        //================== Thông tin tài khoản======================================
        public async Task<TTTaiKhoan> ThongTinTaiKhoan(int UserId)
        {
            var response = await _client.GetAsync($"QuanLyTaiKhoan/{UserId}");
            if (!response.IsSuccessStatusCode)
            {
                return new TTTaiKhoan();
            }
            var jsonString = await response.Content.ReadAsStringAsync();
            var ttNguoiDung = JsonConvert.DeserializeObject<TTTaiKhoan>(jsonString);
            return ttNguoiDung;
        }

        //================ Sửa thông tin người dùng =========================================
        public async Task<ActionResult> SuaThongTinTaiKhoan(int UserId)
        {
            ViewBag.TTNguoiDung = await ThongTinTaiKhoan(UserId);
            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SuaThongTinTaiKhoan(TTTaiKhoan taikhoan)
        {

            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";
            var json = JsonConvert.SerializeObject(taikhoan);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"QuanLyTaiKhoan/SuaTaiKhoan/{taikhoan.UserId}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                dynamic errObj = JsonConvert.DeserializeObject(errorJson);
                TempData["Error"] = errObj?.message?.ToString() ?? "Cập nhật thất bại!";
                ViewBag.TTNguoiDung = taikhoan;
                return View(taikhoan);
            }
            TempData["Success"] = "Cập nhật tài khoản thành công!";
            return RedirectToAction("SuaThongTinTaiKhoan", new { UserId = taikhoan.UserId });
        }
        //================ Tạo tài khoản cho người dùng======================================
        public async Task<ActionResult> TaoTaiKhoan()
        {
            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";
            return View();
        }
        //================== Tạo 1 tài khoản - CẢI TIẾN ======================================
        [HttpPost]
        public async Task<ActionResult> TaoTaiKhoan(TaoTaiKhoan tk)
        {
            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng điền đầy đủ thông tin!";
                return View(tk); // Trả về model để giữ dữ liệu
            }

            var check = await CheckDuLieuClient(tk.Email, tk.SDT);
            if (!check.IsValid)
            {
                ViewBag.Error = check.Message;
                return View(tk); // Trả về model để giữ dữ liệu
            }

            var dto = new
            {
                tk.HoTen,
                tk.Email,
                tk.SDT,
                tk.VaiTro,
                tk.MatKhau,
                tk.TrangThai,
                tk.Anh,
                NgayTao = tk.NgayTao.ToString("yyyy-MM-dd")
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var postResponse = await _client.PostAsync("QuanLyTaiKhoan/ThemTaiKhoan", content);

            if (postResponse.IsSuccessStatusCode)
            {
                ViewBag.Message = "Tạo tài khoản thành công!";
                ModelState.Clear(); // Xóa ModelState để form rỗng
                return View(new TaoTaiKhoan()); // Trả về model rỗng
            }
            else
            {
                var err = await postResponse.Content.ReadAsStringAsync();
                ViewBag.Error = err;
                return View(tk); // Trả về model có dữ liệu khi lỗi
            }
        }
        [HttpPost]
        public async Task<ActionResult> ImportExcel(HttpPostedFileBase excelFile, string validDataJson)
        {
            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";

            try
            {
                // Kiểm tra file có được chọn không
                if (excelFile == null || excelFile.ContentLength == 0)
                {
                    ViewBag.ImportError = "Vui lòng chọn file Excel để import!";
                    // GIỮ LẠI dữ liệu đã validate
                    ViewBag.PreservedData = validDataJson;
                    return View("TaoTaiKhoan");
                }

                // Kiểm tra định dạng file
                var allowedExtensions = new[] { ".xlsx", ".xls" };
                var fileExtension = System.IO.Path.GetExtension(excelFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ViewBag.ImportError = "File không đúng định dạng! Chỉ chấp nhận file .xlsx hoặc .xls";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                // Kiểm tra dữ liệu JSON
                if (string.IsNullOrWhiteSpace(validDataJson))
                {
                    ViewBag.ImportError = "Không có dữ liệu hợp lệ để import!";
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                // Parse JSON thành danh sách tài khoản
                List<TaoTaiKhoan> danhSachTaiKhoan = null;
                try
                {
                    danhSachTaiKhoan = JsonConvert.DeserializeObject<List<TaoTaiKhoan>>(validDataJson);
                }
                catch (JsonException jsonEx)
                {
                    ViewBag.ImportError = $"Lỗi parse dữ liệu JSON: {jsonEx.Message}";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                if (danhSachTaiKhoan == null || danhSachTaiKhoan.Count == 0)
                {
                    ViewBag.ImportError = "Danh sách tài khoản trống!";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                // Chuẩn bị DTO để validate
                var validateList = danhSachTaiKhoan.Select(tk => new
                {
                    HoTen = tk.HoTen?.Trim() ?? "",
                    Email = tk.Email?.Trim() ?? "",
                    SDT = tk.SDT?.Trim() ?? "",
                    VaiTro = tk.VaiTro?.Trim() ?? "",
                    MatKhau = "140letrongtan",
                    TrangThai = "Đang hoạt động",
                    Anh = "/Anh/NguoiDung/macdinh.jpg",
                    NgayTao = DateTime.Now.ToString("yyyy-MM-dd")
                }).ToList();

                var validateJson = JsonConvert.SerializeObject(validateList);
                var validateContent = new StringContent(validateJson, Encoding.UTF8, "application/json");

                // Gọi API để validate
                HttpResponseMessage validateResponse = null;
                try
                {
                    validateResponse = await _client.PostAsync("QuanLyTaiKhoan/ValidateDanhSachTaiKhoan", validateContent);
                }
                catch (HttpRequestException httpEx)
                {
                    ViewBag.ImportError = $"Lỗi kết nối API: {httpEx.Message}";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                var validateResultContent = await validateResponse.Content.ReadAsStringAsync();

                if (!validateResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(validateResultContent);
                        string errorMessage = errorObj?.message?.ToString() ?? validateResultContent;
                        ViewBag.ImportError = $"Lỗi validate: {errorMessage}";
                    }
                    catch
                    {
                        ViewBag.ImportError = $"Lỗi validate: {validateResultContent}";
                    }
                    // GIỮ LẠI dữ liệu
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                // Parse validation result
                dynamic validationData = null;
                try
                {
                    validationData = JsonConvert.DeserializeObject<dynamic>(validateResultContent);
                }
                catch (JsonException jsonEx)
                {
                    ViewBag.ImportError = $"Lỗi parse response từ API: {jsonEx.Message}";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                if (validationData?.chiTiet == null)
                {
                    ViewBag.ImportError = "API không trả về dữ liệu chi tiết validation!";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                var chiTiet = validationData.chiTiet;
                var hasErrors = false;
                var errorMessages = new List<string>();

                foreach (var item in chiTiet)
                {
                    if (item.errors != null && item.errors.Count > 0)
                    {
                        hasErrors = true;
                        int rowIndex = (int)item.index + 1;
                        var errors = new List<string>();
                        foreach (var err in item.errors)
                        {
                            errors.Add(err.ToString());
                        }
                        errorMessages.Add($"Dòng {rowIndex}: {string.Join(", ", errors)}");
                    }
                }

                if (hasErrors)
                {
                    ViewBag.ImportError = "Có lỗi trong dữ liệu:<br/>" + string.Join("<br/>", errorMessages);
                    // GIỮ LẠI dữ liệu
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                // Chuẩn bị dữ liệu để import
                var importDto = danhSachTaiKhoan.Select(tk => new
                {
                    HoTen = tk.HoTen?.Trim(),
                    Email = tk.Email?.Trim(),
                    SDT = tk.SDT?.Trim(),
                    VaiTro = tk.VaiTro?.Trim(),
                    MatKhau = string.IsNullOrWhiteSpace(tk.MatKhau) ? "140letrongtan" : tk.MatKhau.Trim(),
                    TrangThai = "Đang hoạt động",
                    Anh = "/Anh/NguoiDung/macdinh.jpg",
                    NgayTao = DateTime.Now.ToString("yyyy-MM-dd"),
                }).ToList();

                var importJson = JsonConvert.SerializeObject(importDto);
                var importContent = new StringContent(importJson, Encoding.UTF8, "application/json");

                // Gọi API để import
                HttpResponseMessage importResponse = null;
                try
                {
                    importResponse = await _client.PostAsync("QuanLyTaiKhoan/ThemNhieuTaiKhoan", importContent);
                }
                catch (HttpRequestException httpEx)
                {
                    ViewBag.ImportError = $"Lỗi kết nối API khi import: {httpEx.Message}";
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                    return View("TaoTaiKhoan");
                }

                var importResultContent = await importResponse.Content.ReadAsStringAsync();

                if (importResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var resultObj = JsonConvert.DeserializeObject<dynamic>(importResultContent);
                        ViewBag.ImportMessage = resultObj?.message?.ToString() ?? "Import thành công!";
                        // KHÔNG GIỮ dữ liệu khi thành công
                        ViewBag.PreservedData = null;
                        ViewBag.PreservedFileName = null;
                    }
                    catch
                    {
                        ViewBag.ImportMessage = "Import thành công!";
                        ViewBag.PreservedData = null;
                        ViewBag.PreservedFileName = null;
                    }
                }
                else
                {
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(importResultContent);
                        ViewBag.ImportError = $"Lỗi khi import: {errorObj?.message?.ToString() ?? importResultContent}";
                    }
                    catch
                    {
                        ViewBag.ImportError = $"Lỗi khi import: {importResultContent}";
                    }
                    // GIỮ LẠI dữ liệu khi import thất bại
                    ViewBag.PreservedData = validDataJson;
                    ViewBag.PreservedFileName = excelFile.FileName;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ImportError = $"Lỗi hệ thống: {ex.Message}";
                ViewBag.PreservedData = validDataJson;
            }

            return View("TaoTaiKhoan");
        }

    } 

}
