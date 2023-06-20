using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignInTPMedia.Models
{
    public partial class Baocao
    {
        public int IdbaoCao { get; set; }
        public string? MaNv { get; set; }
        public int? MaKh { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }
        public string? KhtimKiem { get; set; }
        public string? MoTaCv { get; set; }
        public string? KhchamSoc { get; set; }
        public int? Idlh { get; set; }
        public string? DongNghiep { get; set; }
        public string? HocTap { get; set; }
        public string? DuDinh { get; set; }
        public string? VanDe { get; set; }
        public string? GiaiQuyet { get; set; }
        public int? IdkeHoach { get; set; }

        public virtual Kehoach? IdkeHoachNavigation { get; set; }
        public virtual Lienhe? IdlhNavigation { get; set; }
        public virtual Khachhang? MaKhNavigation { get; set; }
    }
}
