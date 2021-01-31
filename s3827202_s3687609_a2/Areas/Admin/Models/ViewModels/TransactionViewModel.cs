using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Areas.Admin.Models.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }
        public int AccountNumber { get; set; }
        public int? DestAccount { get; set; }
        public decimal? Amount { get; set; }
        public string? Comment { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
