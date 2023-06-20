using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignInTPMedia.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Nguoidungs = new HashSet<Nguoidung>();
        }

        public int MaNv { get; set; }
        public string TenNv { get; set; } = null!;
        public byte GioiTinh { get; set; }
        public byte GiaDinh { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        public string? NoiSinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Cccd { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayCap { get; set; }
        public string? NoiCap { get; set; }
        public string? HinhAnh { get; set; }
        public byte TinhTrang { get; set; }
        public string? MaChucVu { get; set; }
        public string? MaPhongBan { get; set; }
        [NotMapped]
        public IFormFile HinhAnhFile { get; set; }
        public byte[]? HinhAnhBytes { get; set; }


        public virtual Chucvu? MaChucVuNavigation { get; set; }
        public virtual Phongban? MaPhongBanNavigation { get; set; }
        public virtual ICollection<Nguoidung> Nguoidungs { get; set; }
    }
}
