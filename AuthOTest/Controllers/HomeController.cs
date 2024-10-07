using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace AuthOTest.Controllers
{
    [Authorize] // Require authentication for the entire controller
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubApp()
        {
            string reactAppUrl = "http://localhost:3000"; 
            return Redirect(reactAppUrl);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
