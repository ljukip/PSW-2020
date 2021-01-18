using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    [Table("feedback")]
    public class Feedback
    {
        //[Key]
        public int id { get; set; }
        public string text { get; set; }
        public bool isPublished { get; set; }
        public int patientId { get; set; }
    }
}
