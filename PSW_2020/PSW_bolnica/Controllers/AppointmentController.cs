using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
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
        private IUserService userService;
        private IDoctorService doctorService;
        private readonly IConfiguration _configuration;

        public AppointmentController(DBContext context, IAppointmentService appointmentService, IDoctorService doctorServicee, IUserService userServicee, IConfiguration configuration)
        {
            dbcontext = context;
            service = appointmentService;
            userService = userServicee;
            doctorService = doctorServicee;
            _configuration = configuration;
        }

        //create appointment
        [HttpPost]
        [Route("/newAppointment/{username}/{from}/{to}/{doctorId}")]
        public IActionResult newAppointment(string username, DateTime from, DateTime to, string doctorId)
        {
            int id= Int32.Parse(doctorId);
            int patientId=userService.GetUserId(username);
            AppointmentDao appointmentDao = new AppointmentDao();
            Doctor doctor = doctorService.GetWithId(id);
            appointmentDao.DateTimeFrom = from;
            appointmentDao.DateTimeTo = to;
            appointmentDao.PatientId = patientId;
            appointmentDao.Doctor = doctor;
            appointmentDao.DoctorId = id;

            if (service.Add(appointmentDao) == null)
                return NotFound();

            return Ok();
        }

        [HttpPut("/cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            AppointmentDao appointmentDao = new AppointmentDao();
            appointmentDao = service.Cancel(id);


            return Ok(appointmentDao);
        }

        //get patientes appointments
        [HttpGet]
        [Route("/patientAppointments/{username}")]
        public IActionResult GetPatientsAppointments(string username)
        {
            //go through appointments and check patientId, if pid==id=>add to its list
            int patientId = userService.GetUserId(username);
            List<AppointmentDao> apointments=service.getAppointments(patientId);

            return Ok(apointments);
        }
    }
}
