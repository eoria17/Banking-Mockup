﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MbcaWebAPI.Models
{
    public enum AccountType
    {
        Checking = 1,
        Saving = 2
    }

    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        [Required]
        [Display(Name = "Free Transaction")]
        public int FreeTransaction { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<BillPay> BillPays { get; set; }
    }
}
