using ForumIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumIT.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        //Qly Loai Dien Dan

        public IActionResult HelloAd()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            return RedirectToAction("Error","Home");
        }


    }
}

