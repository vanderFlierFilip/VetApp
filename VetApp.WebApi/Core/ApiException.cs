namespace VDMJasminka.WebApi.Core
{
    /// <summary>
    /// Represents an API exception with additional details
    /// </summary>
    public class ApiException : ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class
        /// </summary>
        /// <param name="statusCode">HTTP response status code</param>
        /// <param name="message">A brief message about the exception</param>
        /// <param name="details">Additional details about the exception</param>
        public ApiException(int statusCode, string message = null, string details = null)
            : base(statusCode, message)
        {
            Details = details;
        }

        /// <summary>
        /// Gets or sets additional details about the exception
        /// </summary>
        public string Details { get; set; }
    }
}
