using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pelo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ILogger<BaseController> Logger;

        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }
    }
}