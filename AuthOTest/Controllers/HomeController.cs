using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthOTest.Controllers
{
    [Authorize] // Require authentication for the entire controller
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
