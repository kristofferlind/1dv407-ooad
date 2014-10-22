using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    class NameEndsWithCriteria : ICriteria
    {
        private string name;

        public NameEndsWithCriteria(string name)
        {
            this.name = name;
        }
        public List<Member> Filter(List<Member> members)
        {
            var nameLikeMembers = from member in members
                                  where member.Name.EndsWith(name)
                                  select member;

            return nameLikeMembers.ToList<Member>();
        }
    }
}
