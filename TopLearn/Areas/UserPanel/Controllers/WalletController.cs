using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Areas.UserPanel.Controllers
{
    public class WalletController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
