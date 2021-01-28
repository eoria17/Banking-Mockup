using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbcaWebAPI.Models.DataManager;
using MbcaWebAPI.Models;
using MbcaWebAPI.Models.ViewModels;

namespace MbcaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManager _repo;

        public CustomerController(CustomerManager repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<CustomerViewModel> Get()
        {
            return _repo.GetAll();
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<Customer> Lock(int id)
        {
            var user = await _repo.Lock(id);
            await _repo.Unlock(id);

            return user;
        }
    }
}
