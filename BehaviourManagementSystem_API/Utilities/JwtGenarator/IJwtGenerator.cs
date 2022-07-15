using BehaviourManagementSystem_API.Models;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Utilities.JwtGenarator
{
    public interface IJwtGenerator
    {
        Task<string> GenerateTokenLoginSuccessAsync(User user);
    }
}