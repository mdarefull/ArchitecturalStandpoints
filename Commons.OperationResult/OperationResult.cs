namespace Commons.OperationResult
{
    public class OperationResult<TResult> : OperationResult
    {
        public virtual TResult Result { get; set; }

        public virtual OperationResult<TTarget> ToResult<TTarget>(TTarget result)
               => new OperationResult<TTarget>
               {
                   Type = Type,
                   Result = result
               };

        public static OperationResult<TResult> Success(TResult result)
            => new OperationResult<TResult>
            {
                Type = OperationResults.Success,
                Result = result,
            };
    }

    public class OperationResult
    {
        public virtual OperationResults Type { get; set; }

        public static OperationResult Success() => new OperationResult { Type = OperationResults.Success, };

        public static OperationResult<TResult> Success<TResult>(TResult result)
            => new OperationResult<TResult>
            {
                Type = OperationResults.Success,
                Result = result,
            };
    }
}
