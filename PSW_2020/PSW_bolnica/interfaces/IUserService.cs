using PSW_bolnica.dao;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.interfaces
{
    public interface IUserService
    {
        UserDao Add(User user);
    }
}
