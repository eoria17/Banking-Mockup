using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using s3827202_s3687609_a2.Areas.Banking.Models;
using System.Security.Claims;

namespace s3827202_s3687609_a2.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BankDbUser class
    public class BankDbUser : IdentityUser
    {
        [PersonalData]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
