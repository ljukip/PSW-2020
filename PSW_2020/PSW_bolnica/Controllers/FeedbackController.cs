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
    public class FeedbackController : Controller
    {
        private readonly DBContext dbcontext;
        private IFeedbackService service;
        private IUserService userService;
        private readonly IConfiguration _configuration;

        public FeedbackController(DBContext context, IFeedbackService feedbackService, IUserService uService, IConfiguration configuration)
        {
            dbcontext = context;
            service = feedbackService;
            userService = uService;
            _configuration = configuration;
        }

        [HttpGet("/getFeedbacks")]
        public IActionResult Get()
        {
            List<Feedback> result = new List<Feedback>();
            dbcontext.feedbacks.ToList().ForEach(f => result.Add(f));

            return Ok(result);
        }

        [HttpPut("/publish/{id}")]
        public IActionResult Publish(int id)
        {
            Feedback feedback = new Feedback();
            feedback = service.Publish(id);


            return Ok(feedback);
        }

        [HttpPut("/unpublish/{id}")]
        public IActionResult Unpublish(int id)
        {
            Feedback feedback = new Feedback();
            feedback = service.Unpublish(id);


            return Ok(feedback);
        }

        [HttpPost]
        [Route("/newFeedback/{username}/{text}")]
        public IActionResult newAppointment(string username,string text)
        {
            int patientId = userService.GetUserId(username);
            FeedbackDao feedbackDao = new FeedbackDao();
            feedbackDao.isPublished = false;
            feedbackDao.patientId = patientId;
            feedbackDao.text = text;

            if (service.Add(feedbackDao) == null)
                return NotFound();

            return Ok();
        }
    }
}
