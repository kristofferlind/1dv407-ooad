using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class RulesFactory : AcceptVisitor
    {
        private AbstractRulesFactory ruleSet;
        public RulesFactory(AbstractRulesFactory rules)
        {
            ruleSet = rules;
        }
        public IHitStrategy GetHitRule()
        {
            return ruleSet.GetHitRuleFactory().GetHitRule();
        }

        public INewGameStrategy GetNewGameRule()
        {
            return ruleSet.GetNewGameStrategyFactory().GetNewGameRule();
        }

        public IWinStrategy GetWinRule()
        {
            return ruleSet.GetWinRuleFactory().GetWinRule();
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
