using ForumIT.Models;
using ForumIT.Models.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using X.PagedList;

namespace ForumIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page,string filter)
        {
            ForumITContext db = new ForumITContext();
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (filter != null)
            {
                ViewBag.filter = filter;
                List<TblBaiViet> danhsachKhachHang = db.TblBaiViets.Where(m => m.Title.ToLower().Contains(filter.ToLower()) == true || m.NoiDung.Contains(filter.ToLower()) == true).OrderByDescending(x => x.Ngaydang).ToList();
                PagedList<TblBaiViet> lst = new PagedList<TblBaiViet>(danhsachKhachHang, pageNumber, pageSize);
                return View(danhsachKhachHang);
            }

            //int pageSize = 3;
            //int pageNumber = page == null || page < 0 ? 1 : page.Value;
            
            var lstMember = db.TblBaiViets.Where(x => x.TrangThai == "Duyệt").OrderByDescending(x=>x.Ngaydang).ToList();
            PagedList<TblBaiViet> lstk = new PagedList<TblBaiViet>(lstMember, pageNumber, pageSize);
            return View(lstMember);
        }

        public IActionResult Error()
        {
            return View();
        }

        

    }
}