using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Fire.Domain.Entities;
using static FireCs.JWT.JWTGenerator;

namespace FireCs.JWT
{
    public class JWTGenerator
    {

        //        private readonly JwtSettings _jwtSettings;


        //    public GeradorTokenJwtService(IOptions<JwtSettings> jwtOptions)
        //        {
        //            _jwtSettings = jwtOptions.Value;
        //        }

        //        public TokenDto GerarTokenJwt(Users usuario)
        //        {
        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        //            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //            var claims = new[]
        //            {
        //        new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
        //        new Claim("email", usuario.Email),
        //         new Claim("id_usuario", usuario.Cod_usuario.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //};

        //            var token = new JwtSecurityToken(
        //                issuer: _jwtSettings.Issuer,
        //                audience: _jwtSettings.Audience,
        //                claims: claims,
        //                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours),
        //                signingCredentials: creds
        //            );

        //            var tokenDto = new TokenDto()
        //            {
        //                Token = new JwtSecurityTokenHandler().WriteToken(token),
        //                ExpirationTime = DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours)
        //            };

        //            return tokenDto;
        //        }
        //    }

    }
}
