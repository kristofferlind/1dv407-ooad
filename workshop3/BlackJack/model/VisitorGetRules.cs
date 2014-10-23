using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class VisitorGetRules : Visitor
    {
        public string HitRuleName { get; private set; }
        public string NewGameRuleName { get; private set; }
        public string WinRuleName { get; private set; }

        public void Visit(rules.RulesFactory rules) {
            HitRuleName = rules.GetHitRule().GetType().Name;
            NewGameRuleName = rules.GetNewGameRule().GetType().Name;
            WinRuleName = rules.GetWinRule().GetType().Name;
        }
    }
}
