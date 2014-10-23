using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface Visitor
    {
        void Visit(rules.RulesFactory rulesFactory);
    }

    interface AcceptVisitor
    {
        void Accept(Visitor visitor);
    }
}
