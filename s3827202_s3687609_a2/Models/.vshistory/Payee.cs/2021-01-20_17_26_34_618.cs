using System;
using System.ComponentModel.DataAnnotations;

namespace s3827202_s3687609_a2.Models
{
    public class Payee
    {
        [Required,Key]
        public int PayeeID { get; set; }

        [Required,StringLength(50)]
        public string PayeeName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [RegularExpression(@"^[A-Z]{3}\b", ErrorMessage = "Please input the correct state format (e.g VIC QLD)")]
        public string State { get; set; }

        [StringLength(10)]
        [RegularExpression(@"\b\d{4}\b", ErrorMessage = "Please input the correct post code format (4 digit post code number)")]
        public int PostCode{ get; set;}
        [Required,StringLength(15)]
        [RegularExpression(@"\+61\d{8}", ErrorMessage = "Please input the correct phone number format (+61xxxxxxxx)")]
            public int Phone { get; set; }
    }
}
