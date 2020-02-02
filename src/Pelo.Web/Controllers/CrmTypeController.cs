using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pelo.Web.Controllers
{
    public class CrmTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}