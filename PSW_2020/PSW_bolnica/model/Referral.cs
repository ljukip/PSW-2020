using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    //[Table("Referral")]
    public class Referral
    {
        public int Id { get; set; }

        public int SpecialistId { get; set; }

        //virtual keyword allows it to be overridden in a derived class
        public virtual string Speciality { get; set; } //specialty which the patient is refferd to

        public virtual Doctor Specialist { get; set; }

        public int idOfPatient { get; set; }

        public virtual User Patient { get; set; }

        public bool IsDeleted { get; set; }
    }
}
