using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        public MessageHub()
        {
            Console.WriteLine("MessageHub is created.");
        }

        public async void OnAddMember(Intern member)
        {
            await Clients.All.SendAsync("AddMember", member.Name, member.Id);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
