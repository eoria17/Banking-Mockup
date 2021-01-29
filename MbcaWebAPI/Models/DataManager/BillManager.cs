using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbcaWebAPI.Data;
using MbcaWebAPI.Models.Repository;
using MbcaWebAPI.Models.ViewModels;

namespace MbcaWebAPI.Models.DataManager
{
    public class BillManager : IDataRepository<BillViewModel, int>, IDataStatusRepository<BillViewModel, int>
    {
        private readonly MbcaDbContext _context;

        public BillManager(MbcaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BillViewModel> GetAll()
        {
            var billPays = _context.Account.
                Select(x => x.BillPays).SelectMany(x => x).ToList();

            var viewModel = billPays.Select(x => new BillViewModel { 
                BillPayID = x.BillPayID,
                AccountNumber = x.AccountNumber,
                PayeeID = x.PayeeID,
                Amount = x.Amount,
                ScheduleDate = x.ScheduleDate,
                Period = x.Period,
                ModifyDate = x.ModifyDate,
                Status = x.Status
            }).ToList();

            return viewModel;
        }

        public BillViewModel Get(int id)
        {
            var x = _context.BillPay.Find(id);

            return new BillViewModel
            {
                BillPayID = x.BillPayID,
                AccountNumber = x.AccountNumber,
                PayeeID = x.PayeeID,
                Amount = x.Amount,
                ScheduleDate = x.ScheduleDate,
                Period = x.Period,
                ModifyDate = x.ModifyDate,
                Status = x.Status
            };
        }

        public async Task<BillViewModel> Lock(int id)
        {
            var x = await _context.BillPay.FindAsync(id);

            if (x == null)
            {
                return null;
            }

            x.Status = BillPayStatus.Blocked;

            await _context.SaveChangesAsync();

            return new BillViewModel
            {
                BillPayID = x.BillPayID,
                AccountNumber = x.AccountNumber,
                PayeeID = x.PayeeID,
                Amount = x.Amount,
                ScheduleDate = x.ScheduleDate,
                Period = x.Period,
                ModifyDate = x.ModifyDate,
                Status = x.Status
            };
        }

        public async Task<BillViewModel> Unlock(int id)
        {
            var x = await _context.BillPay.FindAsync(id);

            if (x == null)
            {
                return null;
            }

            x.Status = BillPayStatus.Available;

            await _context.SaveChangesAsync();

            return new BillViewModel
            {
                BillPayID = x.BillPayID,
                AccountNumber = x.AccountNumber,
                PayeeID = x.PayeeID,
                Amount = x.Amount,
                ScheduleDate = x.ScheduleDate,
                Period = x.Period,
                ModifyDate = x.ModifyDate,
                Status = x.Status
            };
        }
    }
}
