using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K42Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace K42Store.Controllers
{
    public class CartController : Controller
    {
        private readonly MyeStoreContext db;
        public CartController(MyeStoreContext context)
        {
            db = context;
        }

        public List<CartItem> Carts
        {
            get
            {
                List<CartItem> myCart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (myCart == default(List<CartItem>))
                {
                    myCart = new List<CartItem>();
                }

                return myCart;
            }
        }

        public IActionResult AddToCart(int mahh)
        {
            //lấy giỏ hàng đang có
            List<CartItem> gioHang = Carts;
            //kiểm tra xem hàng đã có trong giỏ chưa
            CartItem item = gioHang.SingleOrDefault(p => p.MaHh == mahh);
            //nếu có
            if (item != null)
            {
                item.SoLuong++;//tăng số lượng
            }
            else
            {
                HangHoa hh = db.HangHoa.SingleOrDefault(p => p.MaHh == mahh);
                item = new CartItem
                {
                    MaHh = mahh, SoLuong = 1,
                    TenHh = hh.TenHh, Hinh = hh.Hinh,
                    GiaBan = hh.DonGia.Value * (1 - hh.GiamGia)
                };
                gioHang.Add(item);
            }
            //lưu session
            HttpContext.Session.Set("GioHang", gioHang);
            //chuyển tới trang giỏ hàng để xem
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View(Carts);
        }
    }
}