using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class AdminDao
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public List<User> blockedUsers { get; set; }

        public static Admin AdminDaoToAdmin(AdminDao adminDao) {
            Admin admin = new Admin
            {
                id=adminDao.id,
                name=adminDao.name,
                surname=adminDao.surname,
                username=adminDao.username,
                password= adminDao.password,
                blockedUsers=adminDao.blockedUsers
            };
            return admin;
        }

        public static Admin AdminToAdminDao(Admin admin)
        {
            AdminDao adminDao = new AdminDao
            {
                id = admin.id,
                name = admin.name,
                surname = admin.surname,
                username = admin.username,
                password = admin.password,
                blockedUsers = admin.blockedUsers
            };
            return admin;
        }
    }
}
