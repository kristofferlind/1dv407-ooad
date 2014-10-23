using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IHitRuleFactory
    {
        IHitStrategy GetHitRule();
    }

    class BasicHitRule : IHitRuleFactory
    {
        public IHitStrategy GetHitRule()
        {
            return new BasicHitStrategy();
        }
    }

    class Soft17HitRule : IHitRuleFactory
    {
        public IHitStrategy GetHitRule()
        {
            return new Soft17HitStrategy();
        }
    }

    interface INewGameStrategyFactory
    {
        INewGameStrategy GetNewGameRule();
    }

    class AmericanNewGameRule : INewGameStrategyFactory
    {
        public INewGameStrategy GetNewGameRule()
        {
            return new AmericanNewGameStrategy();
        }
    }
    class InternationalNewGameRule : INewGameStrategyFactory
    {
        public INewGameStrategy GetNewGameRule()
        {
            return new InternationalNewGameStrategy();
        }
    }

    interface IWinRuleFactory
    {
        IWinStrategy GetWinRule();
    }

    class DealerWinRule : IWinRuleFactory
    {
        public IWinStrategy GetWinRule()
        {
            return new DealerWinStrategy();
        }
    }

    class PlayerWinRule : IWinRuleFactory
    {
        public IWinStrategy GetWinRule()
        {
            return new PlayerWinStrategy();
        }
    }

    public abstract class AbstractRulesFactory
    {
        internal abstract IHitRuleFactory GetHitRuleFactory();
        internal abstract INewGameStrategyFactory GetNewGameStrategyFactory();
        internal abstract IWinRuleFactory GetWinRuleFactory();
    }

    public class EasyRulesFactory : AbstractRulesFactory
    {
        internal override IHitRuleFactory GetHitRuleFactory()
        {
            return new BasicHitRule();
        }

        internal override INewGameStrategyFactory GetNewGameStrategyFactory()
        {
            return new InternationalNewGameRule(); 
        }

        internal override IWinRuleFactory GetWinRuleFactory()
        {
            return new PlayerWinRule();
        }
    }

    public class HardRulesFactory : AbstractRulesFactory
    {
        internal override IHitRuleFactory GetHitRuleFactory()
        {
            return new Soft17HitRule();
        }

        internal override INewGameStrategyFactory GetNewGameStrategyFactory()
        {
            return new AmericanNewGameRule();
        }

        internal override IWinRuleFactory GetWinRuleFactory()
        {
            return new DealerWinRule();
        }
    }
}
