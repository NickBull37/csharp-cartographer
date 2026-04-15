namespace csharp_cartographer_backend._02.Utilities.ActionResponse
{
    public class ActionResponse
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string? ErrorMessage { get; }

        private ActionResponse(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ActionResponse Success()
            => new(isSuccess: true, errorMessage: null);

        public static ActionResponse Failure(string errorMessage)
            => new(isSuccess: false, errorMessage);
    }

    public class ActionResponse<T>
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T? Content { get; }

        public string? ErrorMessage { get; }

        private ActionResponse(bool isSuccess, T? content, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Content = content;
            ErrorMessage = errorMessage;
        }

        public static ActionResponse<T> Success(T content)
            => new(true, content, null);

        public static ActionResponse<T> Failure(string errorMessage)
            => new(false, default, errorMessage);

        public static ActionResponse<T> Failure(T content, string errorMessage)
            => new(false, content, errorMessage);
    }
}
