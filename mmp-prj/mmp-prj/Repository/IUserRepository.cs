using mmp_prj.Models;

namespace mmp_prj.Repository
{
    public interface IUserRepository
    {
        public UserModel GetUserByEmail(string email);
        public UserModel AddUser(UserModel user);
    }
}
