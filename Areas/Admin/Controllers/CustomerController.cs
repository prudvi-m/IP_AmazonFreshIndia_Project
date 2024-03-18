using Microsoft.AspNetCore.Mvc;

namespace IP_AmazonFreshIndia_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        [Route("[area]/[controller]s")]
        public IActionResult Index()
        {
            return Content("Area: Admin - Controller : Customer - Action - Index");
        }
    }
}
