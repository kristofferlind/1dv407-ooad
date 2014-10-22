using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    class BirthMonthCriteria : ICriteria
    {
        private string month;

        public BirthMonthCriteria(string month)
        {
            this.month = month;
        }
        public List<Member> Filter(List<Member> members)
        {
            var birthMonthMembers = from member in members
                                    where getBirthMonth(member) == month.ToLower()
                                    select member;

            return birthMonthMembers.ToList<Member>();
        }

        private string getBirthMonth(Member member)
        {
            string ssn = "19" + member.SocialSecurityNumber;
            DateTime birthDate = DateTime.ParseExact(ssn.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            string birthMonth;
            birthMonth = birthDate.ToString("MMM");
            return birthMonth;
        }
    }
}
