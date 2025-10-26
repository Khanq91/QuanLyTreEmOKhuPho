using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        // GET: QuanLyTaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult QuanLyTaiKhoan()
        {
            ViewBag.ActivePage = "QuanLyTaiKhoan";
            ViewBag.PageTitle = "Quản Trị Hệ Thống";
            ViewBag.PageDescription = "Cấu hình và quản lý tài khoản";
            return View();
        }
    }
}