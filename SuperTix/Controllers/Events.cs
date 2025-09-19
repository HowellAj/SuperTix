using Microsoft.AspNetCore.Mvc;

namespace SuperTix.Controllers
{
    public class Events : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
