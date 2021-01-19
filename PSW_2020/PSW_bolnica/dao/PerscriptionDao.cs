using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class PerscriptionDao
    {
        public int Id { get; set; }

        public string therapy { get; set; }

        public string doctorName { get; set; }

        public int patientId { get; set; }

        public static Perscription perscriptionDaotoPerscription(PerscriptionDao perscriptionDao)
        {
            Perscription perscription = new Perscription {
                Id = perscriptionDao.Id,
                therapy = perscriptionDao.therapy,
                doctorName = perscriptionDao.doctorName,
                patientId=perscriptionDao.patientId
            };

            return perscription;
        }

        public static PerscriptionDao perscriptiontoPerscriptionDao(Perscription perscription)
        {
            PerscriptionDao perscriptionDao = new PerscriptionDao
            {
                Id = perscription.Id,
                therapy = perscription.therapy,
                doctorName = perscription.doctorName,
                patientId = perscription.patientId
            };

            return perscriptionDao;
        }

    }

}
