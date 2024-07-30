using System;

namespace ProductTaskFrontEnd.Service.Security.Dto;
public class TokenDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
}