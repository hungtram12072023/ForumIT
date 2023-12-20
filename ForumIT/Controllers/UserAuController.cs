using ForumIT.Models.DTO;
using ForumIT.Models.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumIT.Controllers
{
    public class UserAuController : Controller
    {
        private readonly IAuRepository authRepository;

        public UserAuController(IAuRepository authRepository)
        {
            this.authRepository = authRepository;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginDTO loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequestDto);
            }
            var result = await authRepository.LoginAsync(loginRequestDto);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                
                //TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }

        }

        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Register
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerRequestDto)
        {
            if (!ModelState.IsValid) { return View(registerRequestDto); }
            registerRequestDto.Role = "user";
            var result = await authRepository.RegisterAsync(registerRequestDto);
            //TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Login));
        }


        /// <summary>
        /// Register Admin
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> RegisterAdmin()
        {
            if (User.IsInRole("admin"))
            {
                RegisterDTO registerRequestDto = new RegisterDTO()
                {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Password = "Admin@123456"
                };
                registerRequestDto.Role = "admin";
                var result = await authRepository.RegisterAsync(registerRequestDto);

                return Ok(result);
            }
            return RedirectToAction("Error", "Home");
            
        }


        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authRepository.LogOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
