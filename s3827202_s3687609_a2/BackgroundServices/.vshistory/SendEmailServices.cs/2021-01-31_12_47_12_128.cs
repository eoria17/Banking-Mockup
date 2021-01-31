using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using s3827202_s3687609_a2.Areas.Identity.Data;
using s3827202_s3687609_a2.Common;
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
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoWork(cancellationToken);
                await Task.Delay(TimeSpan.FromMinutes(3), cancellationToken);
            }
        }
        private async Task DoWork(CancellationToken cancellationToken)
        {

            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
            
            List<Transaction> list = await context.Transaction.Where(x=>x.TransactionStatus==TransactionStatus.Idle).ToListAsync(cancellationToken);
            List<Transaction> listOver = await context.Transaction.Where(x => x.TransactionStatus == TransactionStatus.Reported).ToListAsync(cancellationToken);
            SendEmail sendEmail = new SendEmail();
            SendEmail.EmailTemp emailTemp = new SendEmail.EmailTemp();           
            List<Customer> customers = await context.Customer.Include(x=>x.Accounts).ToListAsync(); 
            if (list.Count() > 0)
            {
                if (customers.Count() > 0)
                {
                    foreach (var item in customers)
                    {
                      var newTrancitonList=list.Where(x => item.Accounts.Select(y => y.AccountNumber).Contains(x.AccountNumber)).ToList();
                        var accountNum = item.Accounts.ToList();
                        foreach (var account in accountNum)
                        {                            
                            emailTemp.accountID = account.AccountNumber.ToString();
                            if (listOver.Count() > 0)
                            {
                                if (listOver.Where(x => x.AccountNumber == account.AccountNumber).ToList().Count() > 0)
                                {
                                    emailTemp.balance = account.Balance;
                                }
                                else
                                {
                                    emailTemp.balance = 0;
                                }
                            }
                            else
                            {
                                emailTemp.balance = 0;
                            }
                            emailTemp.customerAddress = item.Address;
                            emailTemp.receiveEmail = context.Users.Where(x => x.CustomerID
                              == item.CustomerID).FirstOrDefault().Email;
                            emailTemp.receiveName = item.CustomerName;
                            emailTemp.transactions = newTrancitonList.Where(x => x.AccountNumber == account.AccountNumber).ToList();
                           bool result= await sendEmail.Sendemail(emailTemp);
                            if (result)
                            {
                                foreach (var tran in emailTemp.transactions)
                                {
                                    tran.TransactionStatus = TransactionStatus.Reported;
                                    context.Transaction.Update(tran);
                                }
                                await context.SaveChangesAsync();
                            }
                        }                       
                    }
                }
            }            
            

        }
    }
}
