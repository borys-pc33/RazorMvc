using System.Collections.Generic;

namespace RazorMvc.Models
{
    public class InternshipClass
    {
        private List<string> _members;

        public InternshipClass()
        {
            _members = new List<string>
            {
                "Borys",
                "Liova",
                "Orest",
            };
        }

        public IList<string> Members
        {
            get { return _members; }
        }
    }
}
