using Kanban.API.Interfaces;
using Kanban.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kanban.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly string key;
        private readonly string issuer;
        public TokenService(IConfiguration config)
        {
            _config = config;
            key = _config["jwt:key"]!;
            issuer = _config["jwt:ValidIssuer"]!;
        }
        private const double ExpiryDurationMinutes = 30;

        public string BuildToken(LoginData loginData)
        {
            var claims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, loginData.Login },
                { ClaimTypes.NameIdentifier, loginData.Login }
            };

            //var key = _config["jwt:key"];
            //var issuer = _config["jwt:ValidIssuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var descriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.UtcNow,
                Claims = claims,
                Issuer = issuer,
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddMinutes(ExpiryDurationMinutes)
            };

            var handler = new JwtSecurityTokenHandler();
            var encodedToken = handler.CreateEncodedJwt(descriptor);
            return encodedToken;
        }

        public ClaimsIdentity GenerateClaims(LoginData loginData)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, loginData.Login),
                new Claim(ClaimTypes.NameIdentifier,
                    loginData.Login)
            };

            var claim = new ClaimsIdentity(claims);

            return claim;
        }
        public bool IsTokenValid(string token)
        {
            //var key = _config["jwt:key"];
            //var issuer = _config["jwt:ValidIssuer"];
            if (String.IsNullOrEmpty(token))
                return false;

            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = mySecurityKey,
                    }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
