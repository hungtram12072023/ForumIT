using ForumIT.Models;
using ForumIT.Models.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ForumIT.ViewComponents
{
    public class DienDanTheoLoaiViewComponent : ViewComponent
    {
        private readonly IType _type;
        
        public DienDanTheoLoaiViewComponent(IType type)
        {
            _type = type;
        }

        public IViewComponentResult Invoke()
        {
            var type = _type.GetAll();

            return View(type);
        }
    }
}
