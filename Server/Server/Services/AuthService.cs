using DataLayer.Entities;
using DataLayer.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Server.DTOs;
using Server.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    /// <summary>
    /// Authentication actions service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _salt;

        public AuthService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
            _salt = _appSettings.Secret;
        }

        public async Task<bool> RegisterAsync(Patient patient)
        {
            // Check if the user exists with this email

            Patient candidate = await _unitOfWork.Patients.FindAsync(u => u.Email == patient.Email);

            if (candidate != null)
                return false;

            patient.Password = GetPasswordHash(patient.Password, _salt);
            patient.DateTime = DateTime.UtcNow;
            patient.Role = "user";
            await _unitOfWork.Patients.CreateAsync(patient);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<AuthDto> LoginAsync(string email, string password)
        {
            // Check if the user exists with this email and password

            Patient candidate = await _unitOfWork.Patients.FindAsync(u => u.Email == email && u.Password == GetPasswordHash(password, _salt));

            if (candidate == null)
                return null;

            // Generate JWT token            

            var secretKey = JwtHelper.GetSymmetricSecurityKey();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: JwtHelper.Issuer,
                audience: JwtHelper.Audience,
                claims: new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, candidate.Role)
                },
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new AuthDto { PatientId = candidate.Id, Token = token, Role =candidate.Role };
        }

        // Password encription
        private static string GetPasswordHash(string password, string salt)
        {
            string computedHash;

            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(salt)))
            {
                computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }

            return computedHash;
        }
    }
}
