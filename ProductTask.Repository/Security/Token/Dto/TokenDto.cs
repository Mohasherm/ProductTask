using System;

namespace ProductTask.Repository.Security.Token.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
