using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    class NameLikeCriteria : ICriteria
    {
        private string name;

        public NameLikeCriteria(string name)
        {
            //name = Regex.Replace(name, "*", ".*");
            //name = Regex.Replace(name, "[a-zA-Z0-9]*", "({0})");
            ////name = Regex.Replace(name, "[!*]*", "({0})");
            //name = "$" + name + "^";
            this.name = name;
        }
        public List<Member> Filter(List<Member> members) {
            var nameLikeMembers = from member in members
                                  where member.Name.Contains(name)
                                  //where isMatch(member, name)
                                  select member;

            return nameLikeMembers.ToList<Member>();
        }

        //private bool isMatch(Member member, string name)
        //{
        //    return Regex.IsMatch(member.Name, name);
        //}
    }
}
