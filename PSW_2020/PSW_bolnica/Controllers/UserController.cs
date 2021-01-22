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

        [HttpPut("/blockUser/{id}")]
        public IActionResult Block(int id)
        {
            User userForBlock = new User();
            userForBlock = service.Block(id);


            return Ok(userForBlock);
        }

        [HttpGet("/checkBlocked/{username}")]
        public IActionResult Check(string username)
        {
            string blocked="notBlocked";
            User user = dbcontext.user.FirstOrDefault(u => u.username == username);
            if (user.isBlocked == true)
            {
                blocked = "blocked";
            }
            return Ok(blocked);
        }

        //return all users
        [HttpGet("/getUsers")]
        public IActionResult Get()
        {
            List<User> allUsers = new List<User>();
            List<User> result = new List<User>();
            dbcontext.user.ToList().ForEach(user => allUsers.Add(user));
            foreach (User u in allUsers) {
                if (u.role=="PATIENT") {
                    result.Add(u);
                }
            }

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
            string password512= Util.SHA512(user.password);
            //search for user thats trying to logg in
            User userForLogin = dbcontext.user.FirstOrDefault(u => u.username == user.username && u.password == password512);
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

     [HttpPut]
     [Route("/updateUser")]

        public IActionResult Update(User newUser)
        {
            User user = service.GetWithUsername(newUser.username);
            service.Update(user, newUser);

            return Ok(user);
        }


//method for session verification
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
                    JWTClaim("address", user.address),
                    JWTClaim("phoneNumber", user.phoneNumber)
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

