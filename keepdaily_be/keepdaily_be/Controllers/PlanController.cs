using Microsoft.AspNetCore.Mvc;

namespace keepdaily_be.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
