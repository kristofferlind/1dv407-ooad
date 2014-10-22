using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    class NameStartsWithCriteria : ICriteria
    {
        private string name;

        public NameStartsWithCriteria(string name)
        {
            this.name = name;
        }
        public List<Member> Filter(List<Member> members)
        {
            var nameLikeMembers = from member in members
                                  where member.Name.StartsWith(name)
                                  select member;

            return nameLikeMembers.ToList<Member>();
        }
    }
}
