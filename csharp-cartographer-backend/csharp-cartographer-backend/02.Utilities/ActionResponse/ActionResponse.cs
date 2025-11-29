namespace csharp_cartographer_backend._02.Utilities.ActionResponse
{
    public class ActionResponse
    {
        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class PassingAR : ActionResponse
    {
        public PassingAR()
        {
            IsSuccess = true;
        }
    }

    public class FailingAR : ActionResponse
    {
        public FailingAR()
        {
            IsSuccess = false;
            ErrorMessage = string.Empty;
        }

        public FailingAR(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }

    public class ActionResponse<T>
    {
        public T? Content { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }

    public class PassingAR<T> : ActionResponse<T>
    {
        public PassingAR(T content)
        {
            Content = content;
            IsSuccess = true;
        }
    }

    public class FailingAR<T> : ActionResponse<T>
    {
        public FailingAR(T content)
        {
            Content = content;
            IsSuccess = false;
            ErrorMessage = string.Empty;
        }

        public FailingAR(string errorMessage)
        {
            Content = default;
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public FailingAR(T content, string errorMessage)
        {
            Content = content;
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}
