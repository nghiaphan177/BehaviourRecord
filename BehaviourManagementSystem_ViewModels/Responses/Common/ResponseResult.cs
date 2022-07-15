namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    /// <summary>
    /// Response Result view model
    /// Writer: DuyLH4
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class ResponseResult<Type>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Type Result { get; set; }
    }
}
