using Blazored.LocalStorage;
using ProductTaskFrontEnd.Service.Security.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductTaskFrontEnd.Service.Security;

public class TokenRepository : ITokenRepository
{
    private readonly ILocalStorageService storageService;
    private readonly HttpClient httpClient;

    public TokenRepository(HttpClient httpClient, ILocalStorageService storageService)
    {
        this.httpClient = httpClient;
        this.storageService = storageService;
    }

    public async Task SetToken(TokenDto tokenDTO)
    {
        await storageService.SetItemAsync("token", tokenDTO);
    }

    public async Task<TokenDto> GetToken()
    {
        return await storageService.GetItemAsync<TokenDto>("token");
    }

    public async Task RemoveToken()
    {
        await storageService.RemoveItemAsync("token");
    }

}
