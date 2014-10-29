<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BlackJackWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Black Jack</h1>
        <p class="lead">Welcome to the black jack game.</p>
        <p><asp:Button ID="StartGameButton" OnClick="StartGameButton_Click" CssClass="btn btn-primary btn-large" runat="server" Text="Start game" /></p>

    </div>
        
    Rules: <asp:Literal ID="RuleSetLiteral" runat="server"></asp:Literal>
        
    <asp:Panel ID="ResultsPanel" runat="server" Visible="False">
        <div class="row">
            <div class="col-md-6">
                <h2>Player</h2>
                Cards:<br />
                <asp:ListView ID="PlayerHandListView" ItemType="BlackJack.model.Card" runat="server">
                    <LayoutTemplate>
                        <ul id="Player" class="cards">
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li><img src="img/deck/<%#: Item.GetColor() %>/<%#: Item.GetValue() %>.png" /></li>
                    </ItemTemplate>
                </asp:ListView>
                <br />
                Score: <span class="PlayerScore"><asp:Literal ID="PlayerScore" runat="server"></asp:Literal></span>
                <br /><br />

                <asp:Button ID="HitButton" OnClick="HitButton_Click" runat="server" Text="Hit" CssClass="btn btn-primary btn-large" />
                <asp:Button ID="StandButton" OnClick="StandButton_Click" runat="server" Text="Stand" CssClass="btn btn-primary btn-large" />

            </div>

            <div class="col-md-6">
                <h2>Croupier</h2>
                Cards:<br />
                <asp:ListView ID="DealerHandListView" ItemType="BlackJack.model.Card" runat="server">
                    <LayoutTemplate>
                        <ul id="Dealer" class="cards">
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li><img src="img/deck/<%#: Item.GetColor() %>/<%#: Item.GetValue() %>.png" /></li>
                    </ItemTemplate>
                </asp:ListView>
                <br />
                Score: <span class="DealerScore"><asp:Literal ID="DealerScore" runat="server"></asp:Literal></span>
            </div>
        </div>
        <br />
        <h4><asp:Literal ID="WinnerLiteral" runat="server"></asp:Literal></h4>
    </asp:Panel>
</asp:Content>
