using ForumIT.Models;
using ForumIT.Models.Domain;
using ForumIT.Models.Repositories.Implement;
using ForumIT.Models.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using X.PagedList;

namespace ForumIT.Controllers
{
    public class PostMNController : Controller
    {

        ForumITContext db = new ForumITContext();

        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment environment;

        public PostMNController(IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            environment = webHostEnvironment;
            _userManager = userManager;
        }

        [Authorize(Roles="user")]
        public IActionResult CreatePost()
        {
            if (User.IsInRole("user"))
            {
                return View();
            }
            return RedirectToAction("Login", "UserAu");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(TblBaiViet pr, IFormFile file)
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
                if (user.Id == id)
                {
                    pr.IdUser= user.Id;
                }
            }

            if (file != null && file.Length > 0)
            {
                string uploadPath = Path.Combine(environment.WebRootPath, "Uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            

            string userName = User.Identity.Name; 
 
            pr.TrangThai = "Chờ";
            pr.Ngaydang=DateTime.Now;
            pr.Img = file.FileName;


            db.TblBaiViets.Add(pr);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }


        public IActionResult ListPost()
        {
            List<TblBaiViet> tl = db.TblBaiViets.ToList();
            return View(tl);
        }


        public IActionResult PostForId(int id)
        {
            var lstMember = db.TblBaiViets.Where(x => x.IdLdd == id);
            List<TblBaiViet> lst=lstMember.ToList();
            return View(lst);
        }

        public IActionResult PostForIdDuyet(int? page, int id,string filter)
        {
            ForumITContext db = new ForumITContext();
            int pageSize = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (filter != null)
            {
                ViewBag.filter = filter;
                List<TblBaiViet> danhsachKhachHang = db.TblBaiViets.Where(m => m.Title.ToLower().Contains(filter.ToLower()) == true || m.NoiDung.Contains(filter.ToLower()) == true && m.TrangThai == "Duyệt" && m.IdLdd == id).OrderByDescending(x => x.Ngaydang).ToList();
                PagedList<TblBaiViet> lstl = new PagedList<TblBaiViet>(danhsachKhachHang, pageNumber, pageSize);
                return View(lstl);
            }

            //int pageSize = 3;
            //int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstMember = db.TblBaiViets.Where(x => x.TrangThai == "Duyệt" && x.IdLdd == id).OrderByDescending(x => x.Ngaydang).ToList();
            PagedList<TblBaiViet> lstk = new PagedList<TblBaiViet>(lstMember, pageNumber, pageSize);
            return View(lstk);

        }

        public IActionResult GettForIdCho()
        {
            var lstMember = db.TblBaiViets.Where(x => x.TrangThai == "Chờ");
            List<TblBaiViet> lst = lstMember.ToList();
            return View(lst);
        }

        public IActionResult GettForIdDuyet()
        {
            var lstMember = db.TblBaiViets.Where(x => x.TrangThai == "Duyệt");
            List<TblBaiViet> lst = lstMember.ToList();
            return View(lst);
        }

        [Authorize(Roles = "admin")]
        public IActionResult DuyetBai(int id)
        {
            var lstMember = db.TblBaiViets.Find(id);
            lstMember.TrangThai = "Duyệt";
            db.TblBaiViets.Update(lstMember);
            db.SaveChanges();
            return RedirectToAction("GettForIdDuyet");
        }

        [Authorize(Roles = "admin")]
        public IActionResult PostForAdmin()
        {
            var lstMember = db.TblBaiViets.ToList();
            List<TblBaiViet> lst = lstMember.ToList();
            return View(lst);
        }

        public IActionResult PostDetail(int idBV)
        {
            TblBaiViet tb = db.TblBaiViets.Find(idBV);
            return View(tb);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostForUser()
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
            string idd = "";
            foreach (var user in lst)
            {
                if (user.Id == id)
                {
                    idd = user.Id;
                }
            }

            var lstMember = db.TblBaiViets.Where(x => x.IdUser == idd);
            List<TblBaiViet> lsts = lstMember.ToList();
            return View(lsts);
        }


        public IActionResult DeletePost(int id)
        {

            var idd = new SqlParameter("@idbv", id);

            db.Database.ExecuteSqlRaw("exec DelBVietBLuan @idbv", idd);

            if (User.IsInRole("user")) {
                return RedirectToAction("PostForUser");
            }
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("PostForAdmin");
            }
            return RedirectToAction("Index","Home");

        }

        [HttpGet]
        public IActionResult UpdatePost(int idbv)
        {
 
            TblBaiViet bv=db.TblBaiViets.Find(idbv);
            return View(bv);
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePost(TblBaiViet pr, IFormFile file)
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
                if (user.Id == id)
                {
                    pr.IdUser = user.Id;
                }
            }

            if (file != null && file.Length > 0)
            {
                string uploadPath = Path.Combine(environment.WebRootPath, "Uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }


            string userName = User.Identity.Name;

            pr.TrangThai = "Chờ";
            pr.Ngaydang = DateTime.Now;
            pr.Img = file.FileName;


            db.TblBaiViets.Update(pr);
            db.SaveChanges();
            return RedirectToAction("PostForUser");
        }


    }
}
