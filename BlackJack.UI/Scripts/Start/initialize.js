$(document).ready( function() {

    $(window).load( function() {
        var roundResult = $("#roundresult").val();
        var gameId = $("#gameid").val();
        var isNewGame = $("#isnewgame").val();
        var transParam = {
            gameId: gameId
        };

        if (isNewGame) {
            startRound(transParam);
        }

        if (stage == 1) {
            var gameId = $("#gameid").val();
            var transParam = {
                gameId: gameId
            };

            $.ajax({
                type: "GET",
                url: "/Round/ResumeAfterStart",
                data: transParam,
                dataType: "json",
                success: function(response) {
                    startRoundPageReloading(response);
                },
                error: function(response) {
                    showError(response);
                }
            });
        }

        if (stage == 2) {
            var gameId = $("#gameid").val();
            var transParam = {
                gameId: gameId
            };

            $.ajax({
                type: "GET",
                url: "/Round/ResumeAfterContinue",
                data: transParam,
                dataType: "json",
                success: function(response) {
                    continueRoundPageReloading(response);
                },
                error: function(response) {
                    showError(response);
                }
            });
        }
    });

    $("endround").click( function() {
        var userName = $("#userName").val();
        var gameId = $("#gameid").val();
        var result = $("#gameresult").text();
        var transParam = {
            result: result,
            gameId: gameId
        };

        $.ajax({
            type: "POST",
            url: "/Round/EndGame",
            data: transParam,
            dataType: "json",
            success: function() {
                window.location.href = `/Start/AuthorizePlayer?userName=${userName}`;
            },
            error: function(response) {
                showError(response);
            }
        });
    });



    function startRound(transParam) {
        $.ajax({
            type: "GET",
            url: "/Round/Start",
            data: transParam,
            dataType: "json",
            success: function (response) {
                loadPlayers(response.Human, response.Dealer, response.Bots);

                if (startRoundView.CanTakeCard) {

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
                    reloadPlayer(response, "#humangameplay");
                }

                if (!response.CanTakeCard) {
                    continueRound(false);
                }
            },
            error: function(response) {
                showError(response);
            }
        });
    }

    function continueRound(continueRound) {
        var gameId = $("#gameid").val();
        var transParam = {
            GameId: gameId,
            ContinueRound: continueRound
        };

        $.ajax({
            type: "POST",
            url: "/Round/Continue",
            data: transParam,
            dataType: "json",
            success: function (response) {
                continueRoundPageReloading(response);
            },
            error: function (response) {
                showError(response);
            }
        });
    }

    function onEndRound() {
        var gameId = $("#gameid").val();
        var transParam = {
            gameId: gameId
        };

        $.ajax({
            type: "GET",
            url: "/Round/End",
            data: transParam,
            dataType: "json",
            success: function() {
                location.reload();
            },
            error: function(response) {
                showError(response);
            }
        });
    }
    
    function drowButtonsBlackJackChoice() {
        $("#gameplay").append("<p>You have BlackJack and dealer has BlackJack-danger</p>");

        var continueRoundButton = $('<input/>', {
            type: "button",
            id: "continueround",
            value: "Continue round",
            class: "btn btn-primary",
            click: function() {
                continueRound(true);
            }
        });

        var takeAwardButton = $('<input/>', {
            type: "button",
            id: "takeaward",
            value: "Take award (1:1)",
            class: "btn btn-primary",
            click: function() {
                continueRound(false);
            }
        });

        $("#gameplay").append(continueRoundButton);
        $("#gameplay").append(takeAwardButton);
    }

    function drowButtonsTakeCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeonemorecard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function() {
                onTakeCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "donttake",
            value: "Don\'t take",
            class: "btn btn-default",
            click: function() {
                continueRound(false);
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
                onEndRound();
            }
        });

        $("#gameplay").append(endRoundButton);
    }    

    function startRoundPageReloading(startRoundView) {
        $("#gameplay").text("");

        if ( (!startRoundView.CanTakeCard) ) {
            continueRound();
        }

        reloadPlayer(startRoundView.Human, "#human");
        reloadPlayer(startRoundView.Dealer, "#dealer");
        reloadBots(startRoundView.Bots);
        
        if (startRoundView.CanTakeCard) {
            drowButtonsTakeCard();
        }
    }

    function continueRoundPageReloading(continueRoundView) {
        $("#gameplay").text("");
        $("#gameplay").append(`<p>${continueRoundView.RoundResult}</p>`);

        reloadPlayer(continueRoundView.Human, "#human");
        reloadPlayer(continueRoundView.Dealer, "#dealer");
        reloadBots(continueRoundView.Bots);

        drowButtonsEndRound();
    }

    function reloadBots(bots) {
        if ($("#botgameplay1").length) {
            reloadPlayer(data.Bots[0], "#botgameplay1");
        }

        if ($("#botgameplayerid2").length) {
            reloadPlayer(data.Bots[1], "#botgameplay2");
        }

        if ($("#botgameplayerid3").length) {
            reloadPlayer(data.Bots[2], "#botgameplay3");
        }

        if ($("#botgameplayerid4").length) {
            reloadPlayer(data.Bots[3], "#botgameplay4");
        }

        if ($("#botgameplayerid5").length) {
            reloadPlayer(data.Bots[4], "#botgameplay5");
        }
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