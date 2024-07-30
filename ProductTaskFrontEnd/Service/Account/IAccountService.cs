using ProductTaskFrontEnd.Service.Account.Dto;
using ProductTaskFrontEnd.Service.Security.Dto;

namespace ProductTaskFrontEnd.Service.Account;

public interface IAccountService
{
    Task<GetResult<TokenDto>> Login(LoginRequest request);
    Task<GetResult<bool>> Register(RegisterRequest request);
    Task<GetResult<bool>> Update(UpdateRequest request);
    Task Logout();
}
