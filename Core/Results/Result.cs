namespace Core.Results
{
    /// <summary>
    /// Result of an operation
    /// </summary>
    public class Result<T> where T : class?
    {
        /// <summary>
        /// Instantiate a result
        /// </summary>
        public Result()
        {

        }

        /// <summary>
        /// Instantiate a result
        /// </summary>
        /// <param name="success">The success status</param>
        /// <param name="item">The processed item</param>
        public Result(bool success, T item)
        {
            Success = success;
            Item = item;
        }

        /// <summary>
        /// Instantiate a result, with optional errors collection
        /// </summary>
        /// <param name="success">The success status</param>
        /// <param name="item">The processed item</param>
        /// <param name="errors">Collection of errors</param>
        public Result(bool success, T item, IEnumerable<string> errors)
        {
            Success = success;
            Item = item;
            Errors = errors;
        }

        /// <summary>
        /// The success status
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The processed item
        /// </summary>
        public T? Item { get; set; }

        /// <summary>
        /// Collection of errors
        /// </summary>
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// A success result
        /// </summary>
        /// <param name="item">The processed item</param>
        /// <returns></returns>
        public static Result<T> SuccessResult(T item) => new(true, item);

        /// <summary>
        /// A failed result
        /// </summary>
        /// <param name="item">The processed item</param>
        /// <param name="error">Collection of errors</param>
        /// <returns></returns>
        public static Result<T> FailedResult(T item, string error) => new(false, item, new List<string> { error });
    }
}
