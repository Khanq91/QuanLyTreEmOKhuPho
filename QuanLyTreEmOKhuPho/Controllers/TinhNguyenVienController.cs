using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace QuanLyTreEmOKhuPho.Controllers
{
    public class TinhNguyenVienController : Controller
    {
        private readonly HttpClient _client;
        // GET: TinhNguyenVien
        public ActionResult Index()
        {
            return View();
        }
        public TinhNguyenVienController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        public async Task<List<T>> GetDataAsync<T>(string endpoint)
        {
            List<T> result = new List<T>();
            HttpResponseMessage response = await _client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return result;
        }
        public async Task<ActionResult> TinhNguyenVien(int? KhuPhoID)
        {
            List<KhuPho> khuPhos = await GetDataAsync<KhuPho>("KhuPho");
            List<TT_TNV> tnvs = await GetDataAsync<TT_TNV>("TinhNguyenVien");

            if (KhuPhoID.HasValue)
            {
                tnvs = tnvs.Where(t => t.KhuPhoID == KhuPhoID.Value).ToList();
            }
            ViewBag.listTNV = tnvs;
            ViewBag.KhuPho = khuPhos;

            ViewBag.ActivePage = "TinhNguyenVien";
            ViewBag.PageTitle = "Tình Nguyện Viên";
            ViewBag.PageDescription = "Quản lý thông tin tình nguyện viên";
            return View();
        }
    }
}