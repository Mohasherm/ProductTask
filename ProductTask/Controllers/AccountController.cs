using ButterflyApi.Base.ErrorHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTask.Repository.Account;
using ProductTask.Repository.Account.Dto;
using ProductTask.Repository.Security.Token.Dto;
using System.Threading.Tasks;

namespace ProductTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }


        [HttpPost]
        [Produces(typeof(TokenDto))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await accountRepository.Login(request);
            return result.GetResult();
        }

        [HttpPost]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await accountRepository.Register(request);
            return result.GetResult();
        }

        [HttpPut]
        [Produces(typeof(bool))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateRequest request)
        {
            var result = await accountRepository.Update(request);
            return result.GetResult();
        }

     

    }
}
