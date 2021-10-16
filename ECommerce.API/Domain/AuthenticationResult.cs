using System.Collections.Generic;

namespace ECommerce.API.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
        //
        public string UserId { get; set; }
    }
}
