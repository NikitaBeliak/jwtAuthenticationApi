using Domain.Entitys;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace Infrastracture.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var user = (User)filterContext.HttpContext.Items["User"];

            if (user == null)
            {
                filterContext.Result = new UnauthorizedResult();
            }
        }
    }
}
