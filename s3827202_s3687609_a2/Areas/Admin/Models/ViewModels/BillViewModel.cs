using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Areas.Admin.Models.ViewModels
{
    public class BillViewModel
    {
        public int BillPayID { get; set; }

        public int AccountNumber { get; set; }
       
        public int PayeeID { get; set; }

        public decimal Amount { get; set; }

        public DateTime ScheduleDate { get; set; }

        public Period Period { get; set; }

        public DateTime ModifyDate { get; set; }

        public BillPayStatus Status { get; set; }
    }
}
