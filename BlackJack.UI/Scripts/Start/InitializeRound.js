$(document).ready(function () {

    $(window).load(function () {
        var stage = $("#stage").val();

        if (stage == 0) {
            return;
        }

        if (stage == 1) {
            var gameId = $("#gameid").val();
            var transParam = { gameId: gameId };

            $.ajax({
                type: "GET",
                url: "/Round/ResumeAfterStart",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    StartRoundPageReloading(response);
                },
                error: function (response) {
                    ShowError(response);
                }
            });
        }

        if (stage == 2) {
            var gameId = $("#gameid").val();
            var transParam = { gameId: gameId };

            $.ajax({
                type: "GET",
                url: "/Round/ResumeAfterContinue",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    ContinueRoundPageReloading(response);
                },
                error: function (response) {
                    ShowError(response);
                }
            });
        }
    });

    $("endround").click(function () {
        var userName = $("#userName").val();
        var gameId = $("#gameid").val();
        var result = $("#gameresult").text();
        var transParam = { result: result, gameId: gameId };

        $.ajax({
            type: "POST",
            url: "/Round/EndGame",
            data: transParam,
            dataType: "json",
            success: function () {
                window.location.href = `/Start/AuthorizePlayer?userName=${userName}`;
            },
            error: function (response) {
                ShowError(response);
            }
        });
    });

    $("#betbutton").click(function () {
        var betInput = $("#betinput").val();
        var humanScore = $("#humanscore").val();
        var validationMessage = "";

        if (betInput <= 0) {
            validationMessage = "Bet must be more than 0";
        }

        if (betInput > humanScore) {
            validationMessage = "Bet must be less than or equals to your score";
        }

        if (validationMessage != "") {
            alert(validationMessage);
            return;
        } 

        var gameId = $("#gameid").val();
        var humanId = $("#humangameplayerid").val();
        var transParam = { bet: betInput, gamePlayerId: humanId, gameId: gameId };
        $.ajax({
            type: "POST",
            url: "/Round/Start",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != null) {
                    alert(response.Message);
                }

                if (response.Message == null) {
                    StartRoundPageReloading(response.Data);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    });
    
    function OnTakeCard () {
        var gameId = $("#gameid").val();
        var transParam = { gameId: gameId };
        $.ajax({
            type: "GET",
            url: "/Round/AddCard",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.CanTakeCard) {
                    ReloadPlayer(response, "#humangameplay");
                }

                if (!response.CanTakeCard) {
                    ContinueRound(false);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }
    
    function OnEndRound() {
        var gameId = $("#gameid").val();
        var transParam = { gameId: gameId };
        $.ajax({
            type: "GET",
            url: "/Round/End",
            data: transParam,
            dataType: "json",
            success: function () {
                location.reload();
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }
    
    function DrowButtonsBlackJackChoice() {
        $("#gameplay").append("<p>You have BlackJack and dealer has BlackJack-danger</p>");

        var continueRoundButton = $('<input/>', {
            type: "button",
            id: "continueround",
            value: "Continue round",
            class: "btn btn-primary",
            click: function () {
                ContinueRound(true);
            }
        });

        var takeAwardButton = $('<input/>', {
            type: "button",
            id: "takeaward",
            value: "Take award (1:1)",
            class: "btn btn-primary",
            click: function () {
                ContinueRound(false);
            }
        });

        $("#gameplay").append(continueRoundButton);
        $("#gameplay").append(takeAwardButton);
    }

    function DrowButtonsTakeCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeonemorecard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function () {
                OnTakeCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "donttake",
            value: "Don\'t take",
            class: "btn btn-default",
            click: function () {
                ContinueRound(false);
            }
        });

        $("#gameplay").append(takeOneMoreCardButton);
        $("#gameplay").append(dontTakeButton);
    }

    function DrowButtonsEndRound() {
        var endRoundButton = $('<input/>', {
            type: "button",
            id: "endroundbutton",
            value: "End round",
            class: "btn btn-primary",
            click: function () {
                OnEndRound();
            }
        });

        $("#gameplay").append(endRoundButton);
    }

    function ContinueRound(continueRound) {
        var gameId = $("#gameid").val();
        var transParam = { GameId: gameId, ContinueRound: continueRound };
        $.ajax({
            type: "POST",
            url: "/Round/Continue",
            data: transParam,
            dataType: "json",
            success: function (response) {
                ContinueRoundPageReloading(response);
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function StartRoundPageReloading(data) {
        $("#gameplay").text("");

        if ((!data.BlackJackChoice) && (!data.CanTakeCard)) {
            ContinueRound(false);
        }

        ReloadPlayers(data);
        ReloadDealerInStartRound(data.Dealer);

        if (data.BlackJackChoice) {
            DrowButtonsBlackJackChoice();
        }

        if (data.CanTakeCard) {
            DrowButtonsTakeCard();
        }
    }

    function ContinueRoundPageReloading(data) {
        $("#gameplay").text("");
        $("#gameplay").append(`<p>${data.RoundResult}</p>`)
        ReloadPlayers(data);
        ReloadDealerInContinueRound(data.Dealer);
        DrowButtonsEndRound();
    }

    function ReloadPlayers(data) {
        ReloadPlayer(data.Human, "#humangameplay");

        if ($("#botgameplay1").length) {
            ReloadPlayer(data.Bots[0], "#botgameplay1");
        }

        if ($("#botgameplayerid2").length) {
            ReloadPlayer(data.Bots[1], "#botgameplay2");
        }

        if ($("#botgameplayerid3").length) {
            ReloadPlayer(data.Bots[2], "#botgameplay3");
        }

        if ($("#botgameplayerid4").length) {
            ReloadPlayer(data.Bots[3], "#botgameplay4");
        }

        if ($("#botgameplayerid5").length) {
            ReloadPlayer(data.Bots[4], "#botgameplay5");
        }
    }

    function ReloadPlayer(player, gamePlay) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>Bet: ${player.Bet}</p>`;
        text = text + `<p>RoundScore: ${player.RoundScore}</p>`;
        text = text + `<p>Cards:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;

        $(gamePlay).text("");
        $(gamePlay).append(text);
    }

    function ReloadDealerInStartRound(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>Card:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;

        $("#dealergameplay").text("");
        $("#dealergameplay").append(text);
    }

    function ReloadDealerInContinueRound(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>RoundScore: ${player.RoundScore}</p>`;
        text = text + `<p>Cards:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;

        $("#dealergameplay").text("");
        $("#dealergameplay").append(text);
    }
   
    function ShowError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }
});