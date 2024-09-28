using Microsoft.AspNetCore.Mvc;

namespace PracticeSite.Controllers
{
    public class ApplicationFormsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
