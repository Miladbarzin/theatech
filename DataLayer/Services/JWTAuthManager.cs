using DataLayer.DBModels;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class JWTAuthManager:IJWTAuthManager
    {
        private readonly IConfiguration _configuration;
        SQLServerContext _db;

        public JWTAuthManager(IConfiguration configuration,
            SQLServerContext db )
        {
            _db= db;
            _configuration = configuration;
 
        }
        

        public async Task<string> GenerateJWT(AspNetUser user)
        {
          
            SecurityHelper securityHelper = new SecurityHelper();

            var claims = new List<Claim>
                {
                    new Claim ("UserId", user.Id.ToString()),
                    new Claim ("Name",  user.UserName),
                };

          //  var expireTime = await _db.Subscriptions.Where(x => x.Id == user.ActiveSubscriptionId).Select(e => e.ExpireTime).FirstOrDefaultAsync();

            string key = _configuration["JwtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWtConfig:issuer"],
                audience: _configuration["JWtConfig:audience"],
                expires: null,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var userJwtToken = new UserJwtToken
            {
                TokenExp = null,
                TokenHash = securityHelper.Getsha256Hash(jwtToken),
                UserId= user.Id,
            };
            //await _db.UserJwtTokens.AddAsync(userJwtToken);
            //await _db.SaveChangesAsync();

            return  jwtToken ;
        }

        public async Task RemoveToken(string userId)
        {
            var userToken = _db.UserJwtTokens.Where(p => p.UserId == userId).ToList();
            _db.UserJwtTokens.RemoveRange(userToken);
            _db.SaveChanges();
        }

    }
}
