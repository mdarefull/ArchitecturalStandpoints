using System;

namespace Commons.OperationResult
{
    public class ExceptionResult : FailureResult
    {
        public Exception InnerException { get; set; }
    }

    public sealed class ExceptionResult<TResult> : ExceptionResult, IResult<TResult>
    {
        public IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult)
            => new ExceptionResult<TNewResult>
            {
                ErrorDescription = ErrorDescription,
                ErrorTitle = ErrorTitle,
                InnerException = InnerException
            };
    }
}
