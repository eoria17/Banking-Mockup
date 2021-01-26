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

namespace s3827202_s3687609_a2.BackgroundJob
{
    public class BillPayServices : BackgroundService
    {
        private readonly IServiceProvider _services;
        public BillPayServices(IServiceProvider services)
        {
            _services = services;           
        }        

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);   
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
       
        private async Task DoWork(CancellationToken cancellationToken)
        {         

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BankDBContext>();

            List<BillPay> list = await context.BillPay.Where(a => a.ScheduleDate <= DateTime.Now).Include(a => a.Account).Include(a => a.Payee).ToListAsync(cancellationToken);
            foreach (var item in list)
            {
                if (item.Account.FreeTransaction > 0)
                {
                    if (item.Account.Balance > item.Amount)
                    {
                        if (item.Period == Period.OnceOff)
                        {
                            
                        }
                    }
                    else
                    {
                        //Not Enough Money 
                    }
                }
                else
                {
                    //pay ServiceCharge
                    double ServiceChargeAmount = 0.2;
                    if (item.Account.Balance > (item.Amount + decimal.Parse(ServiceChargeAmount.ToString())))
                    {

                    }
                    else
                    {
                        //Not Enough Money 
                    }
                }
            }

            await context.SaveChangesAsync(cancellationToken);
          
        }
    }
   
}
