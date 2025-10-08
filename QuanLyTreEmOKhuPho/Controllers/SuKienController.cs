using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class SuKienController : Controller
    {
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
        public ActionResult ChiTietSuKien()
        {
            ViewBag.ActivePage = "SuKien";
            ViewBag.PageTitle = "Quản Lý Sự Kiện";
            ViewBag.PageDescription = "Tổ chức và theo dõi các hoạt động thiếu nhi tại khu phố";
            return View();
        }
    }
}