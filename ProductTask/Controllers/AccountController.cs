using ButterflyApi.Base.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using ProductTask.Repository.Account;
using ProductTask.Repository.Account.Dto;
using ProductTask.Repository.Permission.Dto;
using ProductTask.Repository.Security.Token.Dto;
using ProductTask.Shared.Enums;
using ProductTask.Utill;
using System;
using System.Collections.Generic;
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
        //[DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Set))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await accountRepository.Login(request);
            return result.GetResult();
        }

        [HttpPost]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Set))]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await accountRepository.Register(request);
            return result.GetResult();
        }

        [HttpPut]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Set))]
        public async Task<IActionResult> Update(UpdateRequest request)
        {
            var result = await accountRepository.Update(request);
            return result.GetResult();
        }

        [HttpGet]
        [Produces(typeof(List<GetAllUsersResponse>))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Get))]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await accountRepository.GetAllUsers();
            return result.GetResult();
        }

        [HttpGet]
        [Produces(typeof(GetAllUsersResponse))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Get))]
        public async Task<IActionResult> GetUserById([FromQuery] Guid Id)
        {
            var result = await accountRepository.GetUserById(Id);
            return result.GetResult();
        }

        [HttpDelete]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Delete))]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var result = await accountRepository.DeleteUser(Id);
            return result.GetResult();
        }

        [HttpGet]
        [Produces(typeof(List<GetRolesDto>))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Get))]
        public async Task<IActionResult> GetRolesCp()
        {
            var result = await accountRepository.GetRolesCp();
            return result.GetResult();
        }


        [HttpPost]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Set))]
        public async Task<IActionResult> AddRole(string Name)
        {
            var result = await accountRepository.AddRole(Name);
            return result.GetResult();
        }

        [HttpDelete]
        [Produces(typeof(bool))]
        [DynamicAuthorize(nameof(ControllerNames.Account), nameof(RequestType.Delete))]
        public async Task<IActionResult> DeleteRole(Guid Id)
        {
            var result = await accountRepository.DeleteRole(Id);
            return result.GetResult();
        }

    }
}
