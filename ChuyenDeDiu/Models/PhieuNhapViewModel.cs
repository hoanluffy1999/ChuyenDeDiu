using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuyenDeDiu.Models
{
    public class PhieuNhapViewModel
    {
        public int Ma { get; set; }
        public Nullable<int> MaNV { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<int> MaNCC { get; set; }
    }
}