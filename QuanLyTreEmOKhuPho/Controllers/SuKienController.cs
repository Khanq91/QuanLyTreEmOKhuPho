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
    public class SuKienController : Controller
    {
        private readonly HttpClient _client;
        public SuKienController()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        // GET: SuKien
        public ActionResult Index()
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
        public ActionResult ThemSuKien()
        {
            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Quản Lý Sự Kiện";
            ViewBag.PageDescription = "Tổ chức và theo dõi các hoạt động thiếu nhi tại khu phố";
            return View();
        }
        public async Task<TTCB_ChiTietSuKien> TTCB_ChiTietSuKien(int suKienId)
        {
            string query = $"?SuKienID={suKienId}";
            HttpResponseMessage response = await _client.GetAsync("SuKien/TTCB_ChiTietSuKien" + query);

            if (!response.IsSuccessStatusCode)
                return null;

            string data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TTCB_ChiTietSuKien>(data);

            return result;
        }
        public async Task<List<TTCT_ChiTietSuKien>> TTCT_ChiTietSuKien(int suKienId)
        {
            string query = $"?SuKienID={suKienId}";
            HttpResponseMessage response = await _client.GetAsync("SuKien/TTCT_ChiTietSuKien" + query);

            if (!response.IsSuccessStatusCode)
                return null;

            string data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TTCT_ChiTietSuKien>>(data)
                 ?? new List<TTCT_ChiTietSuKien>();

            return result;
        }
        public async Task<List<TT_ThanhVienSuKien>> LayThanhVienSuKien_TNV(int suKienId)
        {
            string query = $"?SuKienID={suKienId}";
            HttpResponseMessage response = await _client.GetAsync("SuKien/TT_ThanhVienSuKien_TNV" + query);

            if (!response.IsSuccessStatusCode)
                return new List<TT_ThanhVienSuKien>();

            string data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TT_ThanhVienSuKien>>(data)
                         ?? new List<TT_ThanhVienSuKien>();
            return result;
        }
        public async Task<List<TT_ThanhVienSuKien>> LayThanhVienSuKien_PhuHuynhTreEm(int suKienId)
        {
            string query = $"?SuKienID={suKienId}";
            HttpResponseMessage response = await _client.GetAsync("SuKien/TT_ThanhVienSuKienPhuHuynh" + query);

            if (!response.IsSuccessStatusCode)
                return new List<TT_ThanhVienSuKien>();

            string data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TT_ThanhVienSuKien>>(data)
                         ?? new List<TT_ThanhVienSuKien>();
            return result;
        }
        public async Task<bool> CapNhatThongTin_TrangThai(int SuKienID,int UserID,string TrangThai)
        {
            var query = $"SuKien/CapNhatTrangThai?SuKienID={SuKienID}&UserID={UserID}&TrangThai={TrangThai}";
            HttpResponseMessage response = await _client.GetAsync("SuKien/CapNhatTrangThai_TNV" + query);
            response = await _client.PutAsync(query,null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<ActionResult> ChiTietSuKien(int SuKienID)
        {
            //Thông tin cơ bản
            ViewBag.TTCB_ChiTietSuKien = await TTCB_ChiTietSuKien(SuKienID);
            ViewBag.TTCT_ChiTietSuKien = await TTCT_ChiTietSuKien(SuKienID);

            //Danh sách tìn nguyện viên tham gia sự kiện
            var LayThanhVienSuKien = await LayThanhVienSuKien_TNV(SuKienID);
            ViewBag.LayThanhVienSuKien = LayThanhVienSuKien;
            ViewBag.TongSL_TNV = LayThanhVienSuKien.Count();

            //Danh sách phụ huynh trẻ em  tham gia sự kiện
            var DS_PhuHuynh= await LayThanhVienSuKien_PhuHuynhTreEm(SuKienID);
            ViewBag.LayThanhVienSuKien_PhuHuynhTreEm = DS_PhuHuynh;
            ViewBag.TongSL_PhuHuynh = DS_PhuHuynh.Count();

            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Quản Lý Sự Kiện";
            ViewBag.PageDescription = "Tổ chức và theo dõi các hoạt động thiếu nhi tại khu phố";
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CapNhatTrangThai(int suKienId, int userId, string trangThai)
        {
            bool is_UpdateTNV= await CapNhatThongTin_TrangThai(suKienId, userId, trangThai);
            return RedirectToAction("ChiTietSuKien", new { SuKienID = suKienId });
        }

    }
}