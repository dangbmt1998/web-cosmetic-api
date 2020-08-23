using System;
using System.Collections.Generic;

namespace Web_Cosmetic_Api.Models
{
    public partial class TaiKhoan
    {
        public int Id { get; set; }
        public string TenTk { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}
