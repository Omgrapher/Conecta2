using Microsoft.AspNetCore.Mvc;

namespace conecta2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult homePage()
        {
            return View();
        }
    }
}
