using ForumIT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumIT.Controllers
{
    public class ReplyController : Controller
    {
        ForumITContext context = new ForumITContext();
        public IActionResult ListReply()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReply(TblTraLoiBl tl)
        {
            TblBinhLuan bl = context.TblBinhLuans.Find(tl.IdBluanTloi);
            context.TblTraLoiBls.Add(tl);
            context.SaveChanges();
            return RedirectToAction("PostDetail", "PostMN", new { idBV = Convert.ToInt32(bl.IdidBaiVietBl) });
        }

    }
}
