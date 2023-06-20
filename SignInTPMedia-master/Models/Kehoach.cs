using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignInTPMedia.Models
{
    public partial class Kehoach
    {
        public Kehoach()
        {
            Baocaos = new HashSet<Baocao>();
        }

        public int IdkeHoach { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }
        public string? MoTaCv { get; set; }
        public string? MaNv { get; set; }
        public int? MaKh { get; set; }
        public string? KhchamSoc { get; set; }
        public int? SoKhtn { get; set; }
        public string? DongNghiep { get; set; }
        public string? HocTap { get; set; }
        public string? CamKet { get; set; }
        public int? Idlh { get; set; }

        public virtual Lienhe? IdlhNavigation { get; set; }
        public virtual Khachhang? MaKhNavigation { get; set; }
        public virtual ICollection<Baocao> Baocaos { get; set; }
    }
}
