using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly DBContext dbcontext;
        private IDoctorService service;
        private readonly IConfiguration _configuration;

        public DoctorController(DBContext context, IDoctorService doctorService, IConfiguration configuration)
        {
            dbcontext = context;
            service = doctorService;
            _configuration = configuration;
        }
        //return all doctors
        [HttpGet("/allDoctors")]
        public IActionResult Get()
        {
            var result=service.GetAll();
            Debug.Write("in allDoctors:" + result);

            return Ok(result);
        }

        [HttpGet]
        [Route("/getSuggestions/{DateTimeFrom}/{DateTimeTo}/{priority}/{doctorId}")]
        public IActionResult Get(DateTime DateTimeFrom, DateTime DateTimeTo, string priority, string doctorId)
        { 
            Debug.Write("in suggestions:" + doctorId +","+priority+","+DateTimeFrom);
            AppointmentDao appointment = service.GetSuggestions(DateTimeFrom, DateTimeTo, priority, doctorId);
            //should return an appointment
            DoctorDao doctor = DoctorDao.DoctorToDoctorDao(appointment.Doctor);
            doctor.DateTimeFrom = appointment.DateTimeFrom;
            Debug.Write("in suggestions:" + doctor);

            return Ok(doctor);
        }

        [HttpGet]
        [Route("/getSpecialist/{patientId}")]
        public IActionResult GetSpecialist(int patientId)
        {
            if (service.GetSpecialist(patientId) == null)
                return NotFound();

            return Ok(service.GetSpecialist(patientId));
        }
    }
}
