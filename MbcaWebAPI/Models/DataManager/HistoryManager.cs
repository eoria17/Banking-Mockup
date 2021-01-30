using MbcaWebAPI.Models.Repository;
using MbcaWebAPI.Data;
using System.Collections.Generic;
using System.Linq;
using MbcaWebAPI.Models.ViewModels;
using System;

namespace MbcaWebAPI.Models.DataManager
{
    public class HistoryManager : IDataRepository<AccountViewModel, int>
    {
        private readonly MbcaDbContext _context;

        public HistoryManager(MbcaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountViewModel> GetAll()
        {
            var accounts = _context.Account.ToList();

            var viewModel = accounts.Select(x => new AccountViewModel {

                AccountNumber = x.AccountNumber,
                AccountType = x.AccountType,
                CustomerID = x.CustomerID,
                Balance = x.Balance,
                ModifyDate = x.ModifyDate,
                FreeTransaction = x.FreeTransaction,
                Transactions = x.Transactions.Select(y => new TransactionViewModel {
                    TransactionID = y.TransactionID,
                    TransactionType = y.TransactionType,
                    AccountNumber = y.AccountNumber,
                    DestAccount = y.DestAccount,
                    Amount = y.Amount,
                    Comment = y.Comment,
                    ModifyDate = y.ModifyDate,
                    TransactionStatus = y.TransactionStatus
                }).ToList()
            }).ToList();

            return viewModel;
        }

        public AccountViewModel Get(int id)
        {
           
                var x = _context.Account.Where(x => x.CustomerID == id).FirstOrDefault();

            if (x == null)
            {
                return null;
            }

                return new AccountViewModel
                {

                    AccountNumber = x.AccountNumber,
                    AccountType = x.AccountType,
                    CustomerID = x.CustomerID,
                    Balance = x.Balance,
                    ModifyDate = x.ModifyDate,
                    FreeTransaction = x.FreeTransaction,
                    Transactions = x.Transactions.Select(y => new TransactionViewModel
                    {
                        TransactionID = y.TransactionID,
                        TransactionType = y.TransactionType,
                        AccountNumber = y.AccountNumber,
                        DestAccount = y.DestAccount,
                        Amount = y.Amount,
                        Comment = y.Comment,
                        ModifyDate = y.ModifyDate,
                        TransactionStatus = y.TransactionStatus
                    }).ToList()
                };
        }

    }
}
