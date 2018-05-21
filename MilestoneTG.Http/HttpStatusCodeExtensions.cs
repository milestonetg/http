using System.Net;

namespace MilestoneTG.Http
{
    /// <summary>
    /// Additional HTTP status codes typically found in HTTP/ReST-ful services that are not in Microsoft's
    /// <see cref="System.Net.HttpStatusCode"/> enum.
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// Equivalent to HTTP status 422. Usually when an POST/PUT model is invalid. 
        /// </summary>
        public static readonly HttpStatusCode UnprocessableEntity = (HttpStatusCode)422;

        /// <summary>
        /// Equivalent to HTTP status 424. Usually when a dependency service call fails causing the original 
        /// request to fail.
        /// </summary>
        public static readonly HttpStatusCode FailedDependency = (HttpStatusCode)424;

        /// <summary>
        /// Equivalent to HTTP status 429. Usually when a rate limit is reached.
        /// </summary>
        public static readonly HttpStatusCode TooManyRequests = (HttpStatusCode)429;
    }
}