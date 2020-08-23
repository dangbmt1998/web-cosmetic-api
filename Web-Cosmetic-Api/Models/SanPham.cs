using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            HinhAnhs = new HashSet<HinhAnh>();
            KichCoSps = new HashSet<KichCoSp>();
        }

        public int Id { get; set; }
        public int? IdLoaiSp { get; set; }
        public string TenSp { get; set; }
        public string MoTa { get; set; }
        public string LoiIch { get; set; }

        public virtual LoaiSp IdLoaiSpNavigation { get; set; }
        
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
        public virtual ICollection<KichCoSp> KichCoSps { get; set; }
    }
}
