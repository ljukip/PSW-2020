using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.model
{
    public class Admin : User
    {

        public List<User>? blockedUsers { get; set; }
    }
}
