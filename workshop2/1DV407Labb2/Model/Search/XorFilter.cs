using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace _1DV407Labb2.Model.Search
{
    class XorFilter : IFilter
    {

        public IEnumerable<Member> CompareList(IEnumerable<Member> list1, IEnumerable<Member> list2)
        {
            IEnumerable<Member> newList1 = list1.Where<Member>(member => !list2.Contains(member));
            IEnumerable<Member> newList2 = list2.Where<Member>(member => !list1.Contains(member));
            IEnumerable<Member> newList3 = newList1.Concat(newList2).Distinct();
            return newList3;
        }

        
    }
}
