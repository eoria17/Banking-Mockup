using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Areas.Admin.Models.ViewModels
{
    public class AccountViewModel
    {
        public int AccountNumber { get; set; }

        public AccountType AccountType { get; set; }

        public int CustomerID { get; set; }

        public decimal Balance { get; set; }

        public DateTime ModifyDate { get; set; }

        public int FreeTransaction { get; set; }

        public virtual List<TransactionViewModel> Transactions { get; set; }
    }
}
