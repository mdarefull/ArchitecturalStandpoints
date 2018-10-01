using System;

namespace Commons.OperationResult
{
    public class UnexpectedErrorResult : OperationResult
    {
        public Guid ErrorId { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDescription { get; set; }
        public Exception InnerException { get; set; }

        public UnexpectedErrorResult() => Type = OperationResults.UnexpectedError;
    }
}
