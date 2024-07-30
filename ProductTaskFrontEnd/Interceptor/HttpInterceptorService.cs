using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace ProductTaskFrontEnd.Interceptor;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navManager;
    public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
    {
        _interceptor = interceptor;
        _navManager = navManager;
    }

    public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;

    private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        if (!e.Response.IsSuccessStatusCode)
        {
            var statusCode = e.Response.StatusCode;
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _navManager.NavigateTo("/login", true);
            }
        }
    }
    public void DisposeEvent()
    {
        _interceptor.AfterSend -= InterceptResponse;
    }
}
