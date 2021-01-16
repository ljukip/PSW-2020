using PSW_bolnica.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.interfaces
{
    public interface IDoctorService
    {
        List<DoctorDao> GetAll();
        DoctorDao GetSpecialist(int patientId);
        AppointmentDao GetSuggestions(DateTime dateTimeFrom, DateTime dateTimeTo, string priority, string doctorId);
    }
}
