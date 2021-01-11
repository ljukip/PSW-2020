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
using PSW_bolnica.dao;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace PSW_bolnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller

    {
        private readonly DBContext dbcontext;
        private IUserService service;
        private readonly IConfiguration _configuration;

        public UserController(DBContext context, IUserService userService,IConfiguration configuration)
        {
            dbcontext = context;
            service = userService;
            _configuration = configuration;
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

            UserDao userForRegistration = service.Add(user);
            if (userForRegistration == null)
            { return NotFound(); }
            else {
                string token = GenerateJWT(userForRegistration);
                return Ok(token);
            } 
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(Authenticate user)
        {
            //search for user thats trying to logg in
            User userForLogin = dbcontext.user.FirstOrDefault(u => u.username == user.username && u.password == Util.SHA512( user.password));
            if (userForLogin != null)
            {
                UserDao userForLogginDao = UserDao.UserToUserDao(userForLogin);
                string token = GenerateJWT(userForLogginDao);

                return Ok(token);

            }
            else {
                return NotFound();
            }
               
            

        }



        //method for session verification

        [HttpGet]
        [Route("/methodx")]
        [Authorize]
        public IActionResult MethodX()
        {
           // string jsonUser = HttpContext.Session.GetString("SessionLoggedUser");
            //User user = JsonConvert.DeserializeObject<User>(jsonUser);



            return Ok("radi");
        }
        private string GenerateJWT(UserDao user)
        {
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:JWT:Secret_Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    JWTClaim("username", user.username),
                    JWTClaim("name", user.name),
                    JWTClaim("surname", user.surname),
                    JWTClaim("gender",user.gender),
                    JWTClaim("role", user.role),
                }),
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("AppSettings:JWT:Expire_Time_Hours")),
                Issuer = _configuration["AppSettings:JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static Claim JWTClaim(string Name, string Value)
        {
            return Value == null ? null : new Claim(Name, Value);
        }




    }
}

