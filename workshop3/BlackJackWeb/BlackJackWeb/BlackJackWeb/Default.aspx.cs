using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJack.model;
using Microsoft.AspNet.SignalR;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.model.rules;

namespace BlackJackWeb
{
    public static class UserSession
    {
        private static string connection;
        public static string Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

    }

    public class CardDealtHub : Hub
    {
        static CardDealtHub()
        {
        }

        public override Task OnConnected()
        {
            UserSession.Connection = Context.ConnectionId;

            return base.OnConnected();
        }
    }

    public partial class _Default : Page, CardDealtListener
    {
        private Game game;
        private AbstractRulesFactory ruleSet;

        private void save()
        {
            HttpContext.Current.Session["Game"] = game;
        }

        private void reset()
        {
            HttpContext.Current.Session["Game"] = null;
            game = new Game(ruleSet);
            save();
        }

        private void setRules()
        {
            var hitRule = game.GetHitRule();
            var newGameRule = game.GetNewGameRule();
            var winRule = game.GetWinRule();
            RuleSetLiteral.Text = "<br>HitRule: " + hitRule + "<br>NewGameRule: " + newGameRule + "<br>WinRule: " + winRule;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ruleSet = new EasyRulesFactory();
            //ruleSet = new model.rules.HardRulesFactory();


            game = HttpContext.Current.Session["Game"] as Game;
            if (game == null)
            {
                game = new Game(ruleSet);
                HttpContext.Current.Session["Game"] = game;
            }
            else
            {
                ShowResults();
            }
            game.Subscribe(this);
            setRules();
        }

        protected void StartGameButton_Click(object sender, EventArgs e)
        {
            reset();
            game.NewGame();
            save();
            ShowResults();
            Response.Redirect("Default.aspx");
        }

        protected void HitButton_Click(object sender, EventArgs e)
        {
            game.Hit();
            save();
            ShowResults();
            Response.Redirect("Default.aspx");
        }

        protected void StandButton_Click(object sender, EventArgs e)
        {
            game.Stand();
            save();
            ShowResults();
            Response.Redirect("Default.aspx");
        }

        private void ShowResults()
        {
            ResultsPanel.Visible = true;

            DealerHandListView.DataSource = game.GetDealerHand();
            DealerHandListView.DataBind();

            PlayerHandListView.DataSource = game.GetPlayerHand();
            PlayerHandListView.DataBind();
            DealerScore.Text = game.GetDealerScore().ToString();

            PlayerScore.Text = game.GetPlayerScore().ToString();

            if (game.IsGameOver())
            {
                if (game.IsDealerWinner())
                {
                    WinnerLiteral.Text = "Dealer won";
                }
                else
                {
                    WinnerLiteral.Text = "You won, congratulations.";
                }
            }
            else
            {
                WinnerLiteral.Text = "";
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<BlackJack.model.Card> ListView1_GetData()
        {
            return game.GetDealerHand() as IQueryable<Card>;
        }

        public void CardDealt(Card card, Player player)
        {
            Thread.Sleep(550);
            if (UserSession.Connection != null)
            {
                GlobalHost.ConnectionManager.GetHubContext<CardDealtHub>().Clients.Client(UserSession.Connection).sendMessage(card.GetColor(), card.GetValue(), player.ToString(), player.CalcScore());
            }
        }
    }
}