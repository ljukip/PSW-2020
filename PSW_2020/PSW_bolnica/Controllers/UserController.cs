using Microsoft.AspNetCore.Mvc;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PSW_bolnica.services;
using PSW_bolnica.interfaces;

namespace PSW_bolnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller

    {
        private readonly DBContext dbcontext;
        private IUserService service;

        public UserController(DBContext context, IUserService userService)
        {
            dbcontext = context;
            service = userService;
        }
        //return all users
        [HttpGet("/getUsers")]
        public IActionResult Get()
        {
            List<User> result = new List<User>();
            dbcontext.user.ToList().ForEach(user => result.Add(user));

            return Ok(result);
        }
        //add new user
        [HttpPost]
        [Route("/registration")]
        public IActionResult Add(User user)
        {
            System.Console.WriteLine("user za upis je:" + user.ToString());
            if (service.Add(user) == null)
                return NotFound();

            return Ok(user);
        }
    }
}

