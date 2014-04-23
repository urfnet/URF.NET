using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Web.Areas.Spa.Extensions
{
    /// <summary>
    ///     Handles $format from query string and change accept header based on it.
    ///     $format=xml => application/xml
    ///     $format=json => application/json
    ///     $format=atom => application/atom+xml
    /// </summary>
    public class FormatQueryMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var queryStrings = request.RequestUri.ParseQueryString();
            var format = queryStrings["$format"];

            switch (format)
            {
                case null:
                    break;

                case "xml":
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/xml"));
                    break;

                case "json":
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    break;

                case "atom":
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/atom+xml"));
                    break;

                default:
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(format));
                    break;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}