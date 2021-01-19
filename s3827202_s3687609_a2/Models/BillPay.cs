using System;
using System.ComponentModel.DataAnnotations;

namespace s3827202_s3687609_a2.Models
{
    public class BillPay
    {
        [Required,Key]
        public int BillPayID { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        [Required]
        public int PayeeID { get; set; }
        public Payee Payee { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required,DataType(DataType.Date)]
        public DateTime ScheduleDate { get; set; }

        [RegularExpression(@"^[MQS]\b",ErrorMessage = "Please input the correct perios format(e.g Monthly (M), Quarterly (Q) or Once off (S)")]
        [Required]
        public string Period { get; set; }

        [Required,DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
    }
}
