using PSW_bolnica.services;
using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PSW_bolnica.dao;
using PSW_bolnica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.model;

namespace PSW_bolnica.services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private DbContextOptions<DBContext> _options;

        public UserServiceTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "DBC_users").Options;
        }

        private void CreatePatients(DBContext context)
        {
            context.user.Add(new User
            {
                id = 1,
                name = "mika",
                surname = "mikic",
                username = "mmikic",
                password = "b14361404c078ffd549c03db443c3fede2f3e534d73f78f77301ed97d4a436a9fd9db05ee8b325c0ad36438b43fec8510c204fc1c1edb21d0941c00e9e2c1ce2",
                role = "PATIENT",
                gender = "femail",
                address = "knjeginje zorke 3",
                phoneNumber = "0638794655",
                canceledAppointments = 0,
                isBlocked = false,
                ReferralId = 0,
                appointments = new List<Appointment>(),

            });

            context.user.Add(new User
            {
                id = 2,
                name = "ruza",
                surname = "ruzic",
                username = "rruzic",
                password = "b14361404c078ffd549c03db443c3fede2f3e534d73f78f77301ed97d4a436a9fd9db05ee8b325c0ad36438b43fec8510c204fc1c1edb21d0941c00e9e2c1ce2",
                role = "PATIENT",
                gender = "femail",
                address = "knjeginje zorke 16",
                phoneNumber = "0638794655",
                canceledAppointments = 0,
                isBlocked = false,
                ReferralId = 0,
                appointments = new List<Appointment>(),
            });

            context.SaveChanges();
        }
        private void SetupDatabase(DBContext context)
        {
            context.Database.EnsureDeleted();
            CreatePatients(context);
        }

        [TestMethod()]
        public void AddTest()
        {
            using (var context = new DBContext(_options))
            {

                string name = "lola";
                string surname = "lolic";
                string gender = "femail";
                string username = "lloic";
                string password = "123";
                string role = "PATIENT";

                SetupDatabase(context);
                UserService service = new UserService(context);
                User user = new User();
                user.name = name;
                user.surname = surname;
                user.gender = gender;
                user.role = role;
                user.username = username;
                user.password = password;
                user.id = 3;
                UserDao userDao = service.Add(user);

                Assert.AreEqual(user.username,username);
                Assert.AreEqual(user.password, Util.SHA512(password));
            }
        }

        [TestMethod()]
        public void GetUserIdTest()
        {
            using (var context = new DBContext(_options))
            {

                SetupDatabase(context);
                UserService service = new UserService(context);
                int id = service.GetUserId("mmikic");
                Assert.AreEqual(id, 1);
            }
        }

        [TestMethod()]
        public void BlockTest()
        {
            using (var context = new DBContext(_options))
            {

                SetupDatabase(context);
                UserService service = new UserService(context);
                User user = service.Block(2);
                Assert.AreEqual(user.isBlocked, true);
            }
        }

        [TestMethod()]
        public void GetWithIdTest()
        {
            using (var context = new DBContext(_options))
            {

                SetupDatabase(context);
                UserService service = new UserService(context);
                User user = service.GetWithId(1);
                Assert.AreEqual(user.id,1);
            }
        }

        [TestMethod()]
        public void GetWithUsernameTest()
        {
            using (var context = new DBContext(_options))
            {

                SetupDatabase(context);
                UserService service = new UserService(context);
                User user = service.GetWithUsername("mmikic");
                Assert.AreEqual(user.username, "mmikic");
            }
        }
    }

}
