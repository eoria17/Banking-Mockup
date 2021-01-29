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
    public class HistoryController : ControllerBase
    {

        private readonly HistoryManager _repo;

        public HistoryController(HistoryManager repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<AccountViewModel> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public AccountViewModel Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
