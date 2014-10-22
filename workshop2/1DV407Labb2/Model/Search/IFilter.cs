using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace _1DV407Labb2.Model.Search
{
    interface IFilter
    {
        IEnumerable<Member> CompareList(IEnumerable<Member> list1, IEnumerable<Member> list2);
    }
}
