using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pelo.Web.Attributes
{
    public class CustomAuthentication : AuthorizeAttribute,
        IAuthorizationFilter
    {
        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("LogOn",
                    "Account",
                    new
                    {
                        returnUrl = context.HttpContext.Request.Path
                    });
            }
        }

        #endregion
    }
}