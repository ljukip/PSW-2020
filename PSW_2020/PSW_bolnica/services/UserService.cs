using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.services
{
    public class UserService: IUserService
    {
        private DBContext dbcontext;

        public UserService(DBContext context)
        {
            dbcontext = context;
            Debug.WriteLine("context je :" + context.ToString());
        }
        public UserDao Add(User newUser)
        {
            if (newUser == null || newUser.password == null)
                return null;
            //check if the user with the given name already exists
            Debug.WriteLine("usao u proveru");
            if (dbcontext.user.SingleOrDefault(u => u.username == newUser.username) != null)
                return null;

            //TODO: add doctor to new user

            //saving in DB
            //dbcontext.Users.ToList().ForEach(user => Debug.WriteLine("user je :" + user.name));
           
            dbcontext.user.Add(newUser);
            dbcontext.SaveChanges();

            UserDao userDao = UserDao.UserToUserDao(newUser);

            return userDao;
        }
    }
}
