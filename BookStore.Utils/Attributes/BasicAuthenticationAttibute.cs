using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookStore.Utils.Attributes
{
    public class BasicAuthenticationAttibute : AuthorizationFilterAttribute
    {
        private const string BasicAuthResponseHeader = "WWW-Autenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //var header = actionContext.Request.Headers;
            AuthenticationHeaderValue authValue = actionContext.Request.Headers.Authorization;
            if (authValue != null && !string.IsNullOrEmpty(authValue.Parameter) && authValue.Scheme == BasicAuthResponseHeaderValue)
            {
                string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authValue.Parameter)).Split(new[] { ':' });
            }
            else
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}