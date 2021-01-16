using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PSW_bolnica.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PSW_bolnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly DBContext dbcontext;
        private IAppointmentService service;
        private readonly IConfiguration _configuration;

        public AppointmentController(DBContext context, IAppointmentService appointmentService, IConfiguration configuration)
        {
            dbcontext = context;
            service = appointmentService;
            _configuration = configuration;
        }

        //create appointment
        public IActionResult Index()
        {
            return View();
        }

        //get patientes appointments
        public IActionResult get()
        {
            return View();
        }

        //cancel appointment
        public IActionResult cnc()
        {
            return View();
        }
    }
}
