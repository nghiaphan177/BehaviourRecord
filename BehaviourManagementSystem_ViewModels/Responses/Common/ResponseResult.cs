namespace BehaviourManagementSystem_ViewModels.Responses.Common
{
    public class ResponseResult<Type>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Type Result { get; set; }
    }
}
