using Microsoft.AspNetCore.Mvc;

namespace RazorPagesProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
