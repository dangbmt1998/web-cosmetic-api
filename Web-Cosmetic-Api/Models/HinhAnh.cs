using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Models
{
    public partial class HinhAnh
    {
        public int Id { get; set; }
        public int? IdSanPham { get; set; }
        public string LinkHinhAnh { get; set; }
        public string TenHinhAnh { get; set; }
        public int? IdThuongHieu { get; set; }

        public virtual SanPham IdSanPhamNavigation { get; set; }
        public virtual ThuongHieu IdThuongHieuNavigation { get; set; }
    }
}
