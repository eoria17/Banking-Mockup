using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using s3827202_s3687609_a2.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace s3827202_s3687609_a2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "Customer", new { area = "Banking"});
            }
            else if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return View();
            }
            

        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
