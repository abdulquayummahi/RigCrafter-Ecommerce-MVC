using RigCrafter.BLL.Services;
using RigCrafter.DAL;
using RigCrafter.DAL.Models;
using System.Linq;

namespace RigCrafter.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly RigCrafterDbContext _context;

        public UserService(RigCrafterDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(User newUser)
        {
            bool emailExists = _context.Users.Any(u => u.Email == newUser.Email);
            if (emailExists)
            {
                return false;
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        public User? LoginUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}