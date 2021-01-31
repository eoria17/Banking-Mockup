using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using McbaAdmin.Models;


namespace McbaAdmin.Controllers
{   
    public class LoginController : Controller
    {
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            Login login = new Login();
            login.LoginID = loginID;
            login.PasswordHash = password;
            if (loginID.ToLower() == "admin" && password.ToLower() == "admin")
            {
                HttpContext.Session.SetString(nameof(login.LoginID), login.LoginID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(login);
            }
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Remove("LoginID");

            return RedirectToAction("Login", "Login");
        }
    }
}
