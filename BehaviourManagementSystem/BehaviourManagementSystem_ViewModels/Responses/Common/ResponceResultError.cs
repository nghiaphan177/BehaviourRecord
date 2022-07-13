namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    public class ResponceResultError<Type> : ResponceResult<Type>
    {
        public string[] ValidationErrors { get; set; }

        public ResponceResultError()
        {
            Success = false;
        }

        public ResponceResultError(string message)
        {
            Success = false;
            Message = message;
        }

        public ResponceResultError(string[] validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
