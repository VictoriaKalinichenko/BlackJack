$(document).ready(function () {
    var human;
    var dealer;
    var botList;


    $(window).load(function () {
        restoreRound();
    });

    function restoreRound() {
        var gameId = $("#gameId").val();
        var isNewGame = $("#isNewGame").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/Restore",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (!isNewGame) {
                    reloadPlayersOnStart(response);
                    reloadGamePlay(response.RoundResult);
                }

                if (isNewGame) {
                    startRound();
                }
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function startRound() {
        var gameId = $("#gameId").val();
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
        var gameId = $("#gameId").val();
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
        var gameId = $("#gameId").val();
        var transParam = {
            GameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/End",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadDealerOnTakeCard(response.Dealer);
                $("#gameplay").text('');
                $("#gameplay").append(`<p>${response.RoundResult}</p>`);
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

    function reloadDealerOnTakeCard(dealerInfo) {
        dealer.reloadCards(dealerInfo);
        dealer.show();
    }

    function reloadGamePlay(roundResult) {
        $("#gameplay").text('');

        if (roundResult != "Round is in process") {
            $("#gameplay").append(`<p>${roundResult}</p>`);
            drowButtonsEndRound();
        }

        if (roundResult == "Round is in process") {
            drowButtonsForTakeCard();
        }
    }

    function drowButtonsForTakeCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeCard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function () {
                takeCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "dontTake",
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
            id: "endRound",
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