using Microsoft.AspNetCore.Mvc;

namespace IP_AmazonFreshIndia_Project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return Content("Controller : Product - Action - Index");
        }
    }
}
