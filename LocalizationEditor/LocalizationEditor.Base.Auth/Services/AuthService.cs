using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LocalizationEditor.Base.Auth.Infrastructure;
using LocalizationEditor.Base.Models;
using Microsoft.IdentityModel.Tokens;

namespace LocalizationEditor.Base.Auth.Services
{
  internal class AuthService : IAuthService
  {
    private readonly IAuthOptionsProvider _options;

    public AuthService(IAuthOptionsProvider options)
    {
      _options = options;
    }

    public string GenerateJwt(IdNameModel entity)
    {
      var key = _options.GetSecurityKey();
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Sub, entity.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, entity.Name)
      };

      var jwt = new JwtSecurityToken(
        _options.Issuer,
        _options.Audience,
        claims,
        expires: DateTime.Now.AddSeconds(_options.TokenLifeTime),
        signingCredentials: credentials);
      
      return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
  }
}