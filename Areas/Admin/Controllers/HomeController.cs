using Microsoft.AspNetCore.Mvc;

namespace IP_AmazonFreshIndia_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("[area]/[controller]s/{id?}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
