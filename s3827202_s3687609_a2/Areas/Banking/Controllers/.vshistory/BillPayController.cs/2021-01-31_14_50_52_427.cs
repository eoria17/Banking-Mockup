using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Filters;
using s3827202_s3687609_a2.Areas.Banking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using s3827202_s3687609_a2.Areas.Identity.Data;

namespace s3827202_s3687609_a2.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Customer")]
    public class BillPayController : Controller
    {
        private readonly BankDbContext _context;
        private readonly UserManager<BankDbUser> _userManager;

        public BillPayController(BankDbContext context, UserManager<BankDbUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        } 
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            var billPays = _context.Account.
                Where(x => x.CustomerID == CustomerID).
                Select(x => x.BillPays).SelectMany(x => x).ToList();

            return View(billPays);
        }
        // GET: BillPay/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            ViewData["AccountNumber"] = new SelectList(_context.Account.Where(a => a.CustomerID == CustomerID), "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payee.Where(x=>x.PayeeID>0), "PayeeID", "PayeeName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillPay billPay)
        {           
            if (ModelState.IsValid)
            {
                if (_context.Account.Where(x=>x.AccountNumber==billPay.AccountNumber).FirstOrDefault().Balance > billPay.Amount&&billPay.Amount>0)
                {
                    if (billPay.ScheduleDate < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        ModelState.AddModelError(nameof(billPay.ScheduleDate), "Invaild data time.");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "";
                        billPay.ModifyDate = DateTime.UtcNow;
                        billPay.Status = BillPayStatus.Available;
                        _context.Add(billPay);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }                   
                }
                else
                {
                    ModelState.AddModelError(nameof(billPay.Amount), "Not enough money or invaild amount.");                   
                }
            }
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            ViewData["AccountNumber"] = new SelectList(_context.Account.Where(a => a.CustomerID == CustomerID), "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payee.Where(x => x.PayeeID > 0), "PayeeID", "PayeeName");
            return View(billPay);
        }

        public async Task<IActionResult> Modify(int id)
        {
            var billPay = await _context.BillPay.FindAsync(id);

            return View(billPay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(int id, decimal amount, DateTime scheduleDate, Period period)
        {
            var billPay = _context.BillPay.Find(id);

            if (id != billPay.BillPayID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    billPay.Amount = amount;
                    billPay.ScheduleDate = scheduleDate;
                    billPay.Period = period;

                    _context.Update(billPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _context.BillPay.AnyAsync(x => x.BillPayID == billPay.BillPayID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(billPay);
        }
    }
}
