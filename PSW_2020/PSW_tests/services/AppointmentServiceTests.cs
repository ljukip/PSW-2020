using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PSW_bolnica.dao;
using PSW_bolnica.model;

namespace PSW_bolnica.services.Tests
{
    [TestClass()]
    public class AppointmentServiceTests
    {
        private DbContextOptions<DBContext> _options;

        public AppointmentServiceTests()
        {
            _options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(databaseName: "DBC_appointments").Options;
        }

        private void SetupDatabase(DBContext context)
        {
            context.Database.EnsureDeleted();
            CreateDoctors(context);
            CreatePatients(context);
            CreateReferrals(context);
            CreateAppointments(context);
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

        private void CreateReferrals(DBContext context)
        {
            context.referral.Add(new Referral
            {
                Id = 1,
                IsDeleted = false,
                idOfPatient = 1,
                Speciality = "oncology"
            });

            context.referral.Add(new Referral
            {
                Id = 2,
                IsDeleted = false,
                idOfPatient = 2,
                SpecialistId = 2,
                Speciality = "ophtalmology"
            });

            context.SaveChanges();
        }
        private void CreateAppointments(DBContext context)
        {
            context.appointments.Add(new Appointment
            {
                isCanceled = false,
                Id=1,
                DoctorId=1,
                PatientId=1,
                DateTimeFrom=new DateTime(),
                DateTimeTo=new DateTime().AddDays(1),
            }) ;

            context.appointments.Add(new Appointment
            {
                isCanceled = false,
                Id = 2,
                DoctorId = 1,
                PatientId = 1,
                DateTimeFrom = new DateTime(),
                DateTimeTo = new DateTime().AddDays(1),
            });
            context.SaveChanges();
        }


        [TestMethod()]
        public void getAppointmentsTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                AppointmentService service = new AppointmentService(context);

                List<AppointmentDao> appointments = service.getAppointments(1);

                Assert.AreEqual(appointments.Count,2);
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                AppointmentService service = new AppointmentService(context);
                DoctorService doctorService = new DoctorService(context);
                UserService userService = new UserService(context);
                AppointmentDao appointmentDao = new AppointmentDao();

                Doctor doctor = doctorService.GetWithId(1);
                appointmentDao.DateTimeFrom = new DateTime();
                appointmentDao.DateTimeTo = new DateTime().AddDays(1);
                appointmentDao.PatientId = 1;
                appointmentDao.Doctor = doctor;
                appointmentDao.DoctorId = 1;
                appointmentDao.Patient = userService.GetWithId(1);

                AppointmentDao a = service.Add(appointmentDao);

                Assert.AreEqual(a.Patient.ReferralId, 0);
                Assert.IsNotNull(a);
            }
        }

        [TestMethod()]
        public void CancelTest()
        {
            using (var context = new DBContext(_options))
            {
                SetupDatabase(context);
                AppointmentService service = new AppointmentService(context);
                UserService userService = new UserService(context);

                AppointmentDao appointmentDao = service.Cancel(1);
                User patient = userService.GetWithId(1);

                Assert.AreEqual(appointmentDao.isCanceled,true);
                Assert.AreEqual(patient.appointments.Count,1);
            }
        }
    }
}