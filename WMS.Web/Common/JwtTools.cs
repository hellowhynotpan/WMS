using System;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WMS.Model;

namespace WMS.WebApi.Common
{
    public class JwtTools
    {
        public static string GetToken(SysUser user)
        {
            var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,user.Account),
                    new Claim("id",user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDCD-DSADA-FSA-GFDAG-GDMDEAI-VF"));
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000", //颁发者
                audience: "http://localhost:5000", //接收者
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
