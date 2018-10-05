namespace Commons.OperationResult
{
    public class FailureResult : IResult
    {
        public virtual string ErrorTitle { get; set; }
        public virtual string ErrorDescription { get; set; }
    }

    public class FailureResult<TResult> : FailureResult, IResult<TResult>
    {
        public virtual IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default)
            => Result.Failure<TNewResult>(ErrorTitle, ErrorDescription);
    }
}
