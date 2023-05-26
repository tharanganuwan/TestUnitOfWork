using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.API.DTOs;
using TestUnitOfWorkCRUD.Core.ApplicationServices;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGlobalUnitOfWorkService _global;
        public UserController(IUserService userService, IGlobalUnitOfWorkService global)
        {
            _userService = userService;
            _global = global;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO newUser)
        {
            try
            {
                const string description = "Create a user in Application";
                string response;
                if((newUser.FirstName==null || newUser.LastName==null || newUser.LastName==null) || newUser.Email==null || newUser.Password==null)
                {
                    response = "User Service name and email and password can not be empty";
                    User noUser = new User();
                    _global.CreateRequestResponseLogs(description, noUser, -1, response);
                    return BadRequest(response);
                }

                var user = new User
                {
                    FirstName = newUser.FirstName,
                    MiddleName = newUser.MiddleName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    Status = 1,
                    StartDate = DateTime.Now,
                    UserType = newUser.UserType,

                };

                var result = await _userService.CreateUser(user);
                switch (result)
                {
                    case 1:
                        //Request Response Log
                        response = "User create successfull";
                        _global.CreateRequestResponseLogs(description, user, result, response);
                        return Ok(result);
                    case 9:
                        response = "Checking user existence has thrown an exception...!";
                        _global.CreateRequestResponseLogs(description, user, result, response);
                        return BadRequest(response);
                    case 8:
                        response = "The user is already exists";
                        _global.CreateRequestResponseLogs(description, user, result, response);
                        return BadRequest(response);
                    case 7:
                        response = "Something wrong...! Unsuccessful Transaction...!";
                        _global.CreateRequestResponseLogs(description, user, result, response);
                        return BadRequest(response);
                    case 6:
                        response = "The email is already exists...!";
                        _global.CreateRequestResponseLogs(description, user, result, response);
                        return BadRequest(response);
                }
                response = "Something is wrong. User creation unsuccessful...!";
                _global.CreateRequestResponseLogs(description, user, result, response);

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }



            return Ok();
        }
/*        private int?  GetCurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims.ToList();
            var centerCodeClaim = userClaims.FirstOrDefault(o => o.Type == "CenterCode");

            if (centerCodeClaim != null && int.TryParse(centerCodeClaim.Value, out int centerCode))
                return centerCode;

            return null;
        }*/
    }
}
