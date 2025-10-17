using QuanLyTreEmOKhuPho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
namespace QuanLyTreEmOKhuPho.Controllers
{
    public class TinhNguyenVienController : Controller
    {
        //QuanLyTreEmDataContext db = new QuanLyTreEmDataContext();
        // GET: TinhNguyenVien
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TinhNguyenVien()
        {
            //var listKhuPho = db.KhuPhos.ToList();
            //ViewBag.KhuPho = listKhuPho;


            //// truy xuất thông tin tình nguyện viên
            //var listTNV = db.TinhNguyenViens
            //.Select(tnv => new TT_TNV
            //{
            //    ID = tnv.TinhNguyenVienID,
            //    TenTinhNguyenVien = tnv.NguoiDung.HoTen,
            //    ChucVu = tnv.ChucVu,
            //    KhuPho = tnv.KhuPho.TenKhuPho,
            //    SDT = tnv.SDT,
            //    CaRanh = tnv.LichTrongs
            //                .SelectMany(l => l.ChiTietLichTrongs)
            //                .Count(),
            //    SuKien = db.PhanCongTinhNguyenViens
            //                .Where(p => p.TinhNguyenVienID == tnv.TinhNguyenVienID)
            //                .Select(p => p.SuKienID)
            //                .Distinct()
            //                .Count(),
            //    TrangThai = "Hoạt động"
            //})
            //.AsEnumerable() // Chuyển sang LINQ to Objects
            //.OrderBy(t => t.TenTinhNguyenVien,
            //         StringComparer.Create(new System.Globalization.CultureInfo("vi-VN"), false))
            //.ToList();

            //ViewBag.listTNV = listTNV;

            //ViewBag.ActivePage = "TinhNguyenVien";
            //ViewBag.PageTitle = "Tình Nguyện Viên";
            //ViewBag.PageDescription = "Quản lý thông tin tình nguyện viên";
            return View();
        }
        //public ActionResult Detail(int id)
        //{
        //    //var tnv = db.TinhNguyenViens.FirstOrDefault(x => x.TinhNguyenVienID == id);
        //    //if (tnv == null) return HttpNotFound();
        //    //return PartialView("_DetailModal", tnv);
        //}
    }
}