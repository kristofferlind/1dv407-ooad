using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV407Labb2.Model.Search
{
    public interface ICriteria
    {
        List<Member> Filter(List<Member> members);
    }
}
