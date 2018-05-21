using System.IO;
using System.IO.Compression;
using System.Net.Http;

namespace MilestoneTG.Http
{
    /// <summary>
    /// A CompressionDelegatingHandler that uses Deflate to compress requests.
    /// </summary>
    /// <seealso cref="MilestoneTG.Http.CompressionDelegatingHandler" />
    public class DeflateCompressionDelegatingHandler : CompressionDelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionDelegatingHandler"/> class, setting the 
        /// <see cref="DelegatingHandler.InnerHandler"/> to a new <see cref="HttpClientHandler"/>.
        /// Use this constructor if this is the last handler in the pipeline.
        /// </summary>
        public DeflateCompressionDelegatingHandler() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionDelegatingHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public DeflateCompressionDelegatingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        /// <summary>
        /// Gets the instance of a <see cref="DeflateStream"/> to use for compressing the request.
        /// </summary>
        /// <param name="destinationStream">The destination stream.</param>
        /// <returns>DeflateStream</returns>
        protected override Stream GetCompressionStream(Stream destinationStream)
        {
            return new DeflateStream(destinationStream, CompressionMode.Compress);
        }

        /// <summary>
        /// Gets the encoding value to use for the Accept-Encoding and
        /// Content-Encoding headers.
        /// </summary>
        /// <returns>deflate</returns>
        protected override string GetEncoding()
        {
            return "deflate";
        }
    }
}
