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
        [HttpPost]
        public async Task<ActionResult> GhiNhanManhThuongQuan(GhiNhanManhThuongQuan mtq)
        {
            var result = await GhiNhanManhThuongQuan_API(mtq);

            if (result != null)
            {
                ViewBag.MessageSuccess = $"Đã ghi nhận mạnh thường quân: {result.Ten}";
            }
            else
            {
                ViewBag.MessageError = "Ghi nhận thất bại, vui lòng thử lại!";
            }

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Ghi nhận mạnh thường quân";

            return View();
        }
        private async Task<GhiNhanManhThuongQuan> GetManhThuongQuan(int manhThuongQuanId)
        {
            var response = await _client.GetAsync($"ManhThuongQuan/ThongTinManhThuongQuan?ManhThuongQuanID={manhThuongQuanId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var manhThuongQuan = JsonConvert.DeserializeObject<GhiNhanManhThuongQuan>(jsonResponse);

            return manhThuongQuan;
        }
        public async Task<ActionResult> SuaManhThuongQuan(int ManhThuongQuanId)
        {
            var ThongTinManhThuongQuan = await GetManhThuongQuan(ManhThuongQuanId);

            if (ThongTinManhThuongQuan == null)
            {
                return View(new GhiNhanManhThuongQuan());
            }

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa thông tin mạnh thường quân";

            return View(ThongTinManhThuongQuan);  
        }
        [HttpPost]
        public async Task<ActionResult> SuaManhThuongQuan(GhiNhanManhThuongQuan mtq)
        {
            // Kiểm tra dữ liệu đầu vào
            if (mtq == null || mtq.ManhThuongQuanId <= 0)
            {
                TempData["MessageError"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction("ManhThuongQuan", "ManhThuongQuan");
            }

            // Kiểm tra validation
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Vui lòng điền đầy đủ thông tin bắt buộc.";
                return RedirectToAction("SuaManhThuongQuan", new { ManhThuongQuanId = mtq.ManhThuongQuanId });
            }

            try
            {
                var result = await SuaManhThuongQuan_API(mtq);

                if (result != null)
                {
                    TempData["MessageSuccess"] = "Thông tin mạnh thường quân đã được cập nhật thành công!";
                }
                else
                {
                    TempData["MessageError"] = "Cập nhật thông tin thất bại, vui lòng thử lại!";
                }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToAction("SuaManhThuongQuan", new { ManhThuongQuanId = mtq.ManhThuongQuanId });
        }

        //Thêm mạnh thường quân

        //Thong tin chi tiết 1 mạnh thường quân
        public async Task<ActionResult> ChiTietManhThuongQuan(int ManhThuongQuanId)
        {
            ViewBag.ChiTietManhThuongQuan = await ChiTietManhThuongQuan_API(ManhThuongQuanId);
            ViewBag.LichSuUngHo = await LichSuUngHo(ManhThuongQuanId);

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Chi tiết mạnh thường quân";
            return View();
        }
        public async Task<ActionResult> SuaUngHoManhThuongQuan(int UngHoID)
        {
            var model = await LayThongTinUngHo(UngHoID);

            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa ủng hộ mạnh thường quân";
            return View(model);

        }
        [HttpPost]
        public async Task<ActionResult> SuaUngHoManhThuongQuan(SuaThongTinUngHoManhThuongQuan ttmtq)
        {
            var relust = await SuaThongTinUngHo(ttmtq);
            ViewBag.ActivePage = "ManhThuongQuan";
            ViewBag.PageTitle = "Mạnh Thường Quân";
            ViewBag.PageDescription = "Sửa ủng hộ mạnh thường quân";
            return RedirectToAction("SuaUngHoManhThuongQuan", new { UngHoId = ttmtq.UngHoId });
        }
        //Xóa Ủng hộ mạnh thường quân
        [HttpPost]
        public async Task<ActionResult> XoaUngHo(int UngHoId, int ManhThuongQuanId)
        {
            // Gọi API DELETE
            var request = new HttpRequestMessage(HttpMethod.Delete, $"ManhThuongQuan/XoaUngHo?UngHoId={UngHoId}");
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Notification"] = "Xóa ủng hộ thành công";
                TempData["NotificationType"] = "success";
            }

            // Chuyển về chi tiết mạnh thường quân với id đúng
            return RedirectToAction("ChiTietManhThuongQuan", new { ManhThuongQuanId });
        }
    }

}
