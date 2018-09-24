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
                url: "/GameLogic/ResumeGameAfterRoundFirstPhase",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    RoundFirstPhaseReloading(response.Data);
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
                url: "/GameLogic/ResumeGameAfterRoundSecondPhase",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    RoundSecondPhaseReloading(response.Data);
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
        var transParam = { bet: betInput, gamePlayerId: humanId, inGameId: gameId };

        $.ajax({
            type: "POST",
            url: "/GameLogic/EndGame",
            data: transParam,
            dataType: "json",
            success: function () {
                window.location.href = `/StartGame/AuthorizePlayer?userName=${userName}`;
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
            url: "/GameLogic/DoRoundFirstPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "") {
                    alert(response.Message);
                }

                if (response.Message == "") {
                    RoundFirstPhaseReloading(response.Data);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    });
    
    function OnTakeOneMoreCard () {
        var gameId = $("#gameid").val();
        var transParam = { gameId: gameId };
        $.ajax({
            type: "GET",
            url: "/GameLogic/AddCardToHuman",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Data.CanHumanTakeOneMoreCard) {
                    ReloadPlayer(response.Data, "#humangameplay");
                }

                if (!response.Data.CanHumanTakeOneMoreCard) {
                    RoundSecondPhase(false);
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
            url: "/GameLogic/EndRound",
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
    
    function DrowButtonsHumanBlackJackAndDealerBlackJackDanger() {
        $("#gameplay").append("<p>You have BlackJack and dealer has BlackJack-danger</p>");

        var continueRoundButton = $('<input/>', {
            type: "button",
            id: "continueround",
            value: "Continue round",
            class: "btn btn-primary",
            click: function () {
                RoundSecondPhase(true);
            }
        });

        var takeAwardButton = $('<input/>', {
            type: "button",
            id: "takeaward",
            value: "Take award (1:1)",
            class: "btn btn-primary",
            click: function () {
                RoundSecondPhase(false);
            }
        });

        $("#gameplay").append(continueRoundButton);
        $("#gameplay").append(takeAwardButton);
    }

    function DrowButtonsCanHumanTakeOneMoreCard() {
        var takeOneMoreCardButton = $('<input/>', {
            type: "button",
            id: "takeonemorecard",
            value: "Take one more card",
            class: "btn btn-primary",
            click: function () {
                OnTakeOneMoreCard();
            }
        });

        var dontTakeButton = $('<input/>', {
            type: "button",
            id: "donttake",
            value: "Don\'t take",
            class: "btn btn-default",
            click: function () {
                RoundSecondPhase(false);
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

    function RoundSecondPhase(humanBlackJackAndDealerBlackJackDanger) {
        var gameId = $("#gameid").val();
        var transParam = { GameId: gameId, HumanBlackJackAndDealerBlackJackDanger: humanBlackJackAndDealerBlackJackDanger };
        $.ajax({
            type: "POST",
            url: "/GameLogic/DoRoundSecondPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                RoundSecondPhaseReloading(response.Data);
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function RoundFirstPhaseReloading(data) {
        $("#gameplay").text("");

        if ((!data.HumanBlackJackAndDealerBlackJackDanger) && (!data.CanHumanTakeOneMoreCard)) {
            RoundSecondPhase(false);
        }

        ReloadPlayers(data);
        ReloadDealerInFirstPhase(data.Dealer);

        if (data.HumanBlackJackAndDealerBlackJackDanger) {
            DrowButtonsHumanBlackJackAndDealerBlackJackDanger();
        }

        if (data.CanHumanTakeOneMoreCard) {
            DrowButtonsCanHumanTakeOneMoreCard();
        }
    }

    function RoundSecondPhaseReloading(data) {
        $("#gameplay").text("");
        $("#gameplay").append(`<p>${data.RoundResult}</p>`)
        ReloadPlayers(data);
        ReloadDealerInSecondPhase(data.Dealer);
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

    function ReloadDealerInFirstPhase(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>Card:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;

        $("#dealergameplay").text("");
        $("#dealergameplay").append(text);
    }

    function ReloadDealerInSecondPhase(player) {
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