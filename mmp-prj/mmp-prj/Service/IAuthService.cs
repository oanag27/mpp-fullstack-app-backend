using mmp_prj.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace mmp_prj.Service
{
    public interface IAuthService
    {
        string GenerateToken(UserModel user);
        bool VerifyPassword(UserModel user, string password);
        public UserModel Register(string email, string password);
        public UserModel Authenticate(string email, string password);
        bool UserExists(string email);
    }
}
