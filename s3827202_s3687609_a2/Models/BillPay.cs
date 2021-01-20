using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3827202_s3687609_a2.Models
{
    public class BillPay
    {
        public int BillPayID { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        [ForeignKey("AccountNumber")]
        public Account Account { get; set; }

        [Required]
        public int PayeeID { get; set; }
        public Payee Payee { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required,DataType(DataType.Date)]
        public DateTime ScheduleDate { get; set; }

        [RegularExpression(@"^[MQS]\b",ErrorMessage = "Please input the correct period format(e.g Monthly (M), Quarterly (Q) or Once off (S)")]
        [Required]
        public string Period { get; set; }

        [Required,DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
    }
}
