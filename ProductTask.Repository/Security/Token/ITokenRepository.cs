using ProductTask.Repository.Security.Token.Dto;
using System;
using System.Threading.Tasks;

namespace ProductTask.Repository.Security.Token
{
    public interface ITokenRepository
    {
        Task<TokenDto> GenerateJwtToken(Guid userId);
    }

}