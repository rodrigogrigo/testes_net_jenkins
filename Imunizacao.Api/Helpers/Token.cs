using RgCidadao.Api.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RgCidadao.Api.ViewModels.Cadastro;
using Microsoft.AspNetCore.Identity;

namespace RgCidadao.Api.Helpers
{
    public class Token
    {
        private readonly IConfiguration _configuration;
        public Token(IConfiguration _config)
        {
            _configuration = _config;
        }

        public UserToken BuildToken(UserInfo userInfo, int iduser)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, iduser.ToString()),
                new Claim(iduser.ToString(), "imunizacao"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddDays(7);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }


    }
}
