using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VotaE_API.Interface;
using VotaE_API.ViewModel.Usuario;

namespace VotaE_API.Controllers
{
    [Route("api/autenticacao/")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacao _autenticacaoService;

        public AutenticacaoController(IAutenticacao autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginViewModel loginModel)
        {
            var authenticatedUser = _autenticacaoService.Authenticate(loginModel.Email, loginModel.Senha);

            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(authenticatedUser);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(LoginViewModel usuario)
        {
            byte[] secret = Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = "VotaE",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
