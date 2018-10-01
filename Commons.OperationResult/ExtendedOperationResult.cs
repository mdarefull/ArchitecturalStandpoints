using System;

namespace Commons.OperationResult
{
    public class ExtendedOperationResult<TTypeExtended> : OperationResult where TTypeExtended : Enum
    {
        public virtual TTypeExtended TypeExtended { get; set; }
        public ExtendedOperationResult() => Type = OperationResults.Other;
    }

    public class ExtendedOperationResult<TTypeExtended, TResult> : OperationResult<TResult> where TTypeExtended : Enum
    {
        public virtual TTypeExtended TypeExtended { get; set; }
        public ExtendedOperationResult() => Type = OperationResults.Other;

        public override OperationResult<TTarget> ToResult<TTarget>(TTarget result)
               => new ExtendedOperationResult<TTypeExtended, TTarget>
               {
                   Type = Type,
                   TypeExtended = TypeExtended,
                   Result = result
               };
    }
}
