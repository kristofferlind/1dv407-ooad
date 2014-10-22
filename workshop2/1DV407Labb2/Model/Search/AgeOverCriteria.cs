using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    class AgeOverCriteria : ICriteria
    {
        private int age;
        public AgeOverCriteria(int age)
        {
            this.age = age;
        }
        public List<Member> Filter(List<Member> members)
        {
            var ageOverMembers = from member in members
                                 where getAge(member) > age
                                 select member;
            return ageOverMembers.ToList<Member>();
        }
        private int getAge(Member member)
        {
            DateTime now = DateTime.Now;
            string ssn = "19" + member.SocialSecurityNumber;
            DateTime birthDate = DateTime.ParseExact(ssn.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            int age = 0;
            age = now.Year - birthDate.Year;
            if (now < birthDate.AddYears(age))
            {
                age -= 1;
            }

            return age;
        }
    }
}
