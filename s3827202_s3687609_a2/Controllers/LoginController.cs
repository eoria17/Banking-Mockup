using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Data;

namespace s3827202_s3687609_a2.Controllers
{
    public class LoginController : Controller
    {
        private readonly BankDBContext _context;
        public string Index()
        {
            return "This is the login page";
        }
    }
}
