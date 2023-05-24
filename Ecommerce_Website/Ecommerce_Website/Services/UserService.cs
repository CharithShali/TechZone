using Ecommerce_Website.Context;
using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Website.Services
{
    public class UserService : IUserService
    {
        private readonly TechZoneContext _context;
        public UserService(TechZoneContext techZoneContext)
        {
            _context = techZoneContext;
        }

        public async Task<User> Login(User userObj)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userObj.Email);
            return user;
        }

        public  async Task<User> Register(User userObj)
        {
         

           await _context.Users.AddAsync(userObj);
           await _context.SaveChangesAsync();
            return userObj;

        }
        public async Task<bool> CheckEmail(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
         public async Task<List<User>> GetAllUsers()
        {
            var result= await _context.Users.ToListAsync();
            return result;
        }
        public async Task<User> ViewUser(int id)
        {
            var result = await _context.Users.FindAsync(id);
            return result;
        }
        public async Task<User> ViewUserByemail(string email)
        {
            var user = _context.Users
                    .Where(b => b.Email == email)
                    .FirstOrDefault();
            return user;
        }

     
    }
}
