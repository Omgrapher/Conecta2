using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace conecta2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult homePage()
        {
            return View();
        }
    }
}
