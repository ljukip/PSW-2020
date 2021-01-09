using Microsoft.AspNetCore.Mvc;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PSW_bolnica.services;
using PSW_bolnica.interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;

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
        public IActionResult Register(User user)
        {
            System.Console.WriteLine("user za upis je:" + user.ToString());
            if (service.Add(user) == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(User user)
        {
            //search for user thats trying to logg in
            User userForLogin = dbcontext.user.FirstOrDefault(u => u.username == user.username);
            if (userForLogin != null)
            {
                if (userForLogin.password == user.password)
                {
                    //creating jwtSession
                    HttpContext.Session.SetString("SessionLoggedUser", JsonConvert.SerializeObject(userForLogin, Formatting.Indented));
                    // HttpContext.Session.SetString("SessionPass", Random);
                    string sessionId = HttpContext.Session.Id;

                    Debug.WriteLine("########################");
                    Debug.WriteLine(sessionId);

                    //Create an authentication cookie
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, userForLogin.username),
                    new Claim(ClaimTypes.Role, userForLogin.role)
                });
                    var principal = new ClaimsPrincipal(identity);

                    //SignInAsync creates an encrypted cookie and adds it to the current response
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return Ok(userForLogin);
                }
                else
                {
                    userForLogin = null;
                    return Ok(userForLogin);
                }

            }

            return NotFound();
        }
    }
}

