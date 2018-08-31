$(function () {
    $("#betbutton").click(function () {
        var betInput = $("#betinput").val();
        var gameId = $("#gameid").val();
        var transParam = { bet: betInput, inGameId: gameId };
        $.ajax({
            type: "POST",
            url: "/Game/RoundStart",
            data: transParam,
            dataType: "json",
            success: function (response) {
                $("#gameplay").text("");

                if ((!response.HumanBjAndDealerBjDanger) && (!response.CanHumanTakeOneMoreCard)) {
                    $("#gameplay").text("");
                }

                ReloadPlayersInFirstPhase();
                ReloadDealerInFirstPhase();
                if (response.HumanBjAndDealerBjDanger) {
                    DrowButtonsHumanBjAndDealerBjDanger();
                }

                if (response.CanHumanTakeOneMoreCard) {
                    DrowButtonsCanHumanTakeOneMoreCard();
                }
            },
            error: function (response) {
                ShowError(response);
            }
        });
    });

    function ShowError(response) {
        alert(`Status: ${response.status}, ${response.statusText}`);
    }

    function ReloadPlayersInFirstPhase() {
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
            url: "/Game/GetPlayer",
            data: transParam,
            dataType: "json",
            success: function (response) {
                var text = GetPlayerText(response);
                $(gamePlay).text("");
                $(gamePlay).append(text);
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
            url: "/Game/GetDealerInFirstPhase",
            data: transParam,
            dataType: "json",
            success: function (response) {
                var text = GetDealerInFirstPhaseText(response);
                $("#dealergameplay").text("");
                $("#dealergameplay").append(text);
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

    function DrowButtonsHumanBjAndDealerBjDanger() {
        $("#gameplay").append("<p>You have BlackJack and dealer has BlackJack-danger</p>");
        $("#gameplay").append('<input type="button" id="continueround" value="Continue round"/>');
        $("#gameplay").append('<input type="button" id="takeaward" value="Take award (1:1)"/>');
    }

    function DrowButtonsCanHumanTakeOneMoreCard() {
        $("#gameplay").append('<input type="button" id="takeonemorecard" value="Take one more card"/>');
    }

    function SecondRoundPhase() {

    }
});