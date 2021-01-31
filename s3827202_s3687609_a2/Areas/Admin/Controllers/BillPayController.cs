using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using s3827202_s3687609_a2.Areas.Admin.Models.ViewModels;

namespace s3827202_s3687609_a2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BillPayController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient("api");
        public BillPayController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;
        public async Task<IActionResult> Index()
        {
            var billList = await GetBillList();
            return View(billList);
        }
        public async Task<List<BillViewModel>> GetBillList()
        {
            var response = await Client.GetAsync("api/Bill");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            // Storing the response details received from web api.
            var result = await response.Content.ReadAsStringAsync();

            // Deserializing the response received from web api and storing into a list.
            return JsonConvert.DeserializeObject<List<BillViewModel>>(result);
        }
        public async Task<IActionResult> Lock(int? id)
        {
            if (id == null)
                return NotFound();
            var response = await Client.GetAsync($"api/Bill/Block/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<BillViewModel>(result);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UnLock(int? id)
        {
            if (id == null)
                return NotFound();
            var response = await Client.GetAsync($"api/Bill/Unblock/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<BillViewModel>(result);
            return RedirectToAction("Index");
        }
    }
}
