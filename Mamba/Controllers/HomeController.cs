using Microsoft.AspNetCore.Mvc;

namespace MambaProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
