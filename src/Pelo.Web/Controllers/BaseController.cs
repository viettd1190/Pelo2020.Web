using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Web.Models;

namespace Pelo.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserPrincipal _currentUser = new UserPrincipal();

        protected ILogger<BaseController> Logger;

        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }

        protected UserPrincipal CurrentUser
        {
            get
            {
                if(User.Identity.IsAuthenticated)
                {
                    var claimIdentities = (ClaimsIdentity) User.Identity;
                    if(claimIdentities != null)
                    {
                        var claims = claimIdentities.Claims;
                        var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                                       ?.Value ?? string.Empty;
                        var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)
                                             ?.Value ?? string.Empty;
                        var displayName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)
                                                ?.Value ?? string.Empty;
                        _currentUser.Update(id,
                                            username,
                                            displayName);
                    }
                }

                return _currentUser;
            }
        }
    }
}
