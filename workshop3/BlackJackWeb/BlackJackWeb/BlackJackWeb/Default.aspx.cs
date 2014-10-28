﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BlackJack.model;
using Microsoft.AspNet.SignalR;

namespace BlackJackWeb
{
    public class CardDealtHub : Hub
    {
        static CardDealtHub()
        {
            
        }

        public string Activate()
        {
            return "test";
        }
    }

    public partial class _Default : Page, CardDealtListener
    {
        private Game game;

        private void save()
        {
            HttpContext.Current.Session["Game"] = game;
        }

        private void reset()
        {
            HttpContext.Current.Session["Game"] = null;
            game = new Game();
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
            game = HttpContext.Current.Session["Game"] as Game;
            if (game == null)
            {
                game = new Game();
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
            System.Threading.Thread.Sleep(1500);
            GlobalHost.ConnectionManager.GetHubContext<CardDealtHub>().Clients.All.sendMessage(card.GetColor(), card.GetValue(), player.ToString());
        }
    }
}