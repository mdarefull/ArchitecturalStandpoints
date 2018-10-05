namespace Commons.OperationResult
{
    public class SuccessResult : IResult { }

    public sealed class SuccessResult<TResult> : SuccessResult, IResult<TResult>
    {
        public TResult Value { get; set; }

        public IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult)
            => Result.Success(newResult);
    }
}
