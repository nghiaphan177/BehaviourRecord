namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    /// <summary>
    /// Response Result Error model extends Response Result
    /// Writer: DuyLH4
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class ResponseResultError<Type> : ResponseResult<Type>
    {
        public string[] ValidationErrors { get; set; }

        public ResponseResultError()
        {
            Success = false;
        }

        public ResponseResultError(string message)
        {
            Success = false;
            Message = message;
        }

        public ResponseResultError(string[] validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
