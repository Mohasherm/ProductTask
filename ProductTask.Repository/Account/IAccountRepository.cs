using ProductTask.Base.OperationResult;
using ProductTask.Repository.Account.Dto;
using ProductTask.Repository.Security.Token.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductTask.Repository.Account
{
    public interface IAccountRepository
    {
        Task<OperationResult<TokenDto>> Login(LoginRequest request);
        Task<OperationResult<bool>> Register(RegisterRequest request);
        Task<OperationResult<bool>> Update(UpdateRequest request);
        Task<OperationResult<List<GetAllUsersResponse>>> GetAllUsers();
        Task<OperationResult<GetAllUsersResponse>> GetUserById(Guid Id);
        Task<OperationResult<bool>> DeleteUser(Guid Id);

    }
}
