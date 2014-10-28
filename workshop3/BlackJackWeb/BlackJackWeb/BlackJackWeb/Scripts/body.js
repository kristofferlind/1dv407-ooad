$(function () {
    var cardDealtHub = $.connection.cardDealtHub;
    
    if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
        $.connection.hub.start()
    }

    var cards = [];


    cardDealtHub.client.sendMessage = function (color, value, player) {
        var colors = ['Hearts', 'Spades', 'Diamonds', 'Clubs', 'Count', 'Hidden'];
        var values = ['Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Knight', 'Queen', 'King', 'Ace', 'Count', 'Hidden'];
        var card = colors[color] + "-" + values[value];
        var alreadyTaken = cards.some(function (element) {
            return element == card;
        });
        if (!alreadyTaken) {
            displayCard(colors[color], values[value], player.substr("BlackJack.model.".length));
            cards.push(card);
            updateScore(value, player.substr("BlackJack.model.".length));
        }
    };

    var updateScore = function (addedScore, player) {
        var scoreField = document.querySelector("span." + player + "Score");
        var score = parseInt(scoreField.textContent, 10);
        var score = score + addedScore;
        scoreField.textContent = score;
    }

    var displayCard = function (color, value, player) {
        var listItem = buildListItem(color, value);
        var playerList = document.querySelector("#" + player);
        playerList.appendChild(listItem);
    }

    var buildListItem = function (color, value) {
        var li = document.createElement("li");
        var img = document.createElement("img");

        img.src = "img/deck/" + color + "/" + value + ".png";
        li.appendChild(img);

        return li;
    }
});