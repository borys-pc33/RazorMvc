using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService intershipService;

        public HomeController(ILogger<HomeController> logger, IInternshipService intershipService)
        {
            _logger = logger;
            this.intershipService = intershipService;
        }

        public IActionResult Index()
        {
            return View(intershipService.GetMembers());
        }

        public IActionResult Privacy()
        {
            return View(intershipService.GetMembers());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
