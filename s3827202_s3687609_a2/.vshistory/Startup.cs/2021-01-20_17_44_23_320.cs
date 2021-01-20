using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using s3827202_s3687609_a2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s3827202_s3687609_a2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BillPayContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddDbContext<AccountContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddDbContext<CustomerContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddDbContext<LoginContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddDbContext<PayeeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

            services.AddDbContext<TransactionContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BankDB")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
