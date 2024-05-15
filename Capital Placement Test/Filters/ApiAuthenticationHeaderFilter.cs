using cosmo_db_test.Response;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Options;
using cosmo_db_test.Settings;
using cosmo_db_test.Constants;

namespace cosmo_db_test.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiAuthenticationHeaderFilter : Attribute, IAsyncActionFilter
    {
        private readonly HeaderAuthentication headerAuthentication;
        private string? SecretKey { get; set; }

        /// <summary>
        /// Header filer constructor
        /// </summary>
        /// <param name="headerAuthentication"></param>
        public ApiAuthenticationHeaderFilter(IOptionsSnapshot<HeaderAuthentication> headerAuthentication)
        {
            this.headerAuthentication = headerAuthentication.Value;
        }

        /// <summary>
        /// Action to be executed on load
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var hasData = context.HttpContext.Request.Headers.TryGetValue(ApiConstants.API_KEY_HEADER, out var _apiKey);
            if (!hasData)
            {
                context.Result = new ObjectResult(
                    new ErrorResponse
                    {
                        Errors = new List<string> { "Unauthorized:=> Api key required" } 
                    })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                return;
            }

            SecretKey = _apiKey;
            if (string.IsNullOrEmpty(headerAuthentication.Key) || headerAuthentication.Key != SecretKey!.Trim())
            {
                context.Result = new ObjectResult(
                        new ErrorResponse
                        {
                            Errors = new List<string> { "Invalid key entered" }
                        })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                return;
            }

            await next();
        }
    }
}
