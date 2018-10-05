namespace Commons.OperationResult
{
    public sealed class NotFoundResult<TResult> : FailureResult<TResult> {
        public override IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default)
            => new NotFoundResult<TNewResult>
            {
                ErrorDescription = ErrorDescription,
                ErrorTitle = ErrorTitle,
            };
    }
}
