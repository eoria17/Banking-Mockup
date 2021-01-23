using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3827202_s3687609_a2.Models
{
    public enum TransactionType
    {
        Deposit = 1, //Credit
        Withdrawal = 2, //Debit
        Transfer = 3, //Debit
        ServiceCharge = 4, //Debit
        BillPay = 5 //Debit
    }

    public class Transaction
    {
        public int TransactionID { get; set; }

        [Required,StringLength(1)]
        public string TransactionType { get; set; }

        [Required]
        public int AccountNumber { get; set; }
        [ForeignKey("AccountNumber")]
        public Account SourceAccount { get; set; }

        public int? DestAccount { get; set; }
        [ForeignKey("DestAccount")]
        public Account DestinationAccount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }

    }
}
