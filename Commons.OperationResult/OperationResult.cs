using System;

namespace Commons.OperationResult
{
    public class OperationResult<TType> where TType : Enum
    {
        public TType Type { get; set; }
    }

    public class OperationResult<TType, TResult> : OperationResult<TType> where TType : Enum
    {
        public TResult Result { get; set; }
    }
}
