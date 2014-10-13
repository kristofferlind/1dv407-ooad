using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1DV407Labb2.Controller;

namespace _1DV407Labb2
{
    class Program
    {
        static void Main(string[] args)
        {
            var memberController = new MemberController();
            memberController.Start();
        }
    }
}
