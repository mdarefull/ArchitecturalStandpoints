namespace Commons.OperationResult
{
    /// <summary>
    /// Represents the result of executing an operation sucessfully but that 
    /// doesn't return any value.
    /// </summary>
    public class SuccessResult : IResult { }

    /// <summary>
    /// Represents the result of executing an operation sucessfully 
    /// that is expected to return a value.
    /// </summary>
    /// <typeparam name="TResult">The return value's type of the operation.</typeparam>
    public sealed class SuccessResult<TResult> : SuccessResult, IResult<TResult>
    {
        /// <summary>
        /// Gets or sets the return value of the operation.
        /// </summary>
        public TResult Value { get; set; }

        /// <inheritdoc />
        public IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult)
            => Result.Success(newResult);
    }
}
