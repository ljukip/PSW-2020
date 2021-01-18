using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PSW_bolnica.dao;
using PSW_bolnica.interfaces;
using PSW_bolnica.model;
using PSW_bolnica.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_bolnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferralController : Controller
    {
        private readonly DBContext dbcontext;
        private IReferralService service;
        private IUserService userService;
        private readonly IConfiguration _configuration;

        public ReferralController(DBContext context,IUserService userServicee, IReferralService referralService, IConfiguration configuration)
        {
            dbcontext = context;
            service = referralService;
            userService = userServicee;
            _configuration = configuration;
        }

        //add new user
        [HttpPost]
        [Route("/referral/create/{speciality}/{patientId}")]
        public IActionResult Register(string speciality, string patientId)
        {
            int id = Int32.Parse(patientId);
            Referral referral = new Referral();
            referral.IsDeleted = false;
            referral.idOfPatient = id;
            User patient = userService.GetWithId(id);
            patient.ReferralId = referral.Id;
            referral.Patient = patient;
            referral.Speciality = speciality;
            referral.SpecialistId = 1;
            System.Console.WriteLine("user za upis je:" + referral.ToString());

            ReferralDao referralDao = service.Add(referral);
            if (referralDao == null)
            { return NotFound(); }
            else
            {
                return Ok(referralDao);
            }
        }
    }
}
