using Microsoft.AspNetCore.Components.Authorization;
using ProductTaskFrontEnd.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductTaskFrontEnd.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenRepository tokenService;

    public CustomAuthenticationStateProvider(ITokenRepository tokenService)
    {
        this.tokenService = tokenService;
    }

    public void StateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenDTO = await tokenService.GetToken();
        var identity = string.IsNullOrEmpty(tokenDTO?.Token) || string.IsNullOrEmpty(tokenDTO?.RefreshToken) || tokenDTO.ExpiryDate < DateTimeOffset.UtcNow
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDTO.Token), "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));

    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}