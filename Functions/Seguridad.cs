using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sistema.Functions
{
    public class Seguridad
    {
        string nombre_sesion = new Configuracion().nombre_sesion;

        string jwt_key = new Configuracion().jwt_key;
        string jwt_issuer = new Configuracion().jwt_issuer;
        string jwt_audience = new Configuracion().jwt_audience;

        public string GenerarTokenJWT(int id,string usuario,int? id_tipo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(id)),
                new Claim(ClaimTypes.Name, usuario),
                new Claim(ClaimTypes.Role, Convert.ToString(id_tipo))
            };

            var token = new JwtSecurityToken(
                jwt_issuer,
                jwt_audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int CargarIdToken(string token)
        {
            var tokenS = new JwtSecurityToken(token);
            var id = tokenS.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Convert.ToInt32(id);
        }

        public string cargarNombreToken(string token)
        {
            var tokenS = new JwtSecurityToken(token);
            var name = tokenS.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            return name;
        }

        public int cargarTipoToken(string token)
        {
            var tokenS = new JwtSecurityToken(token);
            var role = tokenS.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            return Convert.ToInt32(role);
        }

        public bool ValidarSesionJWT(string token)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt_key));

            TokenValidationParameters options = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = jwt_issuer,
                ValidAudience = jwt_audience
            };

            if (ValidarToken(token, options))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidarToken(string token, TokenValidationParameters options)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, options, out securityToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
