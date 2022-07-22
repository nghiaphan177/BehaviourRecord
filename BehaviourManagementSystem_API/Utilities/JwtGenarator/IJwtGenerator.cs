using BehaviourManagementSystem_API.Models;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Utilities.JwtGenarator
{
    /// <summary>
    /// interface IJwtGenerator.
    /// Writer: DuyLH4
    /// </summary>
    public interface IJwtGenerator
    {
        Task<string> GenerateTokenLoginSuccessAsync(User user);
    }
}