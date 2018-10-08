namespace Commons.OperationResult
{
    /// <summary>
    /// Implements the Operation result pattern.
    /// Represents the result of executing an operation that doesn't return any value.
    /// </summary>
    public interface IResult { }

    /// <summary>
    /// Implements the Operation result pattern.
    /// Represents the result of executing an operation that return a value.
    /// </summary>
    /// <typeparam name="TResult">Return type of the operation.</typeparam>
    public interface IResult<TResult> : IResult
    {
        /// <summary>
        /// Converts the operation result into a new instance of istels with the new return type.
        /// </summary>
        /// <typeparam name="TNewResult">Return type of the operation that it will be converted to.</typeparam>
        /// <param name="newResult">
        /// Return value of the operation that it will be converted to. In the case the result type don't return a value 
        ///  (typically a failure), we can safely pass it default's value.
        /// </param>
        /// <returns>A new instance of the operation result keeping its properties but with a new return type.</returns>
        IResult<TNewResult> ConvertTo<TNewResult>(TNewResult newResult = default);
    }
}
