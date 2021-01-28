using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbcaWebAPI.Models.Repository;
using MbcaWebAPI.Data;
using MbcaWebAPI.Models.ViewModels;

namespace MbcaWebAPI.Models.DataManager
{
    public class CustomerManager : IDataRepository<CustomerViewModel, int>
    {
        private readonly MbcaDbContext _context;

        public CustomerManager(MbcaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            var customer = _context.Customer.ToList();

            var viewModel = customer.Select(x => new CustomerViewModel { 
                CustomerID = x.CustomerID,
                CustomerName = x.CustomerName,
                TFN = x.TFN,
                Address = x.Address,
                City = x.City,
                State = x.State,
                PostCode = x.PostCode,
                Phone = x.Phone,
                Status = x.Status
            }).ToList();

            return viewModel;
        }

        public CustomerViewModel Get(int id)
        {
            var x = _context.Customer.Find(id);

            return new CustomerViewModel
            {
                CustomerID = x.CustomerID,
                CustomerName = x.CustomerName,
                TFN = x.TFN,
                Address = x.Address,
                City = x.City,
                State = x.State,
                PostCode = x.PostCode,
                Phone = x.Phone,
                Status = x.Status
            };
        }

        public async Task<Customer> Lock(int id)
        {
            var user = await _context.Customer.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Status = CustomerStatus.Locked;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Customer> Unlock(int id)
        {
            var user = await _context.Customer.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            await Task.Delay(60000);

            user.Status = CustomerStatus.Unlocked;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
