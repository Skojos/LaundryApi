using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LaundryWebApi.Helpers
{
    public class JwtManager : IJwtManager
    {


        private readonly AppSettings _appSettings;


        public JwtManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;       
        }

        public string GenerateToken(User authUser)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:4200",
                audience: "http://localhost:4200",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials

                );

            /**   
               var tokenHandler = new JwtSecurityTokenHandler();
               var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
               var tokenDescriptor = new SecurityTokenDescriptor
               {
                   Subject = new ClaimsIdentity(new Claim[]
                   {
                       new Claim("id", authUser.Id.ToString())
                   }),
                   Expires = DateTime.UtcNow.AddMinutes(1),
                   SigningCredentials = new SigningCredentials(, SecurityAlgorithms.HmacSha256Signature)
               };

             **/

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
