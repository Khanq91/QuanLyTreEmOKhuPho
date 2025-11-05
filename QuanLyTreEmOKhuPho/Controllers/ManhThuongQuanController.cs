using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class ManhThuongQuanController : Controller
    {
        // GET: ManhThuongQuan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManhThuongQuan()
        {
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Quản lý thông tin nhà tài trợ";
            return View();
        }
    }
}