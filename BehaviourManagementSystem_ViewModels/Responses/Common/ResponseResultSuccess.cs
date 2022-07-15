namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    /// <summary>
    /// Response Result Success model extends Response Result
    /// Writer: DuyLH4
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class ResponseResultSuccess<Type> : ResponseResult<Type>
    {
        public ResponseResultSuccess()
        {
            Success = true;
        }

        public ResponseResultSuccess(Type result)
        {
            Success = true;
            Result = result;
        }
    }
}
