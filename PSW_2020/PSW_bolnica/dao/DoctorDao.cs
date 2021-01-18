using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class DoctorDao
    {
        public int Id { get; set; }

        public string Speciality { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool Specialist { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public List<Referral> Referrals { get; set; }

        public List<Appointment> Appointments { get; set; }

        public List<User> Patients { get; set; }


        public static DoctorDao DoctorToDoctorDao(Doctor doctor)
        {
            DoctorDao doctorDao = new DoctorDao
            {
                Id=doctor.Id,
                Name=doctor.Name,
                Surname=doctor.Surname,
                Specialist=doctor.Specialist,
                IsDeleted=doctor.IsDeleted,
                Speciality=doctor.Speciality
            };
            return doctorDao;
        }
        public static Doctor DoctorDaoToDoctor(DoctorDao doctorDao) {
            Doctor doctor = new Doctor 
            {
                Id = doctorDao.Id,
                Name = doctorDao.Name,
                Surname = doctorDao.Surname,
                Specialist = doctorDao.Specialist,
                IsDeleted = doctorDao.IsDeleted,
                Speciality=doctorDao.Speciality
            };
            return doctor;
        }

    }
}
