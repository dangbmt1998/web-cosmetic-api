using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int Id { get; set; }
        public string TenLoaiSp { get; set; }
        public int? IdThuongHieu { get; set; }
        public string MoTa { get; set; }

        public virtual ThuongHieu IdThuongHieuNavigation { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
