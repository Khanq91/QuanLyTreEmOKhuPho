using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTreEmOKhuPho.Models.PhanCum
{
    public class ClusteringResponse
    {
        public int Total { get; set; }
        public List<TreEmWithPriorityDto> Data { get; set; }
    }

    // Models/AI/TreEmWithPriorityDto.cs
    public class TreEmWithPriorityDto
    {
        public int TreEmId { get; set; }
        public string TenTre { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public int Cluster { get; set; }
        public string PriorityLevel { get; set; }
        public int PriorityRank { get; set; }
        public float Confidence { get; set; }
        public string Color { get; set; }
        public List<string> Recommendations { get; set; }
        public TreEmFeaturesDto Features { get; set; }
    }

    // Models/AI/TreEmFeaturesDto.cs
    public class TreEmFeaturesDto
    {
        public int Tre_em_id { get; set; }
        public float HocTap { get; set; }
        public float HanhVi { get; set; }
        public float TamLy { get; set; }
        public float GiaDinh { get; set; }
        public float NguyCoBoHoc { get; set; }
    }
}