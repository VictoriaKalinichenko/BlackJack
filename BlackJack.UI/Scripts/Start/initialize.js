$(document).ready( function() {

    $(window).load( function() {
        var isNewGame = $("#isnewgame").val();

        if (isNewGame) {
            startRound();
        }

        if (!isNewGame) {
            restoreRound();
        }
    });

    function restoreRound() {
        var roundResult = $("#roundresult").val();
        var gameId = $("#gameid").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/Restore",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (roundResult != "") {
                    response.RoundResult = roundResult;
                    reloadPageForContinueRound(response);
                }

                if (!response.CanTakeCard) {
                    continueRound();
                }

                if (response.CanTakeCard && (roundResult == "")) {
                    reloadPageForStartRound(response);
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
                if ( !response.CanTakeCard ) {
                    continueRound();
                }

                if ( response.CanTakeCard ) {
                    reloadPageForStartRound(response);
                }
            },
            error: function (response) {
                showError(response);
            }
        });
    }
    
    function onTakeCard () {
        var gameId = $("#gameid").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/AddCard",
            data: transParam,
            dataType: "json",
            success: function(response) {
                if (response.CanTakeCard) {
                    reloadPlayer(response, "#human");
                }

                if (!response.CanTakeCard) {
                    continueRound();
                }
            },
            error: function(response) {
                showError(response);
            }
        });
    }

    function continueRound() {
        var gameId = $("#gameid").val();
        var transParam = {
            GameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/Continue",
            data: transParam,
            dataType: "json",
            success: function (response) {
                reloadPageForContinueRound(response);
            },
            error: function (response) {
                showError(response);
            }
        });
    }
    
    function reloadPageForStartRound(startRoundView) {
        $("#gameplay").text("");
        
        reloadPlayer(startRoundView.Human, "#human");
        reloadPlayer(startRoundView.Dealer, "#dealer");
        reloadBots(startRoundView.Bots);

        drowButtonsForTakeCard();
    }

    function reloadPageForContinueRound(continueRoundView) {
        $("#gameplay").text("");
        $("#gameplay").append(`<p>${continueRoundView.RoundResult}</p>`);

        reloadPlayer(continueRoundView.Human, "#human");
        reloadPlayer(continueRoundView.Dealer, "#dealer");
        reloadBots(continueRoundView.Bots);

        drowButtonsEndRound();
    }
    
    function drowButtonsForTakeCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeonemorecard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function () {
                onTakeCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "donttake",
            value: "Don\'t take",
            class: "btn btn-default",
            click: function () {
                continueRound();
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

    function reloadBots(bots) {
        $("#bots").text("");

        bots.forEach(function (bot, iterator, bots) {
            var botPageItem = $('<div/>', {
                id: "bot" + iterator,
                class: "col-lg-2 col-md-4 col-sm-4 col-xs-6 well"
            });
            $("#bots").append(botPageItem);
            
            reloadPlayer(bot, "#bot" + iterator);
        });
    }

    function reloadPlayer(player, gamePlay) {
        var text = `<p>Name: ${player.Name}</p>`;
        text = text + `<p>Cards:</p><ul>`;

        $.each(player.Cards, function(i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;

        $(gamePlay).text("");
        $(gamePlay).append(text);
    }
   
    function showError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }
});