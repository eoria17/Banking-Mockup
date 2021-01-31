

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using s3827202_s3687609_a2.Areas.Admin.Models.ViewModels;
using System.Transactions;

namespace s3827202_s3687609_a2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TransactionHistoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        public TransactionHistoryController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        public async Task<IActionResult> Index(int? CustomerID,DateTime? StartTime, DateTime? EndTime)
        {           
            ViewData["CustomerName"] = new SelectList(await GetCustomerList(), "CustomerID", "CustomerName");
           
            var accounts = await GetTransactionList();
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            if (CustomerID.HasValue)
            {
                accounts = accounts.Where(x => x.CustomerID == CustomerID).ToList();
                if (accounts.Count() > 0)
                {
                    foreach (var item in accounts)
                    {
                        if (item.Transactions.Count() > 0)
                        {
                            transactions.AddRange(item.Transactions.Where(x => x.ModifyDate >= StartTime && x.ModifyDate < Convert.ToDateTime(EndTime).AddDays(1)).ToList());
                        }
                    }
                }
                ViewData["StartTime"] =Convert.ToDateTime(StartTime).ToString("yyyy-MM-dd");
                ViewData["EndTime"] =Convert.ToDateTime(EndTime).ToString("yyyy-MM-dd");
                ViewData["CustomerName"] = new SelectList(await GetCustomerList(), "CustomerID", "CustomerName",CustomerID);
            }
            else
            {
                if (accounts.Count() > 0)
                {
                    foreach (var item in accounts)
                    {
                        if (item.Transactions.Count() > 0)
                        {
                            transactions.AddRange(item.Transactions.ToList());
                        }
                    }
                }
                ViewData["StartTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                ViewData["EndTime"] = DateTime.Now.ToString("yyyy-MM-dd");                
            }
            return View(transactions);
        }       
        public async Task<List<AccountViewModel>> GetTransactionList()
        {
            var response = await Client.GetAsync("api/History");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
           return JsonConvert.DeserializeObject<List<AccountViewModel>>(result);
        }
        public async Task<List<CustomerViewModel>> GetCustomerList()
        {
            var response = await Client.GetAsync("api/Customer");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
            return JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);
        }       
    }
}
