using PSW_bolnica.dao;
using PSW_bolnica.model;

namespace PSW_bolnica.services
{
    public interface IReferralService
    {
        public ReferralDao Add(Referral referral);
    }
}