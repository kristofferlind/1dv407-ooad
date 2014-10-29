$(function () {
    var cardDealtHub = $.connection.cardDealtHub;
    
    if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
        $.connection.hub.start().done(function () {
            //console.log("connected");
        });
    }

    var cards = [];

    var gameScore = {
        player: 0,
        dealer: 0
    }

    var Card = function(color, value, player) {
        var createdCard = {
            color: color,
            value: value,
            player: player
        }

        return createdCard;
    }

    cardDealtHub.client.sendMessage = function (color, value, player, score) {
        var colors = ['Hearts', 'Spades', 'Diamonds', 'Clubs', 'Count', 'Hidden'];
        var values = ['Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Knight', 'Queen', 'King', 'Ace', 'Count', 'Hidden'];
        var card = Card(colors[color], values[value], player.substr("BlackJack.model.".length));
        var alreadyTaken = cards.some(function (element) {
            return element.color == card.color && element.value == card.value;
        });
        if (!alreadyTaken) {
            displayCard(colors[color], values[value], player.substr("BlackJack.model.".length));
            cards.push(Card(colors[color], values[value], player.substr("BlackJack.model.".length)));
            updateScore(value, player.substr("BlackJack.model.".length), score);
        }
    };

    var updateScore = function (addedValue, player, oldScore) {
        var values = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11, 0, 0];
        var addedScore = values[addedValue];
        var scoreField = document.querySelector("span." + player + "Score");
        var score;

        score = oldScore + addedScore;

        //Calculation will be wrong if one of the init cards is an ace
        //It will also be wrong for more than one ace
        if (score > 21 && hasAce(player)) {
            score -= 10;
        }

        scoreField.textContent = score;
        console.log(score + " = " + oldScore + "+" + addedScore);
    }

    var hasAce = function (player) {
        return cards.some(function (element) {
            return element.value == "Ace" && element.player == player;
        });
    }

    var displayCard = function (color, value, player) {
        var listItem = buildListItem(color, value);
        var playerList = document.querySelector("#" + player);
        playerList.appendChild(listItem);
        var card = listItem.firstChild.firstChild;
        //setTimeout(function () {
        //    card.classList.remove("flipped");
        //}, 100);
        card.classList.remove("flipped");
    }

    var buildListItem = function (color, value) {
        var li = document.createElement("li");
        var front = document.createElement("img");
        var back = document.createElement("img");
        var deckCard = document.createElement("div");
        var cardContainer = document.createElement("div");

        cardContainer.className = "cardContainer";

        deckCard.className = "deckCard flipped";

        front.src = "img/deck/" + color + "/" + value + ".png";
        front.className = "front";
        back.src = "img/deck/Hidden/Hidden.png";
        back.className = "back";

        deckCard.appendChild(front);
        deckCard.appendChild(back);

        cardContainer.appendChild(deckCard);

        li.appendChild(cardContainer);

        return li;
    }
});