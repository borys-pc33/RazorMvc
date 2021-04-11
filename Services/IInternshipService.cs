using System.Collections.Generic;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public interface IInternshipService
    {
        int AddMember(string memberName);

        IList<Intern> GetMembers();

        void RemoveMember(int id);

        void RenameMember(int id, string newName);

        Intern GetMember(int id);
    }
}