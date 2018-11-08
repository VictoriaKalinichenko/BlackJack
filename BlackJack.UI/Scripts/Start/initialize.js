$(document).ready(function () {
    var human;
    var dealer;
    var botList;


    $(window).load(function () {
        restoreRound();
    });

    function restoreRound() {
        var gameId = $("#gameid").val();
        var isnewgame = $("#isnewgame").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/Restore",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (!isnewgame) {
                    reloadPlayersOnStart(response);
                    reloadGamePlay(response.RoundResult);
                }

                if (isnewgame) {
                    startRound();
                }
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function startRound() {
        var gameId = $("#gameid").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/Start",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadPlayersOnStart(response);
                reloadGamePlay(response.RoundResult);
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function takeCard() {
        var gameId = $("#gameid").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/TakeCard",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadPlayersOnTakeCard(response);
                reloadGamePlay(response.RoundResult);
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function endRound() {
        var gameId = $("#gameid").val();
        var transParam = {
            GameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/End",
            data: transParam,
            dataType: "json",
            success: function (response) {
                $("#gameplay").text('');
                $("#gameplay").append(`<p>${response}</p>`);
                drowButtonsEndRound();
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function reloadPlayersOnStart(response) {
        human = new Player(response.Human, "human");
        dealer = new Player(response.Dealer, "dealer");
        botList = new BotList(response.Bots);

        human.show();
        dealer.show();
        botList.show();
    }

    function reloadPlayersOnTakeCard(response) {
        human.reloadCards(response.Human);
        dealer.reloadCards(response.Dealer);
        botList.reloadCards(response.Bots);

        human.show();
        dealer.show();
        botList.show();
    }

    function reloadGamePlay(roundResult) {
        $("#gameplay").text('');

        if (roundResult != "") {
            $("#gameplay").append(`<p>${roundResult}</p>`);
            drowButtonsEndRound();
        }

        if (roundResult == "") {
            drowButtonsForTakeCard();
        }
    }

    function drowButtonsForTakeCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeonemorecard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function () {
                takeCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "donttake",
            value: "Don\'t take",
            class: "btn btn-default",
            click: function () {
                endRound();
            }
        });

        $("#gameplay").append(takeOneMoreCardButton);
        $("#gameplay").append(dontTakeButton);
    }

    function drowButtonsEndRound() {
        var endRoundButton = $('<input/>', {
            type: "button",
            id: "endroundbutton",
            value: "End round",
            class: "btn btn-primary",
            click: function () {
                startRound();
            }
        });

        $("#gameplay").append(endRoundButton);
    }
    
    function showError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }



    class Player {
        constructor(player, domId) {
            this.name = player.Name;
            this.cardScore = player.CardScore;
            this.cards = player.Cards;
            this.domId = domId;
        }

        reloadCards(data) {
            this.cardScore = data.CardScore;
            this.cards = data.Cards;
        }

        show() {
            var text = `<p>Name: ${this.name}</p>`;
            text = text + `<p>CardScore: ${this.cardScore}</p>`;
            text = text + `<p>Cards:</p><ul>`;

            $.each(this.cards, function (i, item) {
                text = text + `<li>${item}</li>`;
            });

            text = text + `</ul>`;

            var domId = `#${this.domId}`;
            $(domId).text("");
            $(domId).append(text);
        }
    }

    class BotList {
        constructor(bots) {
            this.bots = [];
            
            for (var iterator = 0; iterator < bots.length; iterator++) {
                this.bots[iterator] = new Player(bots[iterator], `bot${iterator}`);
            }
        }

        reloadCards(bots) {
            for (var iterator = 0; iterator < bots.length; iterator++) {
                this.bots[iterator].reloadCards(bots[iterator]);
            }
        }

        show() {
            $("#bots").text("");

            this.bots.forEach(function (bot, iterator, bots) {
                var botPageItem = $('<div/>', {
                    id: bot.domId,
                    class: "col-lg-2 col-md-4 col-sm-4 col-xs-6 well"
                });
                $("#bots").append(botPageItem);

                bot.show();
            });
        }
    }
});


