namespace Assets.Develop.Scripts.Utility
{
    public struct MethodResult
    {
        public bool Success { get; }
        public bool Failure { get { return !Success; } }
        public string ErrorMessage { get; }

        private MethodResult(bool success, string errorMessage = null)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static MethodResult Fail(string errorMesage)
        {
            return new MethodResult(false, errorMesage);
        }

        public static MethodResult Ok()
        {
            return new MethodResult(true);
        }
    }
}
