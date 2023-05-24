using Ecommerce_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Website.Services
{
    public interface IUserService
    {
        public Task<User> Login(User user);
        public Task<User> Register(User user);
        public Task<bool> CheckEmail(string email);
        public Task<List<User>> GetAllUsers();
        public Task<User> ViewUser(int id);
        public Task<User> ViewUserByemail(string email);
    }
}
