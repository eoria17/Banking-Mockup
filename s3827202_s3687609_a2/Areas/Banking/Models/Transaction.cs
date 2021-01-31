using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3827202_s3687609_a2.Areas.Banking.Models
{
    public enum TransactionType
    {
        Deposit = 1, //Credit
        Withdrawal = 2, //Debit
        Transfer = 3, //Debit
        ServiceCharge = 4, //Debit
        BillPay = 5 //Debit
    }

    public enum TransactionStatus
    {
        Idle = 1,
        Reported = 2
    }

    public record Transaction
    {
        public int TransactionID { get; init; }

        [Required,StringLength(1)]
        public TransactionType TransactionType { get; init; }

        [Required]
        public int AccountNumber { get; init; }
        [ForeignKey("AccountNumber")]
        public virtual Account SourceAccount { get; init; }

        public int? DestAccount { get; init; }
        [ForeignKey("DestAccount")]
        public virtual Account DestinationAccount { get; init; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal? Amount { get; init; }

        [StringLength(255)]
        public string? Comment { get; init; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; init; }

        public TransactionStatus TransactionStatus { get; init; }

    }
}
