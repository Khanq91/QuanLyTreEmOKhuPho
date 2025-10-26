using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.TinhNguyenVien;
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
        public async Task<List<ThongKeTinhNguyenVien>> ThongKeTinhNguyenVien(int? khuPhoID )
        {
            string url = "TinhNguyenVien/GetThongKeTinhNguyenVien";
            if (khuPhoID.HasValue)
                url += $"?KhuPhoID={khuPhoID.Value}";

            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new List<ThongKeTinhNguyenVien>();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ThongKeTinhNguyenVien>>(json);
            return result;
        }
        public async Task<ThongKeTinhNguyenVien> fun_ThongTinChiTietTinhNguyenVien(int? UserID)
        {
            if (!UserID.HasValue)
                return new ThongKeTinhNguyenVien();

            string url = $"TinhNguyenVien/ChiTietThongTinTinhNguyenVien?UserID={UserID.Value}";

            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new ThongKeTinhNguyenVien();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ThongKeTinhNguyenVien>(json);

            return result ?? new ThongKeTinhNguyenVien();
        }
        public async Task<List<LichRanhTNV>> LichRanhTNV(int? UserID)
        {
            string url = $"TinhNguyenVien/GetLichRanhTinhNguyenVien?UserID={UserID.Value}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
               return new List<LichRanhTNV>();
            }
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LichRanhTNV>>(json);
            return result;
        }
        public async Task<List<SuKienDaThamGia>> LaySuKienDaThamGia(int? UserID)
        {
            string url = $"TinhNguyenVien/GetSuKienDaThamGia?UserID={UserID}";
            HttpResponseMessage response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<SuKienDaThamGia>();

            string json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<SuKienDaThamGia>>(json);
            return result;
        }
        public async Task<List<HoTroVanDong>> DSTNV_HoTroVanDong(int? UserID)
        {
            string url = $"TinhNguyenVien/GetHoTroVanDong?userId={UserID.Value}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<HoTroVanDong>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HoTroVanDong>>(json);

            return result;
        }


        //  Controller chính
        public async Task<ActionResult> TinhNguyenVien(int? KhuPhoID)
        {
            List<KhuPho> khuPhos = await GetDataAsync<KhuPho>("KhuPho");
            List<ThongKeTinhNguyenVien> tnvs = await ThongKeTinhNguyenVien(KhuPhoID);

            ViewBag.listTNV = tnvs;
            ViewBag.KhuPho = khuPhos;

            ViewBag.ActivePage = "TinhNguyenVien";
            ViewBag.PageTitle = "Tình Nguyện Viên";
            ViewBag.PageDescription = "Quản lý thông tin tình nguyện viên";
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> TinhNguyenVien(string search, int? KhuPhoID, string ChucVu, string TrangThai)
        {
            List<KhuPho> khuPhos = await GetDataAsync<KhuPho>("KhuPho");
            List<ThongKeTinhNguyenVien> tnvs = await ThongKeTinhNguyenVien(KhuPhoID);
            if (!string.IsNullOrEmpty(search))
            {
                string keyword = search.ToLower();
                tnvs = tnvs.Where(t =>
                    (!string.IsNullOrEmpty(t.TenTinhNguyenVien) && t.TenTinhNguyenVien.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(t.SDT) && t.SDT.Contains(search))
                ).ToList();
            }
            if (KhuPhoID.HasValue)
            {
                tnvs = tnvs.Where(t => t.KhuPhoID == KhuPhoID).ToList();
            }
            if (!string.IsNullOrEmpty(ChucVu))
            {
                tnvs = tnvs.Where(t => t.ChucVu == ChucVu).ToList();
            }
            if (!string.IsNullOrEmpty(TrangThai))
            {
                tnvs = tnvs.Where(t => t.TrangThai == TrangThai).ToList();
            }
            ViewBag.listTNV = tnvs;
            ViewBag.KhuPho = khuPhos;

            ViewBag.ActivePage = "TinhNguyenVien";
            ViewBag.PageTitle = "Tình Nguyện Viên";
            ViewBag.PageDescription = "Quản lý thông tin tình nguyện viên";
            return View(tnvs);
        }
        [HttpGet]
        public async Task<ActionResult> ThongTinChiTietTinhNguyenVien(int? UserID)
        {
            ViewBag.TTChiTietTinhNguyenVien = await fun_ThongTinChiTietTinhNguyenVien(UserID);
            ViewBag.LichRanhTNV= await LichRanhTNV(UserID);
            ViewBag.SuKienDaThamGia = await LaySuKienDaThamGia(UserID);
            ViewBag.DSTNV_HoTroVanDong = await DSTNV_HoTroVanDong(UserID);
            return View() ;
        }

    }
}
