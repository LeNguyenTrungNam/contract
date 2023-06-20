using System;
using System.Collections.Generic;

namespace SignInTPMedia.Models
{
    public partial class Chucvu
    {
        public Chucvu()
        {
            Nhanviens = new HashSet<Nhanvien>();
        }

        public string MaChucVu { get; set; } = null!;
        public string? TenChucVu { get; set; }

        public virtual ICollection<Nhanvien> Nhanviens { get; set; }
    }
}
