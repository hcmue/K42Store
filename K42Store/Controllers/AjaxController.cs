using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K42Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace K42Store.Controllers
{
    public class AjaxController : Controller
    {
        private MyeStoreContext db;
        public AjaxController(MyeStoreContext ctx)
        {
            db = ctx;
        }
        public IActionResult ServerTime()
        {
            var ngaygio = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            return Content(ngaygio);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string keyword)
        {
            keyword = keyword.ToLower();
            var data = db.HangHoa.Where(p => p.TenHh.ToLower().Contains(keyword))
                .Select(p => new HangHoaViewModel
                {
                    MaHh = p.MaHh, TenHh = p.TenHh,
                    Hinh = p.Hinh, DonGia = p.DonGia.Value,
                    GiamGia = p.GiamGia
                }).ToList();
            return PartialView(data);
        }

        public IActionResult Json()
        {
            return View();
        }

        public IActionResult JsonSearch(string keyword)
        {
            keyword = keyword.ToLower();
            var data = db.HangHoa.Where(p => p.TenHh.ToLower().Contains(keyword)).Select(p => new
                {
                    TenHH = p.TenHh, GiaBan = p.DonGia.Value * (1- p.GiamGia)
                });
            return Json(data);
        }
    }
}