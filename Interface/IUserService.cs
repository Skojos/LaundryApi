using System;
using System.IdentityModel.Tokens.Jwt;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IUserService
    {
        User Create(RegisterDto dto);
        User GetByEmail(string email);
        User GetById(int id);
        User Authenticate(LoginDto dto);




    }
}
