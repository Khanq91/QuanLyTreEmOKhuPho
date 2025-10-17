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
        //public async Task<double> TongTienUngHoTheoThang()
        //{
        //    double tongTien = 0;

        //    HttpResponseMessage response = await _client.GetAsync("UngHo/TongTrongThang");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        tongTien = Convert.ToDouble(data);
        //    }

        //    return tongTien;
        //}
        public async Task<ActionResult> SapToi(int? khuPhoId)
        {
            // Gọi API
            string query = khuPhoId.HasValue ? $"?KhuPhoID={khuPhoId.Value}" : "";
            HttpResponseMessage response = await _client.GetAsync("SuKien/SapToi" + query);

            List<SuKien> suKiens = new List<SuKien>();

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                suKiens = JsonConvert.DeserializeObject<List<SuKien>>(data);
            }
            else
            {
                ViewBag.ThongBao = "Không lấy được dữ liệu sự kiện!";
            }

            return View(suKiens);
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

            ////ViewBag thong ke
            ViewBag.TongTreEm = treEms.Count();
            ViewBag.TongSuKien = suKiens.Count();
            ViewBag.TongTNV = tnvs.Count();
            //ViewBag.TongSoTien = await  TongTienUngHoTheoThang();
            ViewBag.KhuPho = khuPhos;

            ViewBag.TongTreEmTungKhuPho = await LayTongTreEmTungKhuPho();

            ViewBag.SuKienSapToi = SapToi(KhuPhoID);
            ViewBag.ActivePage = "TrangChu";
            ViewBag.PageTitle = "Trang Chủ";
            ViewBag.PageDescription = "Tổng quan hệ thống quản lý trẻ em";
            return View();
        }

    }
}
