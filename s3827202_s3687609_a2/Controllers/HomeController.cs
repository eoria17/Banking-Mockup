using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using s3827202_s3687609_a2.Models;

namespace s3827202_s3687609_a2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        //public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
