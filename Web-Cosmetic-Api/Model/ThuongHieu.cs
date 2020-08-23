using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Model
{
    public partial class ThuongHieu
    {
        public ThuongHieu()
        {
            HinhAnhs = new HashSet<HinhAnh>();
            LoaiSps = new HashSet<LoaiSp>();
        }

        public int Id { get; set; }
        public string TenThuongHieu { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
        public virtual ICollection<LoaiSp> LoaiSps { get; set; }
    }
}
