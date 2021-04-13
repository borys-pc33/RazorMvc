using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        private readonly InternshipObservable internshipObservable;

        public MessageHub(InternshipObservable internshipObservable)
        {
            Console.WriteLine("MessageHub is created.");
            this.internshipObservable = internshipObservable;
            internshipObservable.SubscribeToAddMember(this);
        }

        public async void OnAddMember(Intern member)
        {
            await Clients.All.SendAsync("AddMember", member.Name, member.Id);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        protected override void Dispose(bool disposing)
        {
            internshipObservable.UnsubscribeFromAddMember(this);
            base.Dispose(disposing);
        }
    }
}
