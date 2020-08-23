using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Model
{
    public partial class KichCoSp
    {
        public int Id { get; set; }
        public int? IdSanPham { get; set; }
        public int? GiaSp { get; set; }
        public string TenKichCo { get; set; }

        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
