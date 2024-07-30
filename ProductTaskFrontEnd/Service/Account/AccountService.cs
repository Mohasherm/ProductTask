using Microsoft.AspNetCore.Components;
using ProductTaskFrontEnd.Authentication;
using ProductTaskFrontEnd.Interceptor;
using ProductTaskFrontEnd.Service.Account.Dto;
using ProductTaskFrontEnd.Service.Base;
using ProductTaskFrontEnd.Service.Security;
using ProductTaskFrontEnd.Service.Security.Dto;
using System.Net.Http.Json;

namespace ProductTaskFrontEnd.Service.Account;

public class AccountService : BaseRepo, IAccountService
{


    readonly string BaseUrl = "Account";

    public AccountService(HttpClient httpClient, ITokenRepository tokenRepo, NavigationManager navigationManager, CustomAuthenticationStateProvider myAuthenticationStateProvider, HttpInterceptorService interceptor) : base(httpClient, tokenRepo, navigationManager, myAuthenticationStateProvider, interceptor)
    {
    }


    public async Task<GetResult<TokenDto>> Login(LoginRequest request)
    {
        var result = new GetResult<TokenDto>();

        var data = await httpClient.PostAsJsonAsync($"{BaseUrl}/Login", request);
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<TokenDto>();
            result.Data.ExpiryDate = DateTime.UtcNow.AddDays(1);
            await tokenRepo.SetToken(result.Data);
            myAuthenticationStateProvider.StateChanged();
        }

        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task Logout()
    {
        await tokenRepo.RemoveToken();
        myAuthenticationStateProvider.StateChanged();
    }

    public async Task<GetResult<bool>> Register(RegisterRequest request)
    {
        var result = new GetResult<bool>();

        var data = await httpClient.PostAsJsonAsync($"{BaseUrl}/Register", request);
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }

    public async Task<GetResult<bool>> Update(UpdateRequest request)
    {
        var result = new GetResult<bool>();

        await CheckToken();

        var data = await httpClient.PutAsJsonAsync($"{BaseUrl}/Update", request);
        if (data.IsSuccessStatusCode)
        {
            result.Data = await data.Content.ReadFromJsonAsync<bool>();
        }
        else
        {
            var error = await data.Content.ReadFromJsonAsync<GetErrorMessage>();
            result.ErrorMessage = error.ErrorMessage;
        }
        return result;
    }
}
