using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Web.Attributes;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class TaskController : BaseController
    {
        public TaskController(ILogger<BaseController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}