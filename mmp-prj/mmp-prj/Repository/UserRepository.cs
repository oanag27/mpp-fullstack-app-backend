using Microsoft.EntityFrameworkCore;
using mmp_prj.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace mmp_prj.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly MppContext _context;

        public UserRepository(MppContext context)
        {
            _context = context;
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.UserModels.SingleOrDefault(u => u.Email == email);
        }

        public UserModel AddUser(UserModel user)
        {
            _context.UserModels.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel DeleteUser(string userEmail)
        {
            var user = _context.UserModels.SingleOrDefault(u => u.Email == userEmail);
            if (user != null)
            {
                _context.UserModels.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }

        public UserModel UpdateUser(string email, UserModel user)
        {
            var existingUser = _context.UserModels.SingleOrDefault(t => t.Email== email);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = user.Password; // Assuming password is already hashed
                existingUser.Role = user.Role;
                _context.SaveChanges();
            }
            return existingUser;
        }

        public IEnumerable<UserModel> GetUsersByRole(string role)
        {
            return _context.UserModels.Where(u => u.Role == role).ToList();
        }
    }
}
