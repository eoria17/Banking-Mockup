using System;
using System.ComponentModel.DataAnnotations;

namespace s3827202_s3687609_a2.Areas.Banking.Models
{

    public record Payee
    {
        public int PayeeID { get; init; }

        [Required, StringLength(50)]
        public string PayeeName { get; init; }

        [StringLength(50)]
        public string? Address { get; init; }

        [StringLength(40)]
        public string? City { get; init; }

        [StringLength(20)]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Please input the correct state format (e.g VIC QLD)")]
        public string? State { get; init; }

        [StringLength(10)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Please input the correct post code format (4 digit post code number)")]
        public int? PostCode { get; init; }

        [Required, StringLength(15)]
        [RegularExpression(@"^(\+61\d{8})$", ErrorMessage = "Please input the correct phone number format (+61xxxxxxxx)")]
        public string Phone { get; init; }
    }
}
