using mmp_prj.Models;

namespace mmp_prj.Service
{
    public interface IUserService
    {
        public UserModel GetUserByEmail(string email);
        public UserModel AddUser(UserModel user);
        public UserModel DeleteUser(string userEmail);
        public UserModel UpdateUser(string email,UserModel user);
        public IEnumerable<UserModel> GetUsersByRole(string role);
    }
}
