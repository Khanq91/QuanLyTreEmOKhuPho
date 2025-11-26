using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class SuKienController : Controller
    {
        private readonly HttpClient _client;

        public SuKienController()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44362/api/")
            };
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateSuKien()
        {
            return View();
        }
        public ActionResult DangKySuKien()
        {
            return View();
        }
        public ActionResult DangKySuKienChoTreEm()
        {
            return View();
        }

        // ====== GỬI CẬP NHẬT (PUT API) ======
        [HttpPost]
        public async Task<ActionResult> SaveEdit(SuKienDetailVM model)
        {
            if (model == null)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction("SuKien");
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"SuKien/{model.SuKienId}/updateAll", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Cập nhật sự kiện thành công!";
                return RedirectToAction("ChiTiet", new { id = model.SuKienId });
            }
            else
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Lỗi khi cập nhật sự kiện: {errMsg}";
                return RedirectToAction("EditSuKien", new { id = model.SuKienId });
            }
        }
        public ActionResult SuKien()
        {
            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Quản Lý Sự Kiện";
            ViewBag.PageDescription = "Tổ chức và theo dõi các hoạt động thiếu nhi tại khu phố";
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var response = await _client.GetAsync("SuKien/all");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<SuKien>>(json);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = true, message = "Không thể lấy dữ liệu từ API" }, JsonRequestBehavior.AllowGet);
        }
   
        [HttpGet]
        public async Task<ActionResult> ChiTiet(int id)
        {
            var response = await _client.GetAsync($"SuKien/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Không lấy được chi tiết sự kiện từ API.";
                return RedirectToAction("SuKien");
            }

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<SuKienDetailVM>(json);


            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Chi tiết sự kiện";
            ViewBag.PageDescription = model?.TenSuKien ?? "Chi tiết";

            return View("ChiTietSuKien", model);
        }

        public async Task<ActionResult> EditSuKien(int id)
        {
            var response = await _client.GetAsync($"SuKien/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Không lấy được thông tin sự kiện để chỉnh sửa.";
                return RedirectToAction("SuKien");
            }

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<SuKienDetailVM>(json);

            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Chỉnh sửa sự kiện";
            ViewBag.PageDescription = model?.TenSuKien ?? "Chỉnh sửa";

            return View("EditSuKien", model);
        }
    }
}
