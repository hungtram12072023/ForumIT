using ForumIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ForumIT.Controllers
{
    [Authorize(Roles = "admin")]
    public class TypeMNController : Controller
    {
        ForumITContext db = new ForumITContext();
        public IActionResult ListLoaiDD()
        {
            List<TblLoaiDd> lst = db.TblLoaiDds.ToList();
            ViewBag.option = lst;
            return View(lst);
        }

        public IActionResult CreateLoaiDD()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateLoaiDD(TblLoaiDd loaiDd)
        {
            if (loaiDd.TenLoaiDd.ToString().Trim() == null)
            {
                ModelState.AddModelError("TenLoaiDd", "Đang để trống trường loại diễn đàn");
                return View();
            }
            else
            {
                db.TblLoaiDds.Add(loaiDd);
                db.SaveChanges();
                return RedirectToAction("ListLoaiDD");
            }
        }
        public IActionResult UpdateLoaiDD(int id)
        {

            TblLoaiDd model = db.TblLoaiDds.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateLoaiDD(TblLoaiDd model, int id)
        {
            TblLoaiDd ml = db.TblLoaiDds.Find(id);
            ml.TenLoaiDd = model.TenLoaiDd;
            db.SaveChanges();
            return RedirectToAction("ListLoaiDD");
        }
        public IActionResult DeleteLoaiDD(int id)
        {
            var idk = new SqlParameter("@idtype", id);

            db.Database.ExecuteSqlRaw("exec DelType @idtype", idk);

            return RedirectToAction("ListLoaiDD");
        }
        public ActionResult SearchLoaiDD(string search)
        {
            List<TblLoaiDd> lst = db.TblLoaiDds.Where(m => m.TenLoaiDd.ToLower().Contains(search.ToLower()) == true).ToList();
            ViewBag.search = search;
            return View(lst);
        }

        public IActionResult DetailLoaiDD(int id)
        {
            TblLoaiDd model = db.TblLoaiDds.Find(id);
            return View(model);
        }
    }
}
