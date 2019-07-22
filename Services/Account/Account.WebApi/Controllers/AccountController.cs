using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Application.Services;
using Account.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppService _service;

        public AccountController(IAccountAppService service)
        {
            _service = service;
        }

        [HttpPost]
        //[Route("AccountLogin")]
        public AccountVM Login([FromBody] AccountVM model)
        {
            return new AccountVM();
        }

        [HttpGet("{id}")]
        //[Route("AccountSelf/id")]
        public AccountVM GetSelfAccount(Int64 id)
        {
            return _service.GetById(id);
            
        }

        [HttpGet]
        public HasBodyResponseVM<AccountVM> GetOtherAccount(Int64 otherId)
        {
            var account = _service.GetById(otherId);
            return new HasBodyResponseVM<AccountVM>(account);
        }
    }
}