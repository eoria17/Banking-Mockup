using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using s3827202_s3687609_a2.Common;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Filters;
using s3827202_s3687609_a2.Areas.Banking.Models;
using Microsoft.AspNetCore.Authorization;
using s3827202_s3687609_a2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace s3827202_s3687609_a2.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Customer")]
    public class StatementController : Controller
    {
        private readonly BankDbContext _context;
        private readonly UserManager<BankDbUser> _userManager;
        public StatementController(BankDbContext context, UserManager<BankDbUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public class AccountLabel {
            public int AccountNumber { get; set; }
            public decimal Balance { get; set; }          
        }
        [HttpGet]
        [Route("/Statement/GetAccounts")]
        public async Task<string> GetAccounts(int AccountType)
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            List<Account> list = new List<Account>();
            if (AccountType == 1)
            {
                list = _context.Account.Where(x => x.AccountType == s3827202_s3687609_a2.Areas.Banking.Models.AccountType.Checking &&x.CustomerID== CustomerID).ToList();
            }
            else
            {
                list = _context.Account.Where(x => x.AccountType == s3827202_s3687609_a2.Areas.Banking.Models.AccountType.Saving && x.CustomerID == CustomerID).ToList();
            }
            
            return JsonConvert.SerializeObject(list.Select(x=>new AccountLabel() { AccountNumber=x.AccountNumber,Balance=x.Balance}));
        }
        // GET: StatementController
        public async Task<IActionResult> Index(string searchString, string AccountNumber,string AccountType,int? pageNumber)   
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = AccountNumber;
            }
            ViewData["AccountNumber"] = searchString;
            ViewData["AccountType"] = AccountType;
           
            var transactions = from s in _context.Transaction
                           select s;
            if (!String.IsNullOrEmpty(AccountNumber))
            {
                transactions = transactions.Where(s => s.AccountNumber == Convert.ToInt32(AccountNumber));
            }
            else
            {
                transactions = transactions.Where(s => s.AccountNumber == -1);
            }
            int pageSize = 4;           
            return View(await PaginatedList<Transaction>.CreateAsync(transactions.OrderByDescending(x=>x.ModifyDate).AsNoTracking(), pageNumber ?? 1, pageSize));

        }
       

    }
}
