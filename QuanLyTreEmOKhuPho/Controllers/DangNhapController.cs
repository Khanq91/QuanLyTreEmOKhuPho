using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace QuanLyTreEmOKhuPho.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly HttpClient _client;
        //QuanLyTreEmDataContext db = new QuanLyTreEmDataContext();

        public DangNhapController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/"); // cổng API
        }
        //GET: DangNhap
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> DangNhap(string tk, string mk)
        {
 

            string regex  = @"^(0|\+84)\d{9,10}$";
            if (string.IsNullOrEmpty(tk) )
            {
                ViewBag.ThongBao = "Tài khoản không để trống!";
                return View();
            }
            else if( !Regex.IsMatch(tk, regex))
            {
                ViewBag.ThongBao = "Tài khoản không đúng định dạng!";
                return View();
            }
            else if (string.IsNullOrEmpty(mk))
            {
                ViewBag.ThongBao = "Mật khẩu đang trống!";
                return View();
            }
            var NguoiDung = new NguoiDung { TaiKhoan = tk, MatKhau = mk };
            string json = JsonConvert.SerializeObject(NguoiDung);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.GetAsync($"NguoiDung/Login?SDT={tk}&MatKhau={mk}");
            if (response.IsSuccessStatusCode) {
                string result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);
                Session["TenHienThi"] = data.TenHienThi;
                return RedirectToAction("TrangChu", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác!";
                return View();

            }
        }
    }
}