
using Ecommerce_Website.Context;
using Ecommerce_Website.Helper;
using Ecommerce_Website.Models;
using Ecommerce_Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TechZoneContext _authContext;
        private readonly IUserService _userService;

        public UserController(TechZoneContext authContext, IUserService userService)
        {
            _authContext = authContext;
            _userService = userService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            
        if(userObj == null)
                return BadRequest(new
                {
                    Message = "Enter Correct Credentials!"
                });
        var user =await _userService.Login(userObj);
            
         if(user== null)
                return NotFound(new {Message = "User not Found"});



            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new
                {
                    Message = "Password is incorrect"
                });
            }

            user.Token = CreateJwt(user);
            return Ok(new
            {
                Token = user.Token,
            Message = "Login Success"
            }) ;
       

        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody]User userObj)
        {
            
            if(userObj==null)
                return BadRequest();

            if (await CheckEmailExistAsync(userObj.Email)) 
                return BadRequest(new
                {
                    Message = "Email Already Exist!"
                                }
                    );
            DateTime dateTime = DateTime.UtcNow.Date;

            if (userObj==null)
                return BadRequest();
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.CreatedAt = dateTime.ToString("yyyy/MM/dd"); 

             var user=await _userService.Register(userObj);
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "User Not Added!"
                }
                   );
            }
            else
            {
                return Ok(new
                {
                    message = "User Registed!"
                });
            }
        }


        private Task<bool> CheckEmailExistAsync(string email)
        {
            return _userService.CheckEmail(email);
        }


        [HttpGet]
        public async Task<IActionResult> ViewUsers()
        {
            var employee = await _userService.GetAllUsers();
            return Ok(employee);


        }

        [HttpGet("userbyid")]
        public async Task<IActionResult> ViewUser(int id)
        {
            var employer = await _userService.ViewUser(id);
            return Ok(employer);


        }
        private string CreateJwt(User user)
        {
            var jwtTokenHadler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email,user.Email)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHadler.CreateToken(tokenDescriptor);
            return jwtTokenHadler.WriteToken(token);
        }
        [HttpGet("user")]
        public async Task<IActionResult> ViewUser(string email)
        {
            var user = _userService.ViewUserByemail(email);
            return Ok(user.Result);

        }

    }
}
