using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
