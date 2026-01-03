using Microsoft.AspNetCore.Mvc;

namespace Middleware_Auth.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
