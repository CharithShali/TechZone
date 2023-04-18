using Ecommerce_Website.Context;
using Ecommerce_Website.Helper;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public UserController(TechZoneContext authContext)
        {
            _authContext = authContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            
        if(userObj == null)
                return BadRequest();

            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Email == userObj.Email);
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



            Console.WriteLine(userObj.Password);
            if (userObj==null)
                return BadRequest();
            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new{
                message="User Registed!"
            });
        }


        private Task<bool> CheckEmailExistAsync(string email) =>
            _authContext.Users.AnyAsync(x => x.Email == email);


        [HttpGet]
        public async Task<IActionResult> ViewUsers()
        {
            var employee = await _authContext.Users.ToListAsync();
            return Ok(employee);

  
        }
        private string CreateJwt(User user)
        {
            var jwtTokenHadler = new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName} - {user.UserId}"),
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
     
    }
}
