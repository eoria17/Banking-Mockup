using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using s3827202_s3687609_a2.Areas.Banking.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using s3827202_s3687609_a2.Areas.Identity.Data;

namespace s3827202_s3687609_a2.Data
{
    public static class SeedData
    { 

        public static async Task AdminSeed(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<BankDbUser>>();
            string[] roleNames = { "Admin", "Customer" };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var poweruser = new BankDbUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            //Ensure you have these values in your appsettings.json file
            string userPWD = "Abc123!";
            var _user = await UserManager.FindByEmailAsync(poweruser.Email);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }


            var newUser1 = new BankDbUser
            {
                UserName = "s3687609",
                Email = "s3687609@student.rmit.edu.au",
                EmailConfirmed = true,
                CustomerID = 2200
            };


            //Ensure you have these values in your appsettings.json file
            string userPWD1 = "Abc123!";
            var _user2 = await UserManager.FindByEmailAsync(newUser1.Email);

            if (_user2 == null)
            {
                var createNewUser = await UserManager.CreateAsync(newUser1, userPWD1);
                if (createNewUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(newUser1, "Customer");

                }
            }
        }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<BankDbContext>();

            

            //
            //
            //
            //preload data
            // Look for customers.
            if (context.Customer.Any())
                return; // DB has already been seeded.

            context.Customer.AddRange(
                new Customer
                {
                    CustomerID = 2100,
                    CustomerName = "Theo Riandy",
                    Address = "123 Fake Street",
                    City = "Melbourne",
                    PostCode = "3000",
                    Phone = "+6112345678",
                    Status = CustomerStatus.Unlocked
                },
                new Customer
                {
                    CustomerID = 2200,
                    CustomerName = "Shaoxuan Wei",
                    Address = "456 Real Road",
                    City = "Melbourne",
                    PostCode = "3005",
                    Phone = "+6112345678",
                    Status = CustomerStatus.Unlocked
                },
                new Customer
                {
                    CustomerID = 2300,
                    CustomerName = "Shekhar Kalra",
                    Phone = "+6112345678",
                    Status = CustomerStatus.Unlocked

                });

            /*context.Login.AddRange(
                new Login
                {
                    LoginID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2"
                },
                new Login
                {
                    LoginID = "38074569",
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04"
                },
                new Login
                {
                    LoginID = "17963428",
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE"
                });*/

            context.Account.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountType.Saving,
                    CustomerID = 2100,
                    FreeTransaction = 4,
                    Balance = 500
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountType.Checking,
                    CustomerID = 2100,
                    FreeTransaction = 4,
                    Balance = 500
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountType.Saving,
                    CustomerID = 2200,
                    FreeTransaction = 4,
                    Balance = 500.95m
                },
                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountType.Checking,
                    CustomerID = 2300,
                    FreeTransaction = 4,
                    Balance = 1250.50m
                });

            const string initialDeposit = "Initial deposit";
            const string format = "dd/MM/yyyy hh:mm:ss tt";

            context.Transaction.AddRange(
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("08/06/2020 08:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("09/06/2020 09:00:00 AM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("09/06/2020 01:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("09/06/2020 03:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    Amount = 100,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("10/06/2020 11:00:00 AM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4101,
                    Amount = 500,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("08/06/2020 08:30:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 500,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("08/06/2020 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    Amount = 0.95m,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("08/06/2020 09:00:00 PM", format, null)
                },
                new Transaction
                {
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4300,
                    Amount = 1250.50m,
                    Comment = initialDeposit,
                    ModifyDate = DateTime.ParseExact("08/06/2020 10:00:00 PM", format, null)
                });

            context.Payee.AddRange(
               new Payee
               {
                   PayeeName = "RMIT",
                   Address = "842 Union Avenue",
                   City = "Melbourne",
                   State = "VIC",
                   PostCode = 3106,
                   Phone = "+6112345678"
               },
               new Payee
               {
                   PayeeName = "Electric company",
                   Address = "558 Fuller Way",
                   City = "Melbourne",
                   State = "VIC",
                   PostCode = 3110,
                   Phone = "+6112345678"
               }
               );

            context.SaveChanges();

            await AdminSeed(serviceProvider);
        }
    }
}
