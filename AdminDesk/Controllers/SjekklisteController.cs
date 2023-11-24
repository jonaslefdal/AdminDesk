using Microsoft.AspNetCore.Mvc;

namespace AdminDesk.Controllers
{
    public class SjekklisteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
