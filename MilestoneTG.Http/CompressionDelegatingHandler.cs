using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MilestoneTG.Http
{
    /// <summary>
    /// Base class for all HTTP Compression implementations.
    /// </summary>
    /// <seealso cref="System.Net.Http.DelegatingHandler" />
    public abstract class CompressionDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionDelegatingHandler"/> class, setting the 
        /// <see cref="DelegatingHandler.InnerHandler"/> to a new <see cref="HttpClientHandler"/>.
        /// Use this constructor if this is the last handler in the pipeline.
        /// </summary>
        public CompressionDelegatingHandler() : this(new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionDelegatingHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public CompressionDelegatingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        /// <summary>
        /// Send as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(GetEncoding()));

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] rawBytes = await request.Content.ReadAsByteArrayAsync();

                using (Stream compressionStream = GetCompressionStream(ms))
                    await compressionStream.WriteAsync(rawBytes, 0, rawBytes.Length);

                ms.Position = 0;

                request.Content = new StreamContent(ms);
                request.Content.Headers.ContentEncoding.Add(GetEncoding());

                return await base.SendAsync(request, cancellationToken);
            }
        }

        /// <summary>
        /// When implemented in a derived class, gets the encoding value to use for the Accept-Encoding and 
        /// Content-Encoding headers.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string GetEncoding();

        /// <summary>
        /// When implemented in a derived class, gets the compression stream to use for compressing the request.
        /// </summary>
        /// <param name="destinationStream">The destination stream.</param>
        /// <returns>Stream.</returns>
        protected abstract Stream GetCompressionStream(Stream destinationStream);
    }
}
