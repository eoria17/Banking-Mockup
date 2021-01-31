using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Areas.Banking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using s3827202_s3687609_a2.Areas.Identity.Data;
using System.Security.Claims;
using s3827202_s3687609_a2.Utilities;

namespace s3827202_s3687609_a2.Areas.Banking.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly BankDbContext _context;
        private readonly UserManager<BankDbUser> _userManager;

        public CustomerController(BankDbContext context, UserManager<BankDbUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customer.FindAsync(CustomerID);

            // OR
            // Eager loading.
            //var customer = await _context.Customers.Include(x => x.Accounts).
            //    FirstOrDefaultAsync(x => x.CustomerID == _customerID);

            return View(customer);
        }
    }
}
