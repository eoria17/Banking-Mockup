using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using s3827202_s3687609_a2.Common;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Models;


namespace s3827202_s3687609_a2.Controllers
{
    public class StatementController : Controller
    {
        private readonly BankDBContext _context;
      

        public StatementController(BankDBContext context)
        {
            _context = context;
        }
        public class AccountLabel {
            public int AccountNumber { get; set; }
            public decimal Balance { get; set; }
        }
        [HttpGet]
        public string GetAccounts(int AccountType)
        {
            List<Account> list = new List<Account>();
            if (AccountType == 1)
            {
                list = _context.Account.Where(x => x.AccountType ==Models.AccountType.Checking).ToList();
            }
            else
            {
                list = _context.Account.Where(x => x.AccountType == Models.AccountType.Saving).ToList();
            }
            
            return JsonConvert.SerializeObject(list.Select(x=>new AccountLabel() { AccountNumber=x.AccountNumber,Balance=x.Balance}));
        }
        // GET: StatementController
        public async Task<IActionResult> Index(string searchString, string AccountNumber, int? pageNumber)   
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
            return View(await PaginatedList<Transaction>.CreateAsync(transactions.AsNoTracking(), pageNumber ?? 1, pageSize));

        }
       

    }
}
