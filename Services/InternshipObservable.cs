using System;
using System.Collections.Generic;
using RazorMvc.Hubs;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    /// <summary>
    /// Other name is subject. Should be a singleton.
    /// </summary>
    public class InternshipObservable
    {
        private readonly List<IAddMemberSubscriber> subscribers;

        public InternshipObservable()
        {
            this.subscribers = new List<IAddMemberSubscriber>();
        }

        public void FireAddMember(Intern intern)
        {
            subscribers.ForEach(subscriber => subscriber.OnAddMember(intern));
        }

        public void SubscribeToAddMember(IAddMemberSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        internal void UnsubscribeFromAddMember(IAddMemberSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }
    }
}
