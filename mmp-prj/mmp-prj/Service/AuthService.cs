﻿using Microsoft.IdentityModel.Tokens;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mmp_prj.Models;
using mmp_prj.Repository;

namespace mmp_prj.Service
{
    public class AuthService:IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool VerifyPassword(UserModel user, string password)
        {
            return user.Password == password;
        }
        public UserModel Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            // Check if user exists and password is correct
            if (user == null || !VerifyPassword(user, password))
                return null;

            return user;
        }

        public UserModel Register(string email, string password)
        {
            // Check if the user already exists
            if (_userRepository.GetUserByEmail(email) != null)
                return null;

            // Create a new user
            var newUser = new UserModel
            {
                Email = email,
                Password = password
            };

            // Save the user to the database
            var savedUser = _userRepository.AddUser(newUser);

            return savedUser;
        }

        public bool UserExists(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return user != null;
        }
    }
}
