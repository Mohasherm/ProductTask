using Microsoft.AspNetCore.Components;
using ProductTaskFrontEnd.Authentication;
using ProductTaskFrontEnd.Interceptor;
using ProductTaskFrontEnd.Service.Security;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductTaskFrontEnd.Service.Base;

public class BaseRepo : IDisposable
{
    protected readonly HttpClient httpClient;
    protected readonly ITokenRepository tokenRepo;
    protected readonly NavigationManager navigationManager;
    protected readonly CustomAuthenticationStateProvider myAuthenticationStateProvider;
    private readonly HttpInterceptorService interceptor;

    public BaseRepo(HttpClient httpClient, ITokenRepository tokenRepo, NavigationManager navigationManager,
        CustomAuthenticationStateProvider myAuthenticationStateProvider, HttpInterceptorService interceptor)
    {
        this.httpClient = httpClient;
        this.tokenRepo = tokenRepo;
        this.navigationManager = navigationManager;
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ar-sy"));
        this.myAuthenticationStateProvider = myAuthenticationStateProvider;
        this.interceptor = interceptor;
    }

    public async Task CheckToken()
    {
        var token = await tokenRepo.GetToken();
        if (token == null)
            navigationManager.NavigateTo("/login", true);

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", $"{token.Token}");
        interceptor.RegisterEvent();
    }

    public void Dispose() => interceptor.DisposeEvent();

}