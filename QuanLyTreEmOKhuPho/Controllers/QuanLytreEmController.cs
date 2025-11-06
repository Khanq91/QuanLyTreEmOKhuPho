using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class QuanLyTreEmController : Controller
    {
        private readonly string _apiBaseUrl = "https://localhost:44362/api/TreEm/";


        public async Task<ActionResult> TreEm()
        {
            ViewBag.ActivePage = "TreEm";
            ViewBag.PageTitle = "Quản Lý Trẻ Em";
            ViewBag.PageDescription = "Quản lý thông tin trẻ em trong khu phố";

            try
            {
                var treEmList = await GetTreEmListAsync();
                return View(treEmList ?? new List<TreEmViewModel>());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi tải danh sách trẻ em.";
                return View(new List<TreEmViewModel>());
            }
        }
        public async Task<ActionResult> ChiTiet(int id)
        {
            if (id <= 0) return HttpNotFound("ID không hợp lệ");


            try
            {
                var tre = await GetTreEmDetailAsync(id);
                if (tre == null) return HttpNotFound($"Không tìm thấy trẻ với ID = {id}");


                ViewBag.TreEmId = id;
                return View("ChiTiet", tre); // → truyền sang view dạng dynamic hoặc ViewModel mở rộng
            }
            catch
            {
                return new HttpStatusCodeResult(500, "Lỗi khi tải chi tiết trẻ em");
            }
        }

        private async Task<List<TreEmViewModel>> GetTreEmListAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("DanhSach");
                response.EnsureSuccessStatusCode(); // sẽ throw nếu lỗi HTTP

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TreEmViewModel>>(json);
            }
        }

        private async Task<TreEmViewModel> GetTreEmDetailAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{id}");
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TreEmViewModel>(json);
            }
        }
        public async Task<ActionResult> Sua(int id)
        {
            if (id <= 0) return HttpNotFound("ID không hợp lệ");

            try
            {
                var tre = await GetTreEmDetailAsync(id);
                if (tre == null) return HttpNotFound($"Không tìm thấy trẻ với ID = {id}");

                ViewBag.IsEditMode = true; 
                return View("ChiTiet", tre); 
            }
            catch
            {
                return new HttpStatusCodeResult(500, "Lỗi khi tải chi tiết trẻ em");
            }
        }

        public ActionResult ThemTreEm_v1()
        {
            return View();
        }

        public ActionResult ThemTreEm_v2()
        {
            return View();
        }

        public ActionResult ThemTreEm_v3()
        {
            return View();
        }

     
    }
}
