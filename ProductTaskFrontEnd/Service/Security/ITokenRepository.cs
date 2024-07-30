using ProductTaskFrontEnd.Service.Security.Dto;
using System.Threading.Tasks;

namespace ProductTaskFrontEnd.Service.Security;

public interface ITokenRepository
{
    Task<TokenDto> GetToken();
    Task RemoveToken();
    Task SetToken(TokenDto dto);
}
