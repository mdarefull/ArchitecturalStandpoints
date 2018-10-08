namespace Commons.OperationResult
{
    /// <summary>
    /// Represents the result of executing an operation that failed. 
    /// It is the base class for the result of every operation execution that failed.
    /// It is designed to be returned by operations that does not return a value (<code>void</code>).
    /// </summary>
    public class FailureResult : IResult
    {
        /// <summary>
        /// Gets or sets a title for the failure.
        /// </summary>
        public virtual string ErrorTitle { get; set; }
        /// <summary>
        /// Gets or sets a description for the failure.
        /// </summary>
        public virtual string ErrorDescription { get; set; }
    }

    /// <summary>
    /// Represents the result of executing an operation that failed.
    /// It is the base class for the result of every operation execution that failed.
    /// It is designed to be returned by operations that return a value (not <code>void</code>).
    /// </summary>
    /// <typeparam name="TResult">The return value's type of the operation.</typeparam>
    public class FailureResult<TResult> : FailureResult, IResult<TResult>
    {
        /// <inheritdoc />
        public virtual IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default)
            => Result.Failure<TNewResult>(ErrorTitle, ErrorDescription);
    }
}
