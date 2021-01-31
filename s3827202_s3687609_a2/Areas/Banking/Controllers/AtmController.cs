using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Areas.Banking.Models;
using s3827202_s3687609_a2.ViewModel;
using s3827202_s3687609_a2.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using s3827202_s3687609_a2.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using s3827202_s3687609_a2.Areas.Identity.Data;

namespace s3827202_s3687609_a2.Controllers
{
    [Area("Banking")]
    [Authorize(Roles = "Customer")]
    public class AtmController : Controller
    {
        private readonly BankDbContext _context;

        private readonly UserManager<BankDbUser> _userManager;


        public AtmController(BankDbContext context, UserManager<BankDbUser> userManager) {
            _context = context;
            _userManager = userManager;
        } 

        public async Task<IActionResult> AtmTransaction()
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            var sourceAccounts = _context.Account.Where(x => x.CustomerID == CustomerID).
                Select(x => new SelectListItem() {
                    Text = x.AccountNumber.ToString(),
                    Value = x.AccountNumber.ToString()
                }).
                ToList();

            var destAccounts = _context.Account.
                Select(x => new SelectListItem()
            {
                Text = x.AccountNumber.ToString(),
                Value = x.AccountNumber.ToString()
            }).ToList();


            var AtmTransModel = new AtmTransactionViewModel()
            {
                SourceAccounts = sourceAccounts,
                DestinationAccounts = destAccounts
            };

            return View(AtmTransModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtmTransaction(decimal amount, TransactionTypeVM transactionTypeVM, int sourceAccount, int? destAccount, string comment)
        {
            var user = await _userManager.GetUserAsync(User);
            var CustomerID = user.CustomerID.Value;

            var account = await _context.Account.FindAsync(sourceAccount);
            var serviceCharge = account.FreeTransaction == 0 ? 0.2m : 0.0m;

            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            if (transactionTypeVM == TransactionTypeVM.Transfer && destAccount == sourceAccount)
                ModelState.AddModelError(nameof(destAccount), "Cannot transfer to the same account");
            if (transactionTypeVM == TransactionTypeVM.Transfer && destAccount == null)
                ModelState.AddModelError(nameof(destAccount), "Please select a destination account");
            if (transactionTypeVM == TransactionTypeVM.Withdrawal && account.Balance - (amount + serviceCharge) < 0.0m)
                ModelState.AddModelError(nameof(amount), "Insufficient balance.");
            if (transactionTypeVM != TransactionTypeVM.Transfer && destAccount != null)
            {
                ModelState.AddModelError(nameof(destAccount), "Deposit/Withdraw doesn't need a destination account.");
            }
            if (!ModelState.IsValid)
            {
                var sourceAccounts = _context.Account.Where(x => x.CustomerID == CustomerID).
                Select(x => new SelectListItem()
                {
                    Text = x.AccountNumber.ToString(),
                    Value = x.AccountNumber.ToString()
                }).
                ToList();


                var destAccounts = _context.Account.
                Select(x => new SelectListItem()
                {
                    Text = x.AccountNumber.ToString(),
                    Value = x.AccountNumber.ToString()
                }).ToList();


                var AtmTransModel = new AtmTransactionViewModel()
                {
                    SourceAccounts = sourceAccounts,
                    DestinationAccounts = destAccounts
                };

                return View(AtmTransModel);
            }

            if (transactionTypeVM == TransactionTypeVM.Deposit)
            {
                account.Balance += amount;
                account.ModifyDate = DateTime.UtcNow;
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Deposit,
                        Amount = amount,
                        Comment = comment,
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Customer");
            }

            else if (transactionTypeVM == TransactionTypeVM.Withdrawal)
            {
                account.Balance -= amount + serviceCharge;
                account.ModifyDate = DateTime.UtcNow;
                account.FreeTransaction = account.FreeTransaction > 0 ? account.FreeTransaction - 1 : account.FreeTransaction;
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Withdrawal,
                        Amount = amount,
                        Comment = comment,
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });
                if (account.FreeTransaction == 0)
                {
                    account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.ServiceCharge,
                        Amount = serviceCharge,
                        Comment = "service Charge",
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Customer");
            }
            else if (transactionTypeVM == TransactionTypeVM.Transfer)
            {
                account.Balance -= amount + serviceCharge;
                account.ModifyDate = DateTime.UtcNow;
                account.FreeTransaction = account.FreeTransaction > 0 ? account.FreeTransaction - 1 : account.FreeTransaction;
                account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Transfer,
                        Amount = amount,
                        Comment = comment,
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });
                if (account.FreeTransaction == 0)
                {
                    account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.ServiceCharge,
                        Amount = serviceCharge,
                        Comment = "service Charge",
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });
                }
                var destinationAccount = await _context.Account.FindAsync(destAccount);

                destinationAccount.Balance += amount;
                destinationAccount.ModifyDate = DateTime.UtcNow;
                destinationAccount.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.Transfer,
                        Amount = amount,
                        Comment = comment,
                        ModifyDate = DateTime.UtcNow,
                        TransactionStatus = TransactionStatus.Idle
                    });

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Customer");
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
