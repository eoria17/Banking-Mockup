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

    public enum BillPayStatus
    {
        Available = 1,
        Blocked = 2,
        Done = 3
    }

    public class BillPay
    {
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [Display(Name = "ID")]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }

        [Display(Name = "Scheduled Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        [Required]
        public BillPayStatus Status { get; set; } //locked or unlocked
    }
}
