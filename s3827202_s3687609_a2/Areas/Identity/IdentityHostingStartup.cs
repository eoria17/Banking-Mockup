using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using s3827202_s3687609_a2.Areas.Identity.Data;
using s3827202_s3687609_a2.Data;

[assembly: HostingStartup(typeof(s3827202_s3687609_a2.Areas.Identity.IdentityHostingStartup))]
namespace s3827202_s3687609_a2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
        }
    }
}