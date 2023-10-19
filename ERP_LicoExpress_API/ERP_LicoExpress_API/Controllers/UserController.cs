using Microsoft.AspNetCore.Mvc;

namespace ERP_LicoExpress_API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
