using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McbaAdmin.Models.ViewModels
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
