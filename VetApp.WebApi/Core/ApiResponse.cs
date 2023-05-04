namespace VDMJasminka.WebApi.Core
{
    /// <summary>
    /// Represents a response from an API call with a status code and message
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class with a status code
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="message">Response message</param>
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Returns a default message for the specified HTTP status code
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Returns a default message for the specified HTTP status code
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <returns>A default message</returns>
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "Unauthorized request",
                404 => "The resource requested was not found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
