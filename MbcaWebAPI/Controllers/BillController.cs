using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MbcaWebAPI.Models.DataManager;
using MbcaWebAPI.Models.ViewModels;

namespace MbcaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {

        private readonly BillManager _repo;

        public BillController(BillManager repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<BillViewModel> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<BillViewModel> Block(int id)
        {
            return await _repo.Lock(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<BillViewModel> Unblock(int id)
        {
            return await _repo.Unlock(id);
        }
    }
}
