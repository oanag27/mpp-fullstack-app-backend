using mmp_prj.Models;
using mmp_prj.Repository;

namespace mmp_prj.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }
        public UserModel AddUser(UserModel user)
        {
            return _repository.AddUser(user);
        }

        public UserModel DeleteUser(string userEmail)
        {
            return _repository.DeleteUser(userEmail);
        }

        public UserModel GetUserByEmail(string email)
        {
            return _repository.GetUserByEmail(email);
        }

        public IEnumerable<UserModel> GetUsersByRole(string role)
        {
            return _repository.GetUsersByRole(role);
        }

        public UserModel UpdateUser(string email,UserModel user)
        {
            return _repository.UpdateUser(email,user);
        }
    }
}
