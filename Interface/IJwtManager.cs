using System;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IJwtManager
    {
        string GenerateToken(User authUser);
    }
}
