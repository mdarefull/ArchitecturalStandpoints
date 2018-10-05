using System;

namespace Commons.OperationResult
{
    public static class Result
    {
        public static IResult Success() => new SuccessResult();
        public static IResult<TResult> Success<TResult>(TResult value) => new SuccessResult<TResult> { Value = value };

        public static IResult Failure(string title = null, string description = null) => new FailureResult { ErrorTitle = title, ErrorDescription = description };
        public static IResult<TResult> Failure<TResult>(string title = null, string description = null) => new FailureResult<TResult> { ErrorTitle = title, ErrorDescription = description };

        public static IResult Exception(Exception innerException) => new ExceptionResult { InnerException = innerException };
        public static IResult<TResult> Exception<TResult>(Exception innerException) => new ExceptionResult<TResult> { InnerException = innerException };

        public static IResult<TResult> NotFound<TResult>() => new NotFoundResult<TResult>();
    }

    public interface IResult { }

    public interface IResult<TResult> : IResult
    {
        IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default);
    }
}
