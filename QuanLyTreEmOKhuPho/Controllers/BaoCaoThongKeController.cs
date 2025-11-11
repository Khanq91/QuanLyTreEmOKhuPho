using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class BaoCaoThongKeController : Controller
    {
        // GET: BaoCaoThongKe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BaoCaoThongKe()
        {
            ViewBag.PageTitle = "Báo cáo & Thống kê";
            ViewBag.PageDescription = "Xem báo cáo và thống kê hệ thống";
            ViewBag.ActivePage = "BaoCaoThongKe";
            return View();
        }
    }
}