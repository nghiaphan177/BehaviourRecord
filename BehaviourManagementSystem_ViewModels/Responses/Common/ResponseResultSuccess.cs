namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
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
