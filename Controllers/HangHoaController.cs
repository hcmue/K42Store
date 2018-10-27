using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K42Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace K42Store.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext db;
        public HangHoaController(MyeStoreContext context)
        {
            db = context;
        }
        public IActionResult Index(int? id)
        {
            List<HangHoa> dsHangHoas = new List<HangHoa>();
            if (id.HasValue)
            {
                dsHangHoas = db.HangHoa.Where(p => p.MaLoai == id)
                    .ToList();
            }
            else
            {
                dsHangHoas = db.HangHoa.ToList();
            }
            return View(dsHangHoas.Select(p=> new HangHoaViewModel
            {
                MaHh = p.MaHh, TenHh = p.TenHh, Hinh = p.Hinh,
                DonGia = p.DonGia.Value, GiamGia = p.GiamGia
            }));
        }
    }
}