using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.services
{
    public class AppointmentService: IAppointmentService
    {

        private DBContext dbcontext;

        public AppointmentService(DBContext context)
        {
            dbcontext = context;
        }

        public List<AppointmentDao> getAppointments(int patientId) {
            List<AppointmentDao> appointments = new List<AppointmentDao>();
            //dbcontext.appointments.Select(x => AppointmentDao.AppointmentToAppointmentDao(x).PatientId==patientId ).ToList();
            foreach (Appointment appointment in dbcontext.appointments)
            {
                if (appointment.PatientId == patientId) 
                {
                    appointments.Add(AppointmentDao.AppointmentToAppointmentDao(appointment));
                }
            }

                return appointments;
        }

        //appointment is active if its in the db, after it is done, it gets deleted
        public AppointmentDao Add(AppointmentDao appointmentDao)
        {
            if (appointmentDao == null)
                return null;

            Appointment appointment = AppointmentDao.AppointmentDaoToAppointment(appointmentDao);
            User patient = dbcontext.user.FirstOrDefault(p => p.id == appointment.PatientId);
            appointment.Patient = patient;
            appointment.isCanceled = false;
            Doctor doctor = appointment.Doctor;
            

            if (appointment.Patient == null || appointment.Doctor == null)
                return null;

            DateTime appointmentDate = appointmentDao.DateTimeFrom;

            patient.appointments.Add(appointment);
            doctor.Appointments.Add(appointment);

            if (patient.ReferralId != 0) {
                patient.ReferralId = 0;
            }

            dbcontext.appointments.Add(appointment);
            dbcontext.SaveChanges();

            return appointmentDao;
        }

        public AppointmentDao Cancel(int id)
        {
            Appointment appointment = dbcontext.appointments.FirstOrDefault(a => a.Id == id);
            appointment.isCanceled=true;
            appointment.Patient.canceledAppointments++;
            appointment.Patient.appointments.Remove(appointment);
            appointment.Doctor.Appointments.Remove(appointment);
            dbcontext.SaveChanges();
            return AppointmentDao.AppointmentToAppointmentDao(appointment);
        }
    }
}
