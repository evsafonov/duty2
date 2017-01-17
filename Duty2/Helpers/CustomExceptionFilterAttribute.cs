using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Filters;


namespace Duty2.Helpers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            var request = context.Request.RequestUri.AbsoluteUri;
            string userName =
                ((ClaimsIdentity)((HttpContextWrapper)context.Request.Properties["MS_HttpContext"]).User.Identity)?.Claims
                    .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            logger.Error("Произошла ошибка у пользователя: {0}; Запрос: {1}; Exception.Message: {2}; Exception.Message: {3}", userName, request, exception.Message, exception.StackTrace);
            context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }
    }
}