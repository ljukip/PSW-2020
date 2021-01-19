using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    public class Perscription
    {
        public int Id { get; set; }

        public string therapy { get; set; }

        public string doctorName { get; set; }

        public int patientId { get; set; }
    }
}
