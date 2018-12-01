using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using K42Store.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K42Store.Controllers
{
    [Authorize]
    public class KhachHangController : Controller
    {
        private readonly MyeStoreContext db;
        public KhachHangController(MyeStoreContext ctx)
        {
            db = ctx;
        }
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            KhachHang kh = db.KhachHang.SingleOrDefault(p => p.MaKh == model.MaKh && p.MatKhau == model.MatKhau);
            if (kh == null)
            {
                ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                return View();
            }
            else
            {
                //Ghi session

                //Ghi nhận Authenticate
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, kh.HoTen));
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }

                return RedirectToAction("Profile");
            }
        }

        public IActionResult Profile()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}