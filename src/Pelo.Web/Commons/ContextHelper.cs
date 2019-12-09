using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Pelo.Web.Commons
{
    public class ContextHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetToken()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return string.Empty;
            }

            return ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims.ToList()[3]
                .Value;
        }
    }
}