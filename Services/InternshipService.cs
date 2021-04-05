using System;
using System.Collections.Generic;
using System.Linq;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly InternshipClass _internshipClass = new ();

        public void RemoveMember(int id)
        {
            var itemToBeDeleted = _internshipClass.Members.Single(_ => _.Id == id);
            _internshipClass.Members.Remove(itemToBeDeleted);
        }

        public int AddMember(string memberName)
        {
            var maxId = _internshipClass.Members.Max(_ => _.Id);
            var newId = maxId + 1;

            var intern = new Intern()
            {
                Id = maxId + 1,
                Name = memberName,
                DateOfJoin = DateTime.Now,
            };

            _internshipClass.Members.Add(intern);
            return newId;
        }

        public IList<Intern> GetMembers()
        {
            return _internshipClass.Members;
        }
    }
}
