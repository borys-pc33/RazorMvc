using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

        [HttpPut]
        public async void Update(int id)
        {
            System.Console.WriteLine($"Updating member {id}");
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                dynamic jToken = JToken.Parse(content);
                string newName = (string)jToken.name;
                intershipService.RenameMember(id, newName);
            }
        }

        public IActionResult Privacy()
        {
            var interns = intershipService.GetMembers();
            return View(interns);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
