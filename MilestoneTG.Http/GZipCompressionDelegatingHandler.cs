using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace MilestoneTG.Http
{
    /// <summary>
    /// A CompressionDelegatingHandler that uses GZip to compress requests.
    /// </summary>
    /// <example>
    /// var httpClient = new HttpClient(new GZipCompressionDelegatingHandler());
    /// </example>
    /// <seealso cref="MilestoneTG.Http.CompressionDelegatingHandler" />
    public class GZipCompressionDelegatingHandler : CompressionDelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GZipCompressionDelegatingHandler"/> class, setting the 
        /// <see cref="DelegatingHandler.InnerHandler"/> to a new <see cref="HttpClientHandler"/>.
        /// Use this constructor if this is the last handler in the pipeline.
        /// </summary>
        public GZipCompressionDelegatingHandler() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipCompressionDelegatingHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public GZipCompressionDelegatingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        /// <summary>
        /// Gets the instance of a <see cref="GZipStream"/> to use for compressing the request.
        /// </summary>
        /// <param name="destinationStream">The destination stream.</param>
        /// <returns>GZipStream</returns>
        protected override Stream GetCompressionStream(Stream destinationStream)
        {
            return new GZipStream(destinationStream, CompressionMode.Compress);
        }

        /// <summary>
        /// Gets the encoding value to use for the Accept-Encoding and
        /// Content-Encoding headers.
        /// </summary>
        /// <returns>gzip</returns>
        protected override string GetEncoding()
        {
            return "gzip";
        }
    }
}
