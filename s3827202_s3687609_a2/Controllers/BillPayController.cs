using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Filters;
using s3827202_s3687609_a2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s3827202_s3687609_a2.Controllers
{
    [AuthorizeCustomer]
    public class BillPayController : Controller
    {
        private readonly BankDBContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public BillPayController(BankDBContext context)
        {
            _context = context;
        } 
        public IActionResult Index()
        {
            return View();
        }
        // GET: BillPay/Create
        public IActionResult Create()
        {           
            ViewData["AccountNumber"] = new SelectList(_context.Account.Where(a => a.CustomerID == CustomerID), "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payee.Where(x=>x.PayeeID>0), "PayeeID", "PayeeID");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillPay billPay)
        {           
            if (ModelState.IsValid)
            {
                billPay.ModifyDate = DateTime.UtcNow;
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNumber"] = new SelectList(_context.Account.Where(a => a.CustomerID == CustomerID), "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payee.Where(x => x.PayeeID > 0), "PayeeID", "PayeeID");
            return View(billPay);
        }
    }
}
