$(document).ready(function () {

    $(window).load(function () {
        var stage = $("#stage").val();

        if (stage == 0) {
            return;
        }

        if (stage == 1) {
            var gameId = $("#gameid").val();
            var transParam = { inGameId: gameId };

            $.ajax({
                type: "POST",
                url: "/CardAndCheck/FirstPhaseGamePlay",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    if (response.Message != "SUCCESS") {
                        alert(response.Message);
                    }

                    if (response.Message == "SUCCESS") {
                        $("#gameplay").text("");

                        if ((!response.HumanBjAndDealerBjDanger) && (!response.CanHumanTakeOneMoreCard)) {
                            SecondPhase();
                        }

                        ReloadPlayers();
                        ReloadDealerInFirstPhase();
                        if (response.HumanBjAndDealerBjDanger) {
                            DrowButtonsHumanBjAndDealerBjDanger();
                        }

                        if (response.CanHumanTakeOneMoreCard) {
                            DrowButtonsCanHumanTakeOneMoreCard();
                        }

                        document.getElementById('stage').value = 0;
                    }
                },
                error: function (response) {
                    ShowError(response);
                }
            });
        }

        if (stage == 2) {
            var gameId = $("#gameid").val();
            var transParam = { inGameId: gameId };

            $.ajax({
                type: "POST",
                url: "/Api/HumanRoundResult",
                data: transParam,
                dataType: "json",
                success: function (response) {
                    if (response.Message != "SUCCESS") {
                        alert(response.Message);
                    }

                    if (response.Message == "SUCCESS") {
                        $("#gameplay").text("");
                        $("#gameplay").append(`<p>${response.HumanRoundResult}</p>`)
                        ReloadPlayers();
                        ReloadDealerInSecondPhase();
                        DrowButtonsEndRound();
                        document.getElementById('stage').value = 0;
                    }
                },
                error: function (response) {
                    ShowError(response);
                }
            });
        }
    });

    $("#betbutton").click(function () {
        var betInput = $("#betinput").val();
        var gameId = $("#gameid").val();
        var humanId = $("#humangameplayerid").val();
        var transParam = { bet: betInput, humanGamePlayerId: humanId, inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/Api/BetsCreation",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    var gameId = $("#gameid").val();
                    var transParam = { inGameId: gameId };

                    $.ajax({
                        type: "POST",
                        url: "/CardAndCheck/RoundStart",
                        data: transParam,
                        dataType: "json",
                        success: function (response) {
                            if (response.Message != "SUCCESS") {
                                alert(response.Message);
                            }

                            if (response.Message == "SUCCESS") {
                                var gameId = $("#gameid").val();
                                var transParam = { inGameId: gameId };

                                $.ajax({
                                    type: "POST",
                                    url: "/CardAndCheck/FirstPhaseGamePlay",
                                    data: transParam,
                                    dataType: "json",
                                    success: function (response) {
                                        if (response.Message != "SUCCESS") {
                                            alert(response.Message);
                                        }

                                        if (response.Message == "SUCCESS") {
                                            $("#gameplay").text("");

                                            if ((!response.HumanBjAndDealerBjDanger) && (!response.CanHumanTakeOneMoreCard)) {
                                                SecondPhase();
                                            }

                                            ReloadPlayers();
                                            ReloadDealerInFirstPhase();
                                            if (response.HumanBjAndDealerBjDanger) {
                                                DrowButtonsHumanBjAndDealerBjDanger();
                                            }

                                            if (response.CanHumanTakeOneMoreCard) {
                                                DrowButtonsCanHumanTakeOneMoreCard();
                                            }
                                        }
                                    },
                                    error: function (response) {
                                        ShowError(response);
                                    }
                                });
                            }
                        },
                        error: function (response) {
                            ShowError(response);
                        }
                    });
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    });
    
    function OnContinueRound () {
        var gameId = $("#gameid").val();
        var transParam = { inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/CardAndCheck/HumanBjAndDealerBjDangerContinueRound",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    SecondPhase();
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function OnTakeOneMoreCard () {
        var gameId = $("#gameid").val();
        var transParam = { inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/CardAndCheck/AddOneMoreCardToHuman",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    if (response.CanHumanTakeOneMoreCard) {
                        var humanId = $("#humangameplayerid").val();
                        ReloadPlayer(humanId, "#humangameplay");
                    }

                    if (!response.CanHumanTakeOneMoreCard) {
                        SecondPhase();
                    }
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }
    
    function OnEndRound() {
        var gameId = $("#gameid").val();
        var transParam = { inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/Api/UpdateGamePlayersForNewRound",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    if (response.IsGameOver != "") {
                        $("#gameplay").text("");
                        $("#gameplay").append(`<p>${response.IsGameOver}</p>`);
                        DrowButtonsNewGame();
                    }

                    if (response.IsGameOver == "") {
                        location.reload();
                    }
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function ShowError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }

    function ReloadPlayers() {
        var humanId = $("#humangameplayerid").val();
        ReloadPlayer(humanId, "#humangameplay");

        if ($("#botgameplayerid1").length) {
            var bot1Id = $("#botgameplayerid1").val();
            ReloadPlayer(bot1Id, "#botgameplay1");
        }

        if ($("#botgameplayerid2").length) {
            var bot2Id = $("#botgameplayerid2").val();
            ReloadPlayer(bot2Id, "#botgameplay2");
        }

        if ($("#botgameplayerid3").length) {
            var bot3Id = $("#botgameplayerid3").val();
            ReloadPlayer(bot3Id, "#botgameplay3");
        }

        if ($("#botgameplayerid4").length) {
            var bot4Id = $("#botgameplayerid4").val();
            ReloadPlayer(bot4Id, "#botgameplay4");
        }

        if ($("#botgameplayerid5").length) {
            var bot5Id = $("#botgameplayerid5").val();
            ReloadPlayer(bot5Id, "#botgameplay5");
        }
    }

    function ReloadPlayer(gamePlayerId, gamePlay) {
        var transParam = { gamePlayerId: gamePlayerId };

        $.ajax({
            type: "GET",
            url: "/Api/GetPlayer",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    var text = GetPlayerText(response.GamePlayer);
                    $(gamePlay).text("");
                    $(gamePlay).append(text);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function GetPlayerText(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>Bet: ${player.Bet}</p>`;
        text = text + `<p>RoundScore: ${player.CardScore}</p>`;
        text = text + `<p>Cards:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;
        return text;
    }

    function ReloadDealerInFirstPhase() {
        var gamePlayerId = $("#dealergameplayerid").val();
        var transParam = { gamePlayerId: gamePlayerId };

        $.ajax({
            type: "GET",
            url: "/Api/GetDealerInFirstPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    var text = GetDealerInFirstPhaseText(response.GamePlayer);
                    $("#dealergameplay").text("");
                    $("#dealergameplay").append(text);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function GetDealerInFirstPhaseText(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>Card:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;
        return text;
    }

    function ReloadDealerInSecondPhase() {
        var gamePlayerId = $("#dealergameplayerid").val();
        var transParam = { gamePlayerId: gamePlayerId };

        $.ajax({
            type: "GET",
            url: "/Api/GetDealerInSecondPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    var text = GetDealerInSecondPhaseText(response.GamePlayer);
                    $("#dealergameplay").text("");
                    $("#dealergameplay").append(text);
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }

    function GetDealerInSecondPhaseText(player) {
        var text = `<p>Score: ${player.Score}</p>`;
        text = text + `<p>RoundScore: ${player.CardScore}</p>`;
        text = text + `<p>Cards:</p><ul>`;

        $.each(player.Cards, function (i, item) {
            text = text + `<li>${item}</li>`;
        });

        text = text + `</ul>`;
        return text;
    }

    function DrowButtonsHumanBjAndDealerBjDanger() {
        $("#gameplay").append("<p>You have BlackJack and dealer has BlackJack-danger</p>");

        var continueRoundButton = $('<input/>', {
            type: "button",
            id: "continueround",
            value: "Continue round",
            class: "btn btn-primary",
            click: function () {
                OnContinueRound();
            }
        });

        var takeAwardButton = $('<input/>', {
            type: "button",
            id: "takeaward",
            value: "Take award (1:1)",
            class: "btn btn-primary",
            click: function () {
                SecondPhase();
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
                SecondPhase();
            }
        });

        $("#gameplay").append(takeOneMoreCardButton);
        $("#gameplay").append(dontTakeButton);
    }

    function DrowButtonsStartNewRound() {
        var startNewRoundButton = $('<input/>', {
            type: "button",
            id: "startnewround",
            value: "Start new round",
            class: "btn btn-primary",
            click: function () {
                OnStartNewRound();
            }
        });

        $("#gameplay").append(startNewRoundButton);
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

    function DrowButtonsNewGame() {
        var userName = $("#userName").val();
        var newGameButton = $('<input/>', {
            type: "button",
            id: "newgamebutton",
            value: "New game",
            class: "btn btn-primary",
            click: function () {
                window.location.href = `/StartGame/MainPage?userName=${userName}`;
            }
        });

        $("#gameplay").append(newGameButton);
    }

    function SecondPhase() {
        var gameId = $("#gameid").val();
        var transParam = { inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/CardAndCheck/SecondPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                if (response.Message != "SUCCESS") {
                    alert(response.Message);
                }

                if (response.Message == "SUCCESS") {
                    var gameId = $("#gameid").val();
                    var transParam = { inGameId: gameId };
                    $.ajax({
                        type: "POST",
                        url: "/Api/HumanRoundResult",
                        data: transParam,
                        dataType: "json",
                        success: function (response) {
                            if (response.Message != "SUCCESS") {
                                alert(response.Message);
                            }

                            if (response.Message == "SUCCESS") {
                                $("#gameplay").text("");
                                $("#gameplay").append(`<p>${response.HumanRoundResult}</p>`)
                                ReloadPlayers();
                                ReloadDealerInSecondPhase();
                                DrowButtonsEndRound();
                            }
                        },
                        error: function (response) {
                            ShowError(response);
                        }
                    });
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    }
});