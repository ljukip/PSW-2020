using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }

        public bool? isBlocked { get; set; }

        public int canceledAppointments { get; set; }

        public virtual List<Appointment> appointments { get; set; }

        public virtual int ReferralId { get; set; }
    }
}
