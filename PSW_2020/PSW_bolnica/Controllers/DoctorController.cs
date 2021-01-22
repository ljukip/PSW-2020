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
        private IUserService userService;
        private readonly IConfiguration _configuration;

        public DoctorController(DBContext context, IUserService userServicee, IDoctorService doctorService, IConfiguration configuration)
        {
            dbcontext = context;
            service = doctorService;
            userService = userServicee;
            _configuration = configuration;
        }
        //return all doctors
        [HttpGet("/allDoctors/{username}")]
        public IActionResult Get(string username)
        {
            User patient = userService.GetWithUsername(username);
            var result=service.GetAll(patient);
            Debug.Write("in allDoctors:" + result);

            return Ok(result);
        }

        [HttpGet]
        [Route("/getSuggestions/{DateTimeFrom}/{DateTimeTo}/{priority}/{doctorId}")]
        public IActionResult GetS(DateTime DateTimeFrom, DateTime DateTimeTo, string priority, string doctorId)
        { 
            Debug.Write("in suggestions:" + doctorId +","+priority+","+DateTimeFrom);
            AppointmentDao appointment = service.GetSuggestions(DateTimeFrom, DateTimeTo, priority, doctorId);
            //should return an appointment
            DoctorDao doctor = DoctorDao.DoctorToDoctorDao(appointment.Doctor);
            doctor.DateTimeFrom = appointment.DateTimeFrom;
            Debug.Write("in suggestions:" + doctor);

            return Ok(doctor);
        }
    }
}
