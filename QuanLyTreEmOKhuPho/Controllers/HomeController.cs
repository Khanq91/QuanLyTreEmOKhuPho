using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace QuanLyTreEmOKhuPho.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public  HomeController()
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
        public async Task<double> LayTongTienTrongThang()
        {
            HttpResponseMessage response = await _client.GetAsync("UngHo/TongTienUngHoTrongThang");
            if (!response.IsSuccessStatusCode)
                return 0;

            string data = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(data);
        }


        public async Task<List<SuKien>> LaySuKienSapToiAsync(int? khuPhoId)
        {
            string query = khuPhoId.HasValue ? $"?KhuPhoID={khuPhoId.Value}" : "";
            HttpResponseMessage response = await _client.GetAsync("SuKien/SapToi" + query);

            if (!response.IsSuccessStatusCode)
                return new List<SuKien>();

            string data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SuKien>>(data) ?? new List<SuKien>();
        }

        public async Task<List<TreEmTheoKhuPho>> LayTongTreEmTungKhuPho()
        {
            HttpResponseMessage response = await _client.GetAsync("TreEm/TongTreEmTheoKhuPho");
            if (!response.IsSuccessStatusCode)
                return new List<TreEmTheoKhuPho>();

            string data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TreEmTheoKhuPho>>(data)
                         ?? new List<TreEmTheoKhuPho>();
            return result;
        }

        public async Task<ActionResult> TrangChu(int? KhuPhoID)
        {
            List<KhuPho> khuPhos = await GetDataAsync<KhuPho>("KhuPho");
            List<TreEm> treEms = await GetDataAsync<TreEm>("TreEm");
            List<SuKien> suKiens = await GetDataAsync<SuKien>("SuKien");
            List<TT_TNV> tnvs = await GetDataAsync<TT_TNV>("TinhNguyenVien");
            if (KhuPhoID.HasValue)
            {
                treEms = treEms.Where(t => t.KhuPhoID == KhuPhoID.Value).ToList();
                suKiens = suKiens.Where(s => s.khuPhoID == KhuPhoID.Value).ToList();
                tnvs = tnvs.Where(t => t.KhuPhoID == KhuPhoID.Value).ToList();
            }

            ////ViewBag thong ke
            ViewBag.TongTreEm = treEms.Count();
            ViewBag.TongSuKien = suKiens.Count();
            ViewBag.TongTNV = tnvs.Count();
            ViewBag.TongSoTien = await LayTongTienTrongThang();
            ViewBag.KhuPho = khuPhos;

            ViewBag.TongTreEmTungKhuPho = await LayTongTreEmTungKhuPho();

            ViewBag.SuKienSapToi = await LaySuKienSapToiAsync(KhuPhoID);
            ViewBag.ActivePage = "TrangChu";
            ViewBag.PageTitle = "Trang Chủ";
            ViewBag.PageDescription = "Tổng quan hệ thống quản lý trẻ em";
            return View();
        }

    }
}
