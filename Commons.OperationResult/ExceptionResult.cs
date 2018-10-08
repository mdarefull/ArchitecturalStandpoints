using System;

namespace Commons.OperationResult
{
    /// <summary>
    /// Represents the result of executing an operation that failed by getting into an exceptional state.
    /// It is designed to be returned by operations that does not return a value (<code>void</code>).
    /// </summary>
    /// <remarks>
    /// Usually, this class will be used to wrap any handled exception, but can also be
    /// used to return custom exceptions for custom scenarios.
    /// </remarks>
    public class ExceptionResult : FailureResult
    {
        /// <summary>
        /// Gets or sets the exception that caused this result.
        /// </summary>
        public Exception InnerException { get; set; }
    }

    /// <summary>
    /// Represents the result of executing an operation that failed by getting into an exceptional state.
    /// It is designed to be returned by operations that return a value (not <code>void</code>).
    /// </summary>
    /// <typeparam name="TResult">The return value's type of the operation.</typeparam>
    /// <remarks>
    /// Usually, this class will be used to wrap any handled exception, but can also be
    /// used to return custom exceptions for custom scenarios.
    /// </remarks>
    public sealed class ExceptionResult<TResult> : ExceptionResult, IResult<TResult>
    {
        /// <inheritdoc />
        public IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult)
            => new ExceptionResult<TNewResult>
            {
                ErrorDescription = ErrorDescription,
                ErrorTitle = ErrorTitle,
                InnerException = InnerException
            };
    }
}
