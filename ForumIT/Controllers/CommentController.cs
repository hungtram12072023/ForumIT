using ForumIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ForumIT.Controllers
{
    public class CommentController : Controller
    {
        ForumITContext db = new ForumITContext();
        public IActionResult CreateCommment()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateCommment(TblBinhLuan bl)
        { 
            db.TblBinhLuans.Add(bl);
            db.SaveChanges();
            return RedirectToAction("PostDetail","PostMN", new { idBV = Convert.ToInt32(bl.IdidBaiVietBl)});
        }

        [Authorize]
        public IActionResult DeleteCommment(int mbl,int mbv)
        {
            var bl = db.TblBinhLuans.Find(mbl);
            db.TblBinhLuans.Remove(bl);
            db.SaveChanges();
            return RedirectToAction("PostDetail", "PostMN", new { idBV = mbv });
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateCommment(TblBinhLuan bl)
        {
            
            db.TblBinhLuans.Update(bl);
            db.SaveChanges();
            return RedirectToAction("PostDetail", "PostMN", new { idBV = Convert.ToInt32(bl.IdidBaiVietBl) });
        }


    }
}
