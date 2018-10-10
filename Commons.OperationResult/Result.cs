using System;

namespace Commons.OperationResult
{
    /// <summary>
    /// Provides methods to create common operation results as simple IResult.
    /// </summary>
    public static class Result
    {
        /// <summary>
        /// Creates a new <see cref="SuccessResult"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="SuccessResult"/>.</returns>
        public static IResult Success() => new SuccessResult();
        /// <summary>
        /// Creates a new <see cref="SuccessResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Return value's type of the operation.</typeparam>
        /// <param name="value">The return value of the operation.</param>
        /// <returns>A new instance of <see cref="Success{TResult}(TResult)"/>.</returns>
        public static IResult<TResult> Success<TResult>(TResult value) => new SuccessResult<TResult> { Value = value };

        /// <summary>
        /// Creates a new <see cref="FailureResult"/>.
        /// </summary>
        /// <param name="code">Code for the error.</param>
        /// <param name="title">Title for the error.</param>
        /// <param name="description">Description of the error.</param>
        /// <returns>A new instance of <see cref="FailureResult"/>.</returns>
        public static IResult Failure(string code = null, string title = null, string description = null)
            => new FailureResult
            {
                ErrorCode = code,
                ErrorTitle = title,
                ErrorDescription = description,
            };
        /// <summary>
        /// Creates a new <see cref="FailureResult{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult">Return value's type of the operation.</typeparam>
        /// <param name="title">Title for the error.</param>
        /// <param name="description">Description of the error.</param>
        /// <returns>A new instance of <see cref="FailureResult{TResult}"/>.</returns>
        public static IResult<TResult> Failure<TResult>(string title = null, string description = null) => new FailureResult<TResult> { ErrorTitle = title, ErrorDescription = description };

        /// <summary>
        /// Creates a new <see cref="ExceptionResult"/>.
        /// </summary>
        /// <param name="innerException"><see cref="Exception(System.Exception)"/> that was caught.</param>
        /// <returns>A new instance of <see cref="ExceptionResult"/>.</returns>
        public static IResult Exception(Exception innerException) => new ExceptionResult { InnerException = innerException };
        /// <summary>
        /// Creates a new <see cref="Exception{TResult}(System.Exception)"/>.
        /// </summary>
        /// <typeparam name="TResult">Return value's type of the operation.</typeparam>
        /// <param name="innerException"><see cref="Exception(System.Exception)"/> that was caught.</param>
        /// <returns>A new instance of <see cref="Exception{TResult}(System.Exception)"/>.</returns>
        public static IResult<TResult> Exception<TResult>(Exception innerException) => new ExceptionResult<TResult> { InnerException = innerException };

        /// <summary>
        /// Creates a new <see cref="NotFoundResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Return value's type of the operation.</typeparam>
        /// <returns>A new instance of <see cref="NotFoundResult{TResult}"/>.</returns>
        public static IResult<TResult> NotFound<TResult>() => new NotFoundResult<TResult>();
    }
}
