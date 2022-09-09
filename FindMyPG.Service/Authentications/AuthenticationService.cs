using FindMyPG.Core.Entities;
using FindMyPG.Service.Tokens;
using FindMyPG.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Authentications
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthenticationService(IJwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }
        public async Task<AuthenticationResult> AuthenticateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            var tokenRequest = new TokenRequest()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                UserId = user.Id,
                Role = await _userService.GetRolesByUser(user)
            };
            var accessToken = _jwtTokenService.GenerateAccessToken(tokenRequest);
            if (!accessToken.Success && !await _userService.SetAccessTokenAsync(user, accessToken.Token))
            {
                accessToken.Token = null;
            }
            return new AuthenticationResult(accessToken);
        }
    }
}
