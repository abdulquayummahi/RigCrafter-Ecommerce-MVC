using RigCrafter.DAL.Models;

namespace RigCrafter.BLL.Services
{
    public interface IUserService
    {
        bool RegisterUser(User newUser);
        User? LoginUser(string email, string password);
    }
}