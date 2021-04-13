using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RazorMvc.Hubs;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly IInternshipService internshipService;
        private readonly IHubContext<MessageHub> messageHubContext;

        public InternController(
            IInternshipService internshipService,
            IHubContext<MessageHub> messageHubContext)
        {
            this.internshipService = internshipService;
            this.messageHubContext = messageHubContext;
        }

        // GET: api/<InternController>
        [HttpGet]
        public IEnumerable<Intern> Get()
        {
            return internshipService.GetMembers();
        }

        // GET api/<InternController>/5
        [HttpGet("{id}")]
        public Intern Get(int id)
        {
            return internshipService.GetMember(id);
        }

        // POST api/<InternController>
        [HttpPost]
        public int Post([FromBody] Intern intern)
        {
            int newId = internshipService.AddMember(intern.Name);

            messageHubContext.Clients.All.SendAsync("AddMember", intern.Name, newId);

            return newId;
        }

        // PUT api/<InternController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Intern intern)
        {
            internshipService.RenameMember(id, intern.Name);
        }

        // DELETE api/<InternController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            internshipService.RemoveMember(id);
        }
    }
}
