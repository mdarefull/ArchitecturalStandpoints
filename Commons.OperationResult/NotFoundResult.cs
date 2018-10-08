namespace Commons.OperationResult
{
    /// <summary>
    /// Represents the result of execution an operation designed to retrieve a resource that was not able to find.
    /// </summary>
    /// <typeparam name="TResult">The return value's type of the operation.</typeparam>
    public sealed class NotFoundResult<TResult> : FailureResult<TResult>
    {
        /// <inheritdoc />
        public override IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default)
            => new NotFoundResult<TNewResult>
            {
                ErrorDescription = ErrorDescription,
                ErrorTitle = ErrorTitle,
            };
    }
}
