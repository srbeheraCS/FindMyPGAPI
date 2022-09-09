using FindMyPG.Service.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Authentications
{
    public class AuthenticationResult
    {
        public AuthenticationResult(TokenResult accessToken)
        {
            AccessToken=accessToken;    
            Errors = new List<string>();
        }
        public IList<string> Errors { get; set; }
        public bool Success =>AccessToken!=null && AccessToken.Success && !Errors.Any();
        public TokenResult AccessToken { get; set; }
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
    
