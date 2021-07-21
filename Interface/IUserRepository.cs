﻿using System;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetById(int id);
        User AuthenticateUser(LoginDto dto);
    }
}
