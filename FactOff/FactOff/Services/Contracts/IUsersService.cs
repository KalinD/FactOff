﻿using System;

namespace FactOff.Services.Contracts
{
    public interface IUsersService
    {
        Guid CreateUser(string email, string name, string password);
    }
}
