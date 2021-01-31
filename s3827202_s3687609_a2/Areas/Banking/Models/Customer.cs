using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace s3827202_s3687609_a2.Areas.Banking.Models
{

    public enum CustomerStatus
    {
        Locked = 1,
        Unlocked = 2
    }

    public record Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; init; }

        [Required, StringLength(50)]
        public string CustomerName { get; init; }

        [StringLength(11)]
        public string? TFN { get; init; }

        [StringLength(50)]
        public string? Address { get; init; }

        [StringLength(40)]
        public string? City { get; init; }

        [StringLength(20)]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Please input the correct state format (e.g VIC QLD)")]
        public string? State { get; init; }

        [StringLength(10)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please input the correct post code format (4 digit post code number)")]
        public string? PostCode { get; init; }

        [Required, StringLength(15)]
        [RegularExpression(@"^(\+61\d{8})$", ErrorMessage = "Please input the correct phone number format (+61xxxxxxxx)")]
        public string Phone { get; init; }

        [Required]
        public CustomerStatus Status { get; init; } //locked or unlocked

        public virtual List<Account> Accounts { get; init; }
    }
    
}
