using FindMyPG.Core;
using FindMyPG.Core.Configs;
using FindMyPG.Service.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Tokens
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IOptions<FindMyPGConfig> _configuration;
        public JwtTokenService(JwtSecurityTokenHandler jwtSecurityTokenHandler, IOptions<FindMyPGConfig> configuration)
        {
            _jwtSecurityTokenHandler =jwtSecurityTokenHandler;
            _configuration = configuration;
        }
        public TokenResult GenerateAccessToken(TokenRequest request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));
           var claims=new List<Claim>()
           {
               new Claim(UserDefault.Claims.Name,request.UserName),
               new Claim(UserDefault.Claims.NameIdentifier,request.UserId.ToString(),ClaimValueTypes.Integer64),
               new Claim(UserDefault.Claims.Iat,DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
               new Claim(UserDefault.Claims.Email,request.Email),
               new Claim(UserDefault.Claims.PhoneNumber,request.PhoneNumber),
               new Claim(UserDefault.Claims.Aud,UserDefault.TokenTypes.AccessToken),
               new Claim(UserDefault.Claims.Role,request.Role.Name)               
           };
            var config = new FindMyPGConfig();
            var expiry = DateTime.Now.AddMinutes(_configuration.Value.JwtAccessTokenExpireInMinutes);
            TokenResult response = GenerateToken(claims, expiry);
            return response;
        }
        private TokenResult GenerateToken(IList<Claim> claims, DateTime expiry)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.JwtSigningKey));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var notBefore = DateTime.Now;
            var result = new TokenResult();
            try
            {
                var securityToken=new JwtSecurityToken(
                    _configuration.Value.JwtValidIssuer,
                    _configuration.Value.JwtValidAudience,
                    claims,
                    notBefore:notBefore,
                    expires:expiry,
                    signingCredentials:creds);
                var token = _jwtSecurityTokenHandler.WriteToken(securityToken);
                result.Token = token;
                result.ExpireIn=CommonHelper.ToUnixTimeSeconds(expiry);
            }
            catch
            {
                //TODO
            }
            return result;
        }
        

    }
}
