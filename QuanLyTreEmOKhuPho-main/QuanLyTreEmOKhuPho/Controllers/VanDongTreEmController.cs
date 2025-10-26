using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class VanDongTreEmController : Controller
    {
        // GET: VanDongTreEm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VanDongTreEm()
        {
            ViewBag.ActivePage = "VanDongTreEm";
            ViewBag.PageTitle = "Vận Động Trẻ";
            ViewBag.PageDescription = "Quản lý hoạt động vận động trẻ em";
            return View();
        }
    }
}