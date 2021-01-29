using McbaAdmin.Filters;
using McbaAdmin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace McbaAdmin.Controllers
{
    [AuthorizeCustomer]
    public class CustomerLockController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        public CustomerLockController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;
        public async Task<IActionResult> Index()
        {
            var customerList = await GetCustomerList();
            return View(customerList);
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
        public async Task<IActionResult> Lock(int? id)
        {
            if (id == null)
                return NotFound();
            var response = await Client.GetAsync($"api/Customer/Lock/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            //var result = await response.Content.ReadAsStringAsync();
            //var customer = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            return RedirectToAction("Index");
        }
    }
}
