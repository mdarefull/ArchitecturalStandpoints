namespace Commons.OperationResult
{
    public class Result<TResult> : Result
    {
        public virtual TResult Value { get; set; }

        public virtual Result<TTarget> ToResult<TTarget>(TTarget result)
               => new Result<TTarget>
               {
                   Type = Type,
                   Value = result
               };

        public static Result<TResult> Success(TResult result)
            => new Result<TResult>
            {
                Type = Results.Success,
                Value = result,
            };
    }

    public class Result
    {
        public virtual Results Type { get; set; }

        public static Result Success() => new Result { Type = Results.Success, };

        public static Result<TResult> Success<TResult>(TResult result)
            => new Result<TResult>
            {
                Type = Results.Success,
                Value = result,
            };
    }
}
