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
            var datenow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            if (list.Count <= 0)
            {
                return;
            }
            foreach (var item in list)
            {
                if (item.Account.FreeTransaction > 0)
                {
                    if (item.Account.Balance > item.Amount)
                    {
                        if (item.Period == Period.OnceOff)
                        {
                            if (datenow == item.ScheduleDate)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    item.Account.FreeTransaction = item.Account.FreeTransaction - 1;
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                        else if (item.Period == Period.Monhly)
                        {
                            if (item.ScheduleDate.AddDays(30) == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    item.Account.FreeTransaction = item.Account.FreeTransaction - 1;
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }

                        }
                        else
                        {
                            if (item.ScheduleDate.AddDays(90) == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount;
                                    item.Account.FreeTransaction = item.Account.FreeTransaction - 1;
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }
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
                        if (item.Period == Period.OnceOff)
                        {
                            if (datenow == item.ScheduleDate)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.ServiceCharge,
                                        AccountNumber = item.AccountNumber,
                                        Amount = decimal.Parse(ServiceChargeAmount.ToString()),
                                        Comment = "ServiceCharge",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount - decimal.Parse(ServiceChargeAmount.ToString());
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                        else if (item.Period == Period.Monhly)
                        {
                            if (item.ScheduleDate.AddDays(30) == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.ServiceCharge,
                                        AccountNumber = item.AccountNumber,
                                        Amount = decimal.Parse(ServiceChargeAmount.ToString()),
                                        Comment = "ServiceCharge",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount - decimal.Parse(ServiceChargeAmount.ToString());
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }

                        }
                        else
                        {
                            if (item.ScheduleDate.AddDays(90) == datenow)
                            {
                                var count = context.Transaction.Where(x => x.AccountNumber == item.AccountNumber && x.TransactionType == TransactionType.Withdrawal && x.Amount == item.Amount && x.ModifyDate == datenow).Count();
                                if (count > 0)
                                {
                                    //pay over
                                }
                                else
                                {
                                    Transaction transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.Withdrawal,
                                        AccountNumber = item.AccountNumber,
                                        Amount = item.Amount,
                                        Comment = "Transfer",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    transaction = new Transaction()
                                    {
                                        TransactionType = TransactionType.ServiceCharge,
                                        AccountNumber = item.AccountNumber,
                                        Amount = decimal.Parse(ServiceChargeAmount.ToString()),
                                        Comment = "ServiceCharge",
                                        ModifyDate = datenow
                                    };
                                    context.Transaction.Add(transaction);
                                    await context.SaveChangesAsync();
                                    item.Account.Balance = item.Account.Balance - item.Amount - decimal.Parse(ServiceChargeAmount.ToString());
                                    context.Account.Update(item.Account);
                                    await context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        //Not Enough Money 
                    }
                }
            }

            await context.SaveChangesAsync();

        }
    }

}
