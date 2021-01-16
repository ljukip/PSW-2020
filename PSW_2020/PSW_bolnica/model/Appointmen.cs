using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    public enum Status
    {
        passed,
        pending,
        cancelled
    }

    [Table("Appointment")]
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual User Patient { get; set; }

        public DateTime DateTimeFrom { get; set; }

        public DateTime DateTimeTo { get; set; }
    }
}
