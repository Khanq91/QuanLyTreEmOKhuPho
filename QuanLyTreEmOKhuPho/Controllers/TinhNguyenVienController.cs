using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class TinhNguyenVienController : Controller
    {
        // GET: TinhNguyenVien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TinhNguyenVien()
        {
            ViewBag.ActivePage = "TinhNguyenVien";
            ViewBag.PageTitle = "Tình Nguyện Viên";
            ViewBag.PageDescription = "Quản lý thông tin tình nguyện viên";
            return View();
        }
    }
}