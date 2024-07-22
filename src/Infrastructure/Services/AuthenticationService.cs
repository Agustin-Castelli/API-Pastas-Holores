using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IClientRepository clientRepository, IAdminRepository adminRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
            _options = options.Value;
        }

        public Client? ClientAuthenticate(CredentialsRequest credentialsRequest)
        {
            if (string.IsNullOrEmpty(credentialsRequest.Username) || string.IsNullOrEmpty(credentialsRequest.Password))
                return null;

            var client = _clientRepository.GetByUsername(credentialsRequest.Username);

            if (client == null) return null;

            if (client.Username == credentialsRequest.Username && client.Password == credentialsRequest.Password) return client;

            return null;
        }

        public Admin? AdminAuthenticate(CredentialsRequest credentialsRequest)
        {
            if (string.IsNullOrEmpty(credentialsRequest.Username) || string.IsNullOrEmpty(credentialsRequest.Password))
                return null;

            var admin = _adminRepository.GetByUsername(credentialsRequest.Username);

            if (admin == null) return null;

            if (admin.Username == credentialsRequest.Username && admin.Password == credentialsRequest.Password) return admin;

            return null;
        }

        public string Authentication(CredentialsRequest credentialsRequest)
        {

            if (credentialsRequest.Rol == Domain.Enums.RolEnum.admin)
            {
                var userLogged = AdminAuthenticate(credentialsRequest);

                if (userLogged == null)
                {
                    return null;
                }

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));  //Acá armo el Secret de la Signature.

                var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                // ---- Claims ----

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userLogged.Id.ToString()));
                claimsForToken.Add(new Claim("given_name", userLogged.Username));
                claimsForToken.Add(new Claim("role", credentialsRequest.Rol.ToString()));

                var jwtSecurityToken = new JwtSecurityToken(
                    _options.Issuer,
                    _options.Audience,
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    credentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return tokenToReturn;

            }

            if(credentialsRequest.Rol == Domain.Enums.RolEnum.cliente)
            {
                var userLogged = ClientAuthenticate(credentialsRequest);

                if (userLogged == null)
                {
                    return null;
                }

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

                var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                // ---- Claims ----

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userLogged.Id.ToString()));
                claimsForToken.Add(new Claim("username", userLogged.Username));
                claimsForToken.Add(new Claim("given_name", userLogged.FirstName));
                claimsForToken.Add(new Claim("family_name", userLogged.LastName));
                claimsForToken.Add(new Claim("role", credentialsRequest.Rol.ToString()));

                var jwtSecurityToken = new JwtSecurityToken(
                    _options.Issuer,
                    _options.Audience,
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    credentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return tokenToReturn;
            }

            return null;
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
