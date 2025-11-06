using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

            // Truyền thêm meta cho header trang
            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Chi tiết sự kiện";
            ViewBag.PageDescription = model?.TenSuKien ?? "Chi tiết";

            return View("ChiTietSuKien", model);
        }
    }
}
