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
    public class PerscriptionController : Controller
    {
        private readonly DBContext dbcontext;
        private IUserService userService;
        private IDoctorService doctorService;
        private readonly IConfiguration _configuration;

        public PerscriptionController(DBContext context, IUserService userServicee, IDoctorService doctorServicee, IConfiguration configuration)
        {
            dbcontext = context;
            userService = userServicee;
            doctorService = doctorServicee;
            _configuration = configuration;
        }

        [HttpGet("/getPerscriptions")]
        public IActionResult Get()
        {
            List<Perscription> result = new List<Perscription>();
            dbcontext.perscriptions.ToList().ForEach(p => result.Add(p));

            return Ok(result);
        }

        [HttpGet("/getPerscription/{patientId}")]
        public IActionResult GetPatientsPerscriptions(string patientId)
        {
            int id = Int32.Parse(patientId);
            List<Perscription> allPerscriptions = new List<Perscription>();
            List<Perscription> patientsPerscriptions = new List<Perscription>();
            dbcontext.perscriptions.ToList().ForEach(p => allPerscriptions.Add(p));
            foreach (Perscription p in allPerscriptions) {
                if (p.patientId == id) {
                    patientsPerscriptions.Add(p);
                }
            }

            return Ok(patientsPerscriptions);
        }

        [HttpPost]
        [Route("/newPerscription/{doctorUsername}/{patientId}/{therapy}")]
        public IActionResult Perscribe(string doctorUsername, string patientId, string therapy)
        {
            int pId = Int32.Parse(patientId);
            Perscription perscription = new Perscription();
            perscription.patientId = pId;
            perscription.doctorName = doctorUsername;
            perscription.therapy = therapy;

            dbcontext.perscriptions.Add(perscription);
            dbcontext.SaveChanges();

            return Ok(perscription);
        }
    }
}