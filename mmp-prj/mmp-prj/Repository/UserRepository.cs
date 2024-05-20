using mmp_prj.Models;

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
            return _context.UserModels.FirstOrDefault(u => u.Email == email);
        }

        public UserModel AddUser(UserModel user)
        {
            _context.UserModels.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
