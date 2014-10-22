using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace _1DV407Labb2.Model.Search
{
    class OrFilter : IFilter
    {

        public IEnumerable<Member> CompareList(IEnumerable<Member> list1, IEnumerable<Member> list2)
        {

            IEnumerable<Member> newList = list1.Concat(list2).Distinct(); //TODO: check that duplicates are removed
            return newList;
        }

    }
}
