using System;
using System.Collections.Generic;

namespace SignInTPMedia.Models
{
    public partial class Phongban
    {
        public Phongban()
        {
            Nhanviens = new HashSet<Nhanvien>();
        }

        public string MaPhongBan { get; set; } = null!;
        public string? TenPhongBan { get; set; }

        public virtual ICollection<Nhanvien> Nhanviens { get; set; }
    }
}
