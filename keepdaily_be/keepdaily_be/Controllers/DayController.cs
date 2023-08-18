using Microsoft.AspNetCore.Mvc;

namespace keepdaily_be.Controllers
{
    public class DayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
