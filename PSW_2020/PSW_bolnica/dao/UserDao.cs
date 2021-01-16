using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class UserDao
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string gender { get; set; }
        public string jwt { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }

        public  List<Appointment> appointments { get; set; }
        public  Referral Referral { get; set; }

        public static User UserDaoToUser(UserDao userDao)
        {
            User user = new User
            {
                id = userDao.id,
                username = userDao.username,
                surname= userDao.surname,
                name=userDao.name,
                password=userDao.password,
                role=userDao.role,
                gender=userDao.gender,
                address=userDao.address,
                phoneNumber=userDao.phoneNumber
                
            };

            return user;
        }

        public static UserDao UserToUserDao(User user)
        {
            UserDao userDao = new UserDao
            {
                id = user.id,
                username = user.username,
                surname = user.surname,
                name = user.name,
                password = user.password,
                role = user.role,
                gender = user.gender,
                address = user.address,
                phoneNumber = user.phoneNumber
            };

            return userDao;
        }
    }
}
