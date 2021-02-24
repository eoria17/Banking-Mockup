using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbcaWebAPI.Models.Repository;
using MbcaWebAPI.Data;
using MbcaWebAPI.Models.ViewModels;
using System.Threading;

namespace MbcaWebAPI.Models.DataManager
{
    public class CustomerManager : IDataRepository<CustomerViewModel, int>, IDataStatusRepository<CustomerViewModel, int>
    {
        private readonly MbcaDbContext _context;

        public CustomerManager(MbcaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            var customer = _context.Customer.ToList();

            var viewModel = customer.Select(x => new CustomerViewModel
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

        public async Task<CustomerViewModel> Lock(int id)
        {
            var x = await _context.Customer.FindAsync(id);

            if (x == null)
            {
                return null;
            }

            x.Status = CustomerStatus.Locked;

            await _context.SaveChangesAsync();

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

        public async Task<CustomerViewModel> Unlock(int id)

        {
            await Task.Delay(30000);

            var x =  await _context.Customer.FindAsync(id);

            x.Status = CustomerStatus.Unlocked;

            await _context.SaveChangesAsync();

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
    }
}
