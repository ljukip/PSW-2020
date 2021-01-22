using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.services;
using PSW_bolnica.model;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PSW_bolnica.dao;

namespace PSW_bolnica.services.Tests
{
    [TestClass()]
    public class DoctorServiceTests
    {
        private DbContextOptions<DBContext> _options;

        public DoctorServiceTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "DBC_Doctors").Options;
        }

        private void SetupDatabase(DBContext context)
        {
            context.Database.EnsureDeleted();
            CreateDoctors(context);
            CreatePatients(context);
            CreateReferrals(context);
        }

        private void CreateDoctors(DBContext context)
        {
            context.doctors.Add(new Doctor
            {
                Id = 1,
                Name = "Pera",
                Surname = "Peric",
                Specialist = true,
                IsDeleted = false,
                Speciality = "oncology",
                Appointments = new List<Appointment>(),
                Patients = new List<User>(),
                Referrals = new List<Referral>(),
            });

            context.doctors.Add(new Doctor
            {
                Id = 2,
                Name = "Zika",
                Surname = "Zikic",
                Specialist = true,
                IsDeleted = false,
                Speciality = "ophtalmology",
                Appointments = new List<Appointment>(),
                Patients = new List<User>(),
                Referrals = new List<Referral>(),
            });
            context.doctors.Add(new Doctor
            {
                Id = 3,
                Name = "Zika",
                Surname = "Zikic",
                Specialist = false,
                IsDeleted = false,
                Speciality = "",
                Appointments = new List<Appointment>(),
                Patients = new List<User>(),
                Referrals = new List<Referral>(),
            });

            context.SaveChanges();
        }

        private void CreatePatients(DBContext context)
        {
            context.user.Add(new User
            {
                id=1,
                name="mika",
                surname="mikic",
                username="mmikic",
                password= "b14361404c078ffd549c03db443c3fede2f3e534d73f78f77301ed97d4a436a9fd9db05ee8b325c0ad36438b43fec8510c204fc1c1edb21d0941c00e9e2c1ce2",
                role="PATIENT",
                gender="femail",
                address="knjeginje zorke 3",
                phoneNumber="0638794655",
                canceledAppointments=0,
                isBlocked=false,
                ReferralId=0,
                appointments= new List<Appointment>(),

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

        private void CreateReferrals(DBContext context)
        {
            context.referral.Add(new Referral
            {
                Id = 1,
                IsDeleted = false,
                idOfPatient = 1,
                Speciality="oncology"
            });

            context.referral.Add(new Referral
            {
                Id = 2,
                IsDeleted = false,
                idOfPatient = 2,
                SpecialistId = 2,
                Speciality="ophtalmology"
            });

            context.SaveChanges();
        }


        [TestMethod]
        public void GetAllTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                DoctorService service = new DoctorService(context);
                UserService userService = new UserService(context);
                User user = userService.GetWithId(1);
                List<DoctorDao> doctors = service.GetAll(user);

                Assert.AreNotEqual(doctors.Count, 0);
            }
        }

        [TestMethod()]
        public void GetSuggestionsTest()
        {
            using (var context = new DBContext(_options))
            {
                DateTime from = new DateTime();
                DateTime to = from.AddDays(1);
                string priority = "Doctor";
                string priority2 = "date";

                SetupDatabase(context);
                DoctorService service = new DoctorService(context);

                AppointmentDao appointmentDao=service.GetSuggestions(from, to, priority, "1");
                AppointmentDao appointmentDao2 = service.GetSuggestions(from, to, priority2, "1");
                Assert.IsNotNull(appointmentDao);
                Assert.IsNotNull(appointmentDao2);
            }
        }

        [TestMethod()]
        public void checkTest()
        {
            using (var context = new DBContext(_options))
            {
                DateTime from = new DateTime();
                DateTime to = from.AddDays(1);

                SetupDatabase(context);
                DoctorService service = new DoctorService(context);
                Doctor doctor = service.GetWithId(1);
                Appointment appointment = service.check(from, to, DoctorDao.DoctorToDoctorDao(doctor));
                Assert.IsNotNull(appointment);
            }
        }

            [TestMethod()]
        public void GetWithIdTest()
        {
            using (var context = new DBContext(_options))
            {

                SetupDatabase(context);
                DoctorService service = new DoctorService(context);
                Doctor doctor = service.GetWithId(1);
                Assert.IsNotNull(doctor);
            }
        }
    }
}