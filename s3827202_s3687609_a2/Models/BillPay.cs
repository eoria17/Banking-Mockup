using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3827202_s3687609_a2.Models
{
    public enum Period
    {
        Monhly = 1,
        Quarterly = 2,
        OnceOff = 3
    }

    public class BillPay
    {
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        public int PayeeID { get; set; }
        public Payee Payee { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }
    }
}
