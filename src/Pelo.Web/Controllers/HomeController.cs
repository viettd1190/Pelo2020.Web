using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pelo.Web.Attributes;
using Pelo.Web.Models;

namespace Pelo.Web.Controllers
{
    [CustomAuthentication]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}