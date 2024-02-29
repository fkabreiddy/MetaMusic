using MetaMusic.Data.OtherEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MetaMusic.Authentication
{
    public class JwtAuthentication
    {

        private string CreateJWT(LoginResponse user)
        {
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("mainurgotM_7")); // NOTE: SAME KEY AS USED IN Program.cs FILE; DO NOT REUSE THIS GUID
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[] // NOTE: could also use List<Claim> here
			{
                new Claim(ClaimTypes.Name, user.Nombre), // NOTE: this will be the "User.Identity.Name" value
				new Claim(JwtRegisteredClaimNames.Sub, user.Correo),
                new Claim(ClaimTypes.Role, user.Rol.Tipo),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                // NOTE: this could a unique ID assigned to the user by a database
			};

            var token = new JwtSecurityToken(issuer: "localhost:7277", audience: "localhost:7277", claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials); // NOTE: ENTER DOMAIN HERE
            var jsth = new JwtSecurityTokenHandler();
            return jsth.WriteToken(token);
        }
    }
}
