using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class QuanLytreEmController : Controller
    {
        // GET: QuanLytreEm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TreEm()
        {
            ViewBag.ActivePage = "TreEm";
            ViewBag.PageTitle = "Quản Lý Trẻ Em";
            ViewBag.PageDescription = "Quản lý thông tin trẻ em trong khu phố";
            return View();
        }
        public ActionResult ThemTreEm_v1()
        {
            ViewBag.ActivePage = "TreEm";
            ViewBag.PageTitle = "Quản Lý Trẻ Em";
            ViewBag.PageDescription = "Quản lý thông tin trẻ em trong khu phố";
            return View();
        }
        public ActionResult ThemTreEm_v2()
        {
            ViewBag.ActivePage = "TreEm";
            ViewBag.PageTitle = "Quản Lý Trẻ Em";
            ViewBag.PageDescription = "Quản lý thông tin trẻ em trong khu phố";
            return View();
        }
        public ActionResult ThemTreEm_v3()
        {
            ViewBag.ActivePage = "TreEm";
            ViewBag.PageTitle = "Quản Lý Trẻ Em";
            ViewBag.PageDescription = "Quản lý thông tin trẻ em trong khu phố";
            return View();
        }
    }
}