using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LaundryWebApi.Dtos;
using LaundryWebApi.Helpers;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LaundryWebApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       private readonly IJwtManager _jwtManager;



        public UserService(IUserRepository userRepository, IJwtManager jwtManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
           
        }

        public User Create(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            return _userRepository.Create(user);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User GetById(int id)
        {
          return _userRepository.GetById(id);

        }

        public User Authenticate(LoginDto dto)
        {

            var authUser = _userRepository.AuthenticateUser(dto);
            if (authUser == null)
                return null;

            // authentication successful so generate jwt token
            authUser.Token = _jwtManager.GenerateToken(authUser);
            
            // remove password before returning
            authUser.Password = null;

            return authUser;

        }

    }
}
