using Microsoft.AspNetCore.Mvc;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
