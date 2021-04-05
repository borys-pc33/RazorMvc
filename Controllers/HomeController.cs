using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InternshipService intershipService;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, InternshipService intershipService, InternDbContext db)
        {
            _logger = logger;
            this.intershipService = intershipService;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(intershipService.GetMembers());
        }

        [HttpDelete]
        public void RemoveMember(int id)
        {
            intershipService.RemoveMember(id);
        }

        [HttpGet]
        public int AddMember(string memberName)
        {
            return intershipService.AddMember(memberName);
        }

        public IActionResult Privacy()
        {
            var interns = db.Interns.ToList();
            return View(interns);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
