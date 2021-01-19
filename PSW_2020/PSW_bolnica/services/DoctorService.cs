using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.services
{
    public class DoctorService: IDoctorService
    {
        private DBContext dbcontext;

        public DoctorService(DBContext context)
        {
            dbcontext = context;
        }

        public List<DoctorDao> GetAll(User patient)
        {
            List<DoctorDao> allDoctors = dbcontext.doctors.Select(x => DoctorDao.DoctorToDoctorDao(x)).ToList();
            List<DoctorDao> doctors = new List<DoctorDao>();
            Referral referral = dbcontext.referral.FirstOrDefault(x => x.Id==patient.ReferralId);
            //proveri da li ima referral, ako ima vrati one specijaliste koji odg uputu, ako ne vrati lekare bez specijalnosti
            if (referral != null) {
                foreach (DoctorDao doctor in allDoctors) {
                    if (doctor.Speciality == referral.Speciality) {
                        doctors.Add(doctor);
                    }
                }
                return doctors;
            }
            else
            {
                foreach (DoctorDao doctor in allDoctors)
                {
                    if (doctor.Specialist == false)
                    {
                        doctors.Add(doctor);
                    }
                }
                return doctors;
            }

        }

        public AppointmentDao GetSuggestions(DateTime DateTimeFrom, DateTime DateTimeTo, string priority, string Id )
        {
            Appointment appointment = null;
            int id = Int32.Parse(Id);

            List<DoctorDao> freeDoctors = new List<DoctorDao>();
            List<DoctorDao> allDoctors = dbcontext.doctors.Select(x => DoctorDao.DoctorToDoctorDao(x)).ToList();
            Doctor doctor = GetWithId(id); //doctor from the request

            appointment = check(DateTimeFrom, DateTimeTo, DoctorDao.DoctorToDoctorDao(doctor));
            
            if (appointment == null){
                if (priority.Equals("Doctor")) {
                    DateTime newDateFrom = DateTimeFrom.AddDays(-7);
                    DateTime newdateTo = DateTimeTo.AddDays(7);
                    appointment = check(newDateFrom, newdateTo, DoctorDao.DoctorToDoctorDao(doctor));
                }
                if (priority.Equals("dates")) {
                    for (int i = 0; i < allDoctors.Count; i++) {
                        Doctor newDoctor = GetWithId(i);
                        appointment = check(DateTimeFrom, DateTimeTo, DoctorDao.DoctorToDoctorDao(newDoctor));
                        if (appointment != null) {
                            return AppointmentDao.AppointmentToAppointmentDao(appointment);
                        }
                    }
                }
            }

            return AppointmentDao.AppointmentToAppointmentDao(appointment);
        }

        public Appointment check(DateTime DateTimeFrom, DateTime DateTimeTo, DoctorDao doctor) {
            Appointment appointment = null;

            List<AppointmentDao> allAppointments = new List<AppointmentDao>();
            doctor.Appointments = new List<Appointment>();

            dbcontext.appointments.ToList().ForEach(x => allAppointments.Add(AppointmentDao.AppointmentToAppointmentDao(x)));

            foreach (AppointmentDao a in allAppointments) {
                
                if (a.DoctorId == doctor.Id) {
                    if (!a.isCanceled) {
                            doctor.Appointments.Add(AppointmentDao.AppointmentDaoToAppointment(a));
                    
                    }
                }
            }
            if (doctor.Appointments == null)
            {
                appointment = new Appointment();
                appointment.DateTimeFrom = DateTimeFrom;
                appointment.DateTimeTo = DateTimeTo;
                appointment.Doctor = DoctorDao.DoctorDaoToDoctor(doctor);
            }
            else
            {
                //for each date in the interval [from-to], at a given time, check if theres already an appointment, if not select one
                for (DateTime date = DateTimeFrom; date < DateTimeTo; date = date.AddDays(0.02084))
                {
                    Appointment appointmentExisting = doctor.Appointments.FirstOrDefault(x => x.DateTimeFrom == date);
                    if (appointmentExisting == null)
                    {
                        appointment = new Appointment();
                        appointment.DateTimeFrom = date;
                        appointment.DateTimeTo = DateTimeTo;
                        appointment.Doctor = DoctorDao.DoctorDaoToDoctor(doctor);
                        break;
                    }
                }
            }
            return appointment;
        }

        public Doctor GetWithId(int id)
        {
            Doctor doctor = dbcontext.doctors.FirstOrDefault(doctor => doctor.Id == id);

            if (doctor == null)
                return null;

            return doctor;
        }

        /* public DoctorDao GetSpecialist(int patientId)
         {
             User patient = dbcontext.user.FirstOrDefault(patient => patient.id == patientId);

             if (patient == null || patient.ReferralId == null)
                 return null;

             Doctor doctor = dbcontext.doctors.FirstOrDefault(doc => doc.Speciality == patient.Referral.Speciality);

             if (doctor == null)
                 return null;

             return DoctorDao.DoctorToDoctorDao(doctor);
         }*/
    }
}
