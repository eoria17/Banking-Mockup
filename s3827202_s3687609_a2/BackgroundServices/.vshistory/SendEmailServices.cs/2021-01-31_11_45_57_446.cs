using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using s3827202_s3687609_a2.Data;
using s3827202_s3687609_a2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace s3827202_s3687609_a2.BackgroundServices
{

    public class SendEmailServices : BackgroundService
    {
        private readonly IServiceProvider _services;
        public SendEmailServices(IServiceProvider services)
        {
            _services = services;
        }
    }
}
