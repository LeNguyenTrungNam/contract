using System;
using System.Collections.Generic;

namespace SignInTPMedia.Models
{
    public partial class Nguoidung
    {
        public int Id { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public int? MaNv { get; set; }
        public bool? Status { get; set; }

        public virtual Nhanvien? MaNvNavigation { get; set; }
    }
}
