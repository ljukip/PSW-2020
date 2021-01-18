using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.dao
{
    public class ReferralDao
    {
        public int Id { get; set; }

        public int SpecialistId { get; set; }

        public Doctor Specialist { get; set; }

        public string Speciality { get; set; }

        public int PatientId { get; set; }

        public User Patient { get; set; }

        public bool IsDeleted { get; set; }

        public static ReferralDao ReferralToRefrralDao(Referral referral) {
            ReferralDao referralDao = new ReferralDao
            {
                Id = referral.Id,
                SpecialistId = referral.SpecialistId,
                PatientId=referral.idOfPatient,
                IsDeleted=referral.IsDeleted
            };
            return referralDao;
        }
        public static Referral ReferralDaoToRefrral(ReferralDao referralDao)
        {
            Referral referral = new Referral
            {
                Id = referralDao.Id,
                SpecialistId = referralDao.SpecialistId,
                idOfPatient = referralDao.PatientId,
                IsDeleted = referralDao.IsDeleted
            };
            return referral;
        }
    }
}
