namespace Commons.OperationResult
{
    public class BasicOperationResult : OperationResult<OperationResults>
    {
        public static BasicOperationResult Success()
            => new BasicOperationResult
            {
                Type = OperationResults.Success,
            };
        public static BasicOperationResult<TResult> Success<TResult>(TResult result)
            => new BasicOperationResult<TResult>
            {
                Type = OperationResults.Success,
                Result = result,
            };
    }
    public class BasicOperationResult<TResult> : OperationResult<OperationResults, TResult> { }
}
