using Microsoft.AspNetCore.Mvc;

namespace IP_AmazonFreshIndia_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        [Route("[area]/[controller]s/{id?}")]
        public IActionResult Index()
        {
            return Content("Area: Admin - Controller : Product - Action - Index");
        }
    }
}
