using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Banking.Models;

namespace s3827202_s3687609_a2.Areas.Admin.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string? TFN { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostCode { get; set; }

        public string Phone { get; set; }

        public CustomerStatus Status { get; set; }
    }
}
