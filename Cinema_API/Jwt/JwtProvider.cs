using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cinema_API.Jwt
{
    public class JwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString()), new("Admin", user.IsAdmin.ToString())];
            
            var signingsCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingsCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
