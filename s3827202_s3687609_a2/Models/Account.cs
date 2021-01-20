using System;
using System.ComponentModel.DataAnnotations;


namespace s3827202_s3687609_a2.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"^[CS]\b", ErrorMessage = "Please input the correct account type format(e.g C or S)")]
        public string AccountType { get; set; }

        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required,DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
    }
}
