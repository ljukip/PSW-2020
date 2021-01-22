using PSW_bolnica.dao;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.services
{
    public class ReferralService : IReferralService
    {
        private DBContext dbcontext;

        public ReferralService(DBContext context)
        {
            dbcontext = context;
            Debug.WriteLine("context je :" + context.ToString());
        }

        public ReferralDao Add(Referral referral) {

            if (referral == null)
                return null;
            
            dbcontext.referral.Add(referral);
            //dbcontext.Entry(referral.Patient).Property(x => x.ReferralId).IsModified = true;
            dbcontext.SaveChanges();


            return ReferralDao.ReferralToRefrralDao(referral);
        }
    }
}
