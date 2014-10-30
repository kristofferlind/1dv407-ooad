using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            model.rules.AbstractRulesFactory ruleSet = new model.rules.EasyRulesFactory();
            //model.rules.AbstractRulesFactory ruleSet = new model.rules.HardRulesFactory();

            model.Game g = new model.Game(ruleSet);
            view.IView v = new view.SwedishView();//new view.SimpleView();
            controller.PlayGame ctrl = new controller.PlayGame();

            g.Subscribe(ctrl);

            while (ctrl.Play(g, v));
        }
    }
}
