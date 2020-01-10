using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelo.Web.Attributes;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class WarrantyController : BaseController
    {
        public WarrantyController(ILogger<BaseController> logger) : base(logger)
        {
        }
    }
}