using Microsoft.Extensions.Configuration;
using PSW_bolnica.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.interfaces
{
    public interface IAppointmentService
    {
        AppointmentDao Add(AppointmentDao appointmentDao);

        public List<AppointmentDao> getAppointments(int patientId);

        public AppointmentDao Cancel(int id);
    }
}
