namespace BehaviourManagementSystem_API.Utilities
{
    public static class CheckRequestExtensions
    {
        public static bool CheckRequest(this string str) => string.IsNullOrEmpty(str);
        public static bool CheckRequest(this object obj) => obj is null;
    }
}
