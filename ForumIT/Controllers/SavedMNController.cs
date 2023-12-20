using ForumIT.Models;
using ForumIT.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ForumIT.Controllers
{
    public class SavedMNController : Controller
    {
        ForumITContext _context =new ForumITContext();
        private readonly UserManager<User> _userManager;

        public SavedMNController( UserManager<User> userManager, ForumITContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> ListSaved()
        {
            var userss = await _userManager.Users.ToListAsync();

            var usersInfoo = new List<User>();


            foreach (var user in userss)
            {
                var userInfoo = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                    // Thêm các thông tin khác tùy ý
                };

                usersInfoo.Add(userInfoo);
            }
            List<User> lstt = usersInfoo.ToList();

            var idd = _userManager.GetUserId(User);

            string iddUS = "";

            foreach (var user in lstt)
            {
                if (user.Id == idd)
                {
                    iddUS = user.Id;
                }
            }

            List<TblDaLuu> dl = _context.TblDaLuus.Where(x=>x.IdUser==iddUS).ToList();
            return View(dl);
        }

        [Authorize(Roles= "user")]
        [HttpPost]
        public IActionResult CreateSaved(TblDaLuu bl)
        {
            _context.TblDaLuus.Add(bl);
            _context.SaveChanges();
            return RedirectToAction("ListSaved");
        }


        [Authorize(Roles = "user")]
        public IActionResult DeleteSaved(int idl, int idbv)
        {        
            var cl = _context.TblDaLuus.Find(idl);
            _context.TblDaLuus.Remove(cl);
            _context.SaveChanges();
            return RedirectToAction("PostDetail","PostMN", new { idBV = idbv });
        }

    }
}
