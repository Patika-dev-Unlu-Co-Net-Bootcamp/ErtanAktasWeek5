using hafta1WebApi.DBOperations;
using hafta1WebApi.Filters;
using hafta1WebApi.UserOperation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using static hafta1WebApi.UserOperation.CreateUser;
using static hafta1WebApi.UserOperation.LoginAuth;

namespace hafta1WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        readonly IConfiguration _configuration;


        public UserController(UserDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet]
        [CustomActionFilter]
        public ActionResult<List<User>> Get()
        {
            var users = _context.Users.OrderBy(x => x.Id).ToList();

            
            return Ok(users);

        }
        [HttpPost]

        public IActionResult AddUser([FromBody] CreateUserModel newUser)
        {
            CreateUser command = new CreateUser(_context);
            command.Model = newUser;
            command.Handle();

            

            
            return Ok();

        }
        [HttpPost("/Login")]

        public  IActionResult login([FromForm] LoginUserModel user )
        {
            LoginAuth command = new LoginAuth(_context, _configuration);
            command.Model = user;
            var result = command.Handle();

            return Ok(result);
        }
        



    }
}
