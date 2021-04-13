using System;
using System.Collections.Generic;
using System.Linq;
using RazorMvc.Data;
using RazorMvc.Models;

namespace RazorMvc.Services
{
    public class DbInternshipService : IInternshipService
    {
        private readonly InternDbContext db;

        public DbInternshipService(InternDbContext db)
        {
            this.db = db;
        }

        public int AddMember(string memberName)
        {
            var intern = new Intern { Name = memberName, DateOfJoin = DateTime.Now };
            db.Interns.Add(intern);
            db.SaveChanges();

            return intern.Id;
        }

        public Intern GetMember(int id)
        {
            var intern = db.Find<Intern>(id);
            return intern;
        }

        public IList<Intern> GetMembers()
        {
            return db.Interns.ToList();
        }

        public void RemoveMember(int id)
        {
            var intern = db.Find<Intern>(id);
            db.Remove(intern);
            db.SaveChanges();
        }

        public void RenameMember(int id, string newName)
        {
            var intern = db.Find<Intern>(id);
            intern.Name = newName;
            db.SaveChanges();
        }
    }
}
