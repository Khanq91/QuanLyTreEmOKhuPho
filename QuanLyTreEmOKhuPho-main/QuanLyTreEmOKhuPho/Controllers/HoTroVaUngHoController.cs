using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class HoTroVaUngHoController : Controller
    {
        // GET: HoTroVaUngHo
        public ActionResult HoTroVaUngHo()
        {
            ViewBag.ActivePage = "HoTroVaUngHo";
            ViewBag.PageTitle = "Hỗ Trợ & Phúc Lợi";
            ViewBag.PageDescription = "Quản lý các chương trình hỗ trợ và phúc lợi";
            return View();
        }
    }
}