using ForumIT.Models;
using ForumIT.Models.Domain;
using ForumIT.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForumIT.Controllers
{

    public class UserMNController : Controller
    {
        ForumITContext db=new ForumITContext();
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _siginManager;

        public UserMNController(UserManager<User> userManager, SignInManager<User> siginManager)
        {
            _userManager = userManager;
            _siginManager = siginManager;
        }
        public async Task<IActionResult> ListUserAsync()
        {
            List<AspNetUser> users =db.AspNetUsers.ToList();

            //var user = await _userManager.FindByNameAsync("Admin");
            //var userId = await _userManager.GetUserIdAsync(user);

            return View(users);
        }

        public async Task<List<User>> GetRegisteredUsersInfo()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersInfo = new List<User>();

            foreach (var user in users)
            {
                var userInfo = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                    // Thêm các thông tin khác tùy ý
                };

                usersInfo.Add(userInfo);
            }
            List<User> lst=usersInfo.ToList();
            return lst;
        }

        public async Task<string> getUser()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersInfo = new List<User>();

            foreach (var user in users)
            {
                var userInfo = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                    // Thêm các thông tin khác tùy ý
                };

                usersInfo.Add(userInfo);
            }
            List<User> lst = usersInfo.ToList();

            var id = _userManager.GetUserId(User);
            
            foreach (var user in lst)
            {
                if(user.Id == id)
                {
                    return user.Id;
                }
            }


            return "";
        
        }

        public async Task<IActionResult> getProfile()
        {
            var users = await _userManager.Users.ToListAsync();
            //var id = _userManager.GetUserId(User);

            var usersInfo = new List<AspNetUser>();

            foreach (var user in users)
            {
                var userInfo = new AspNetUser
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName=user.FirstName, 
                    LastName=user.LastName,
                    // Thêm các thông tin khác tùy ý
                };

                usersInfo.Add(userInfo);
            }
            List<AspNetUser> lst = usersInfo.ToList();
            var id = _userManager.GetUserId(User);

            foreach (var user in lst)
            {
                if (user.Id == id)
                {
                    return View(user);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getProfile(AspNetUser us)
        {
            var id = _userManager.GetUserId(User);
            AspNetUser usa = new AspNetUser();
            usa.Id = id;
            usa.UserName = us.UserName;
            usa.Email = us.Email;
            usa.FirstName = us.FirstName;
            usa.LastName = us.LastName;
            db.AspNetUsers.Update(usa);
            db.SaveChanges();
            
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> changePass()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> changePass(ChangePass ps)
        {
            var us = await _userManager.GetUserAsync(User);


            if (us != null)
            {
                var passwordValid = await _userManager.CheckPasswordAsync(us, ps.PasswordOld);
                if (!passwordValid)
                {
                    ModelState.AddModelError("PasswordOld", "No ....");
                    // Current password is invalid
                    return View();
                }

                else if (ps.Password != ps.ConfirmPassword)
                {
                    
                    return View();

                }
                else {
                    var result = await _userManager.ChangePasswordAsync(us, ps.PasswordOld, ps.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            
            return RedirectToAction("Login", "UserAu");

        }

        public IActionResult DeleteUsser(string idd)
        {
            var id = new SqlParameter("@idus", idd);

            db.Database.ExecuteSqlRaw("exec DelUser @idus", id);

            return RedirectToAction("ListUser","UserMN");
        }
    }
}
