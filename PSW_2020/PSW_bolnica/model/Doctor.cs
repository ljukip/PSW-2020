using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    public enum Specialities { 
        oftalmologist,
        gastroenterologist,
        neurologist,
        epidemiologist,
        hematologist,
        radiologist,
        cardiologist,
        surgeon
    }

    [Table("Doctor")]
    public class Doctor
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Speciality { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool Specialist { get; set; }

        public bool IsDeleted { get; set; }
        public virtual List<Referral> Referrals { get; set; }

        public virtual List<Appointment> Appointments { get; set; }

        public virtual List<User> Patients { get; set; }


    }
}
