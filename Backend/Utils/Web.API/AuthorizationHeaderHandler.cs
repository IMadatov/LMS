using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace Web.API;

public class AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor):DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        var httpContext = httpContextAccessor.HttpContext;

        if(httpContext.User.Identity.IsAuthenticated)
        {
            foreach(var cookie in httpContext.Request.Cookies)
            {
                request.Headers.Add(cookie.Key, cookie.Value);
            }
            //request.Headers.Authorization = new AuthenticationHeaderValue(IdentityConstants.ApplicationScheme, httpContext.Request.Headers["Cookie"]);


        }
        return base.SendAsync(request, cancellationToken);
    }

}
