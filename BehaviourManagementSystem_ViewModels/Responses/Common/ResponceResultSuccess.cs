namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    public class ResponceResultSuccess<Type> : ResponceResult<Type>
    {
        public ResponceResultSuccess()
        {
            Success = true;
        }

        public ResponceResultSuccess(Type result)
        {
            Success = true;
            Result = result;
        }
    }
}
