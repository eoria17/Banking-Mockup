using System.Collections.Generic;
using s3827202_s3687609_a2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace s3827202_s3687609_a2.ViewModel
{
    public enum TransactionTypeVM
    {
        Deposit = 1, //Credit
        Withdrawal = 2, //Debit
        Transfer = 3, //Debit
        
    }

    public class AtmTransactionViewModel
    {
        [Display(Name = "Source Accounts")]
        public List<SelectListItem> SourceAccounts { get; set; }

        [Display(Name = "Destination Accounts")]
        public List<SelectListItem> DestinationAccounts { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionTypeVM TransactionTypeVM { get; set; }

        [Display(Name = "Source Account")]
        public Account SourceAccount { get; set; }

        [Display(Name = "To Account")]
        public Account DestAccount { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }

    }
}
