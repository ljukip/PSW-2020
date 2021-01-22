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
            //check if the user with the given username already exists
            Debug.WriteLine("usao u proveru");
            if (dbcontext.user.SingleOrDefault(u => u.username == newUser.username) != null)
                return null;

            newUser.password = Util.SHA512(newUser.password);

            dbcontext.user.Add(newUser);
            dbcontext.SaveChanges();

            UserDao userDao = UserDao.UserToUserDao(newUser);

            return userDao;
        }

        public int GetUserId(string username) {
            User user= dbcontext.user.FirstOrDefault(u => u.username.Equals(username));
            return user.id;
        }

        public User Block(int id)
        {
            User user = dbcontext.user.FirstOrDefault(u => u.id== id);
            user.isBlocked = true;
            dbcontext.SaveChanges();

            return user;
        }
        public User GetWithId(int id) {
            return dbcontext.user.FirstOrDefault(u => u.id == id);
        }
        public User GetWithUsername(string username)
        {
            return dbcontext.user.FirstOrDefault(u => u.username == username);
        }

        public void Update(User oldUser, User newUser)
        {
            oldUser.name = newUser.name;
            oldUser.surname = newUser.surname;
            oldUser.address = newUser.address;
            oldUser.phoneNumber = newUser.phoneNumber;
            if (newUser.password != null)
            {
                oldUser.password = newUser.password;
            }
            oldUser.gender = newUser.gender;

            dbcontext.SaveChanges();
        }
    }
}
