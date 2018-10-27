using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K42Store.Models
{
    public class HangHoaViewModel
    {
        [Key]
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public double GiamGia { get; set; }
        public double GiaBan => DonGia * (1 - GiamGia);
        public bool DangKhuyenMai => GiamGia > 0;
    }
}
