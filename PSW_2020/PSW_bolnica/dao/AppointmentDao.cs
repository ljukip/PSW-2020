using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class AppointmentDao
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public  Doctor Doctor { get; set; }

        public  User Patient { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }

        public Status Status { get; set; }

        public static Appointment AppointmentDaoToAppointment(AppointmentDao appointmentDao)
        {
            Appointment appointment = new Appointment
            {
                Id = appointmentDao.Id,
                DoctorId = appointmentDao.DoctorId,
                PatientId = appointmentDao.PatientId,
                DateTimeFrom = appointmentDao.DateTimeFrom,
                DateTimeTo = appointmentDao.DateTimeTo,
                Doctor = appointmentDao.Doctor
            };

            return appointment;
        }

        public static AppointmentDao AppointmentToAppointmentDao(Appointment appointment)
        {
            AppointmentDao appointmentDao = new AppointmentDao
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                DateTimeFrom = appointment.DateTimeFrom,
                DateTimeTo = appointment.DateTimeTo,
                Doctor= appointment.Doctor
            };

            return appointmentDao;
        }
    }
}

