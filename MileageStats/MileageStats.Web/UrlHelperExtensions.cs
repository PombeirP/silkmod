using System;
using System.Web;
using System.Web.Mvc;

namespace MileageStats.Web
{
    public static class UrlHelperExtensions
    {
        public static string ToPublicUrl(this UrlHelper urlHelper, Uri relativeUri)
        {
            HttpContextBase httpContext = urlHelper.RequestContext.HttpContext;

            var uriBuilder = new UriBuilder
                                 {
                                     Host = httpContext.Request.Url.Host,
                                     Path = "/",
                                     Port = 80,
                                     Scheme = "http",
                                 };

            if (httpContext.Request.IsLocal)
            {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}
