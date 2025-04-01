using Microsoft.AspNetCore.Mvc;

namespace MeetingPoint_AG_Service.Controllers
{
    public class MeetingRoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
