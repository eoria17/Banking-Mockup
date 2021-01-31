﻿using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();

            // Get all the data of the transfer date earlier than the next day
            List<BillPay> list = await context.BillPay.Where(a => a.ScheduleDate <= DateTime.Now&&a.Status==BillPayStatus.Available).Include(a => a.Account).Include(a => a.Payee).ToListAsync(cancellationToken);
            var datenow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            if (list.Count <= 0)
            {
                return;
            }
            foreach (var item in list)
            {
                    if (item.Account.Balance > item.Amount)
                    {
                        if (item.Period == Period.OnceOff)
                        {
                            if (datenow == item.ScheduleDate)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.BillPay && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.BillPay,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Bill pay",
                                        ModifyDate = datenow,
                                        TransactionStatus=TransactionStatus.Idle
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    context.Account.Update(item.Account);
                                    item.Status = BillPayStatus.Done;
                                    context.BillPay.Update(item);
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                        else if (item.Period == Period.Monthly)
                        {
                            var date = item.ScheduleDate;
                            while (true)
                            {
                                date = date.AddMonths(1);
                                if (date >= datenow)
                                {
                                    break;
                                }
                            }
                            if (date == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.BillPay && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.BillPay,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Bill pay",
                                        ModifyDate = datenow,
                                        TransactionStatus = TransactionStatus.Idle
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }

                        }
                        else
                        {
                            var date = item.ScheduleDate;
                            while (true)
                            {
                                date = date.AddMonths(3);
                                if (date >= datenow)
                                {
                                    break;
                                }
                            }
                            if (date == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.BillPay && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.BillPay,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Bill pay",
                                        ModifyDate = datenow,
                                        TransactionStatus = TransactionStatus.Idle
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    else {
                        var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.BillPay && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                        if (count > 0)
                        {
                            //pay over
                        }
                        else
                        {

                        }
                    }
                   
            }

            await context.SaveChangesAsync();

        }
    }

}
