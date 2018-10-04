using System;

namespace Commons.OperationResult
{
    public class ExtendedResult<TTypeExtended> : Result where TTypeExtended : Enum
    {
        public virtual TTypeExtended TypeExtended { get; set; }
        public ExtendedResult() => Type = Results.Other;
    }

    public class ExtendedResult<TTypeExtended, TResult> : Result<TResult> where TTypeExtended : Enum
    {
        public virtual TTypeExtended TypeExtended { get; set; }
        public ExtendedResult() => Type = Results.Other;

        public override Result<TTarget> ToResult<TTarget>(TTarget result)
               => new ExtendedResult<TTypeExtended, TTarget>
               {
                   Type = Type,
                   TypeExtended = TypeExtended,
                   Value = result
               };
    }
}
