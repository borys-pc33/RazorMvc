using RazorMvc.Models;
using System.Collections.Generic;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        int AddMember(string memberName);
        IList<Intern> GetMembers();
        void RemoveMember(int id);
    }
}