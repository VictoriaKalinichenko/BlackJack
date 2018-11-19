$(document).ready(function () {
    var human;
    var dealer;
    var botList;
    
    $(window).load(function () {
        var isNewGame = $("#isNewGame").val();
        var isNewRound = (isNewGame == 'True');
        startRound(isNewRound);
    });

    function startRound(isNewRound) {
        var gameId = $("#gameId").val();
        var transParam = {
            GameId: gameId,
            IsNewRound: isNewRound
        };

        $.ajax({
            type: "POST",
            url: "/Round/Start",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadPlayersOnStart(response)
                reloadGamePlay(response.roundResult);
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
                reloadGamePlay(response.roundResult);
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function endRound() {
        var gameId = $("#gameId").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/End",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadDealerOnTakeCard(response.dealer);
                reloadGamePlay(response.roundResult);
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function reloadPlayersOnStart(response) {
        var humanName = $("#userName").val();
        human = new Player(response.human, humanName, "human");
        dealer = new Player(response.dealer, "Dealer", "dealer");
        botList = new BotList(response.bots);
        human.show();
        dealer.show();
        botList.show();
    }

    function reloadPlayersOnTakeCard(response) {
        human.reloadCards(response.human);
        dealer.reloadCards(response.dealer);
        botList.reloadCards(response.bots);
        human.show();
        dealer.show();
        botList.show();
    }

    function reloadDealerOnTakeCard(dealerInfo) {
        dealer.reloadCards(dealerInfo);
        dealer.show();
    }

    function reloadGamePlay(roundResult) {
        $("#gamePlay").text('');

        if (roundResult != "Round is in process") {
            $("#gamePlay").append(`<p>${roundResult}</p>`);
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

        $("#gamePlay").append(takeOneMoreCardButton);
        $("#gamePlay").append(dontTakeButton);
    }

    function drowButtonsEndRound() {
        var endRoundButton = $('<input/>', {
            type: "button",
            id: "endRound",
            value: "End round",
            class: "btn btn-primary",
            click: function () {
                startRound(true);
            }
        });

        $("#gamePlay").append(endRoundButton);
    }
    
    function showError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }

    class Player {
        constructor(cardsInformation, name, domId) {
            this.cardsInformation = cardsInformation;
            this.name = name;
            this.domId = domId;
        }

        reloadCards(data) {
            this.cardsInformation = data;
        }

        show() {
            var domId = `#${this.domId}`;
            $(domId).text("");
            $(domId).append(`<p>Name: ${this.name}</p>`);
            $(domId).append(`<p>CardScore: ${this.cardsInformation.cardScore}</p>`);
            $(domId).append(`<p>Cards:</p><ul>`);

            $.each(this.cardsInformation.cards, function (i, item) {
                $(domId).append(`<li>${item}</li>`);
            });

            $(domId).append(`</ul>`);
        }
    }

    class BotList {
        constructor(bots) {
            this.bots = [];
            
            for (var iterator = 0; iterator < bots.length; iterator++) {
                this.bots[iterator] = new Player(bots[iterator], `Bot${iterator}`, `bot${iterator}`);
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