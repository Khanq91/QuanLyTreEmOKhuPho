using Newtonsoft.Json;
using QuanLyTreEmOKhuPho.Models;
using QuanLyTreEmOKhuPho.Models.PhanCum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTreEmOKhuPho.Controllers
{
    public class PhanCumController : Controller
    {
        private readonly HttpClient _client;
        public PhanCumController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44362/api/");
        }
        // GET: PhanCum
        public async Task<ClusteringResponse> GetAllChildrenAnalysisAsync()
        {
            var response = await _client.GetAsync("clustering/analyze-all");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ClusteringResponse>(json);
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> PhanCum()
        {
            var clusteringResponse = await GetAllChildrenAnalysisAsync();
            ViewBag.GetAllChildrenAnalysisAsync = clusteringResponse;
            ViewBag.ChildrenJsonData = JsonConvert.SerializeObject(
                clusteringResponse.Data.Select(item => new
                {
                    treEmId = item.TreEmId,
                    hoTen = item.TenTre,
                    ngaySinh = item.NgaySinh.ToString("yyyy-MM-dd"),
                    gioiTinh = item.GioiTinh,
                    cluster = item.Cluster,
                    priorityLevel = item.PriorityLevel,
                    priorityRank = item.PriorityRank,
                    confidence = item.Confidence / 100.0,
                    color = item.Color,
                    features = new
                    {
                        hocTap = item.Features.HocTap,
                        hanhVi = item.Features.HanhVi,
                        tamLy = item.Features.TamLy,
                        giaDinh = item.Features.GiaDinh,
                        nguyCoBoHoc = item.Features.NguyCoBoHoc
                    },
                    recommendations = item.Recommendations
                }).ToList()
            );
            ViewBag.ActivePage = "PhanCum";
            ViewBag.PageTitle = "Phân cụm trẻ em theo mức độ ưu tiên";
            ViewBag.PageDescription = "Trang này hiển thị kết quả phân cụm trẻ em dựa trên các chỉ số học tập, hành vi, tâm lý và hoàn cảnh gia đình, giúp quản lý đánh giá mức độ ưu tiên hỗ trợ.";
            return View();
        }

    }
}