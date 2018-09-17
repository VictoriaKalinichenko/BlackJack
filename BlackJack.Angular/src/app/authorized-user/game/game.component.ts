import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { HttpService } from '../../services/http.service';
import { ErrorService } from '../../services/error.service';
import { deserialize } from 'json-typescript-mapper';
import { forEach } from '@angular/router/src/utils/collection';
import { GameViewModel } from '../../viewmodels/GameViewModel';
import { PlayerViewModel } from '../../viewmodels/PlayerViewModel';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    GameId: number;
    GameViewModel: GameViewModel;

    BetInput: boolean = false;
    TakeCard: boolean = false;
    BlackJackDangerChoice: boolean = false;
    EndRound: boolean = false;

    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _httpService: HttpService,
        private _errorService: ErrorService
    ) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.GameId = params['Id'];
            this.GetGame();
        });
    }

    GamePlayInitializer() {
        if (this.GameViewModel.Stage == 0) {
            this.GamePlayBetInput();
        }

        if (this.GameViewModel.Stage == 1) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerFirstPhaseUpdate();
            this.FirstPhaseGamePlay();
        }

        if (this.GameViewModel.Stage == 2) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerSecondPhaseUpdate();
            this.GamePlayEndRound();
        }
    }

    GetGame() {
        this._httpService.GetGame(this.GameId)
            .subscribe(
            (data) => {
                this.GameViewModel = deserialize(GameViewModel, data);
                this.GamePlayInitializer();
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    OnBetsCreation() {
        this.FirstPhase();
    }

    OnBlackJackDangerChoice(takeAward: boolean) {
        if (!takeAward) {
            this._httpService.BlackJackDangerContinueRound(this.GameId)
                .subscribe(
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
                );
        }

        this.SecondPhase();
    }

    OnTakingCard(takeCard: boolean) {
        if (takeCard) {
            this._httpService.AddOneMoreCardToHuman(this.GameId)
                .subscribe(
                (data) => {
                    this.HumanUpdate();
                    this.GamePlayInitializer();
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
                );
        }

        if (!takeCard) {
            this.SecondPhase();
        } 
    }

    FirstPhaseGamePlay() {
        this._httpService.FirstPhaseGamePlay(this.GameId)
            .subscribe(
            (data) => {
                this.HumanUpdate();
                this.BotsUpdate();
                this.DealerFirstPhaseUpdate();

                if (data["HumanBlackJackAndDealerBlackJackDanger"]) {
                    this.GamePlayBlackJackDangerChoice();
                }
                if (data["CanHumanTakeOneMoreCard"]) {
                    this.GamePlayTakeCard();
                }
                if (!(data["HumanBlackJackAndDealerBlackJackDanger"]) && !(data["CanHumanTakeOneMoreCard"])) {
                    this.SecondPhase();
                }
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    FirstPhase() {
        this._httpService.RoundStart(this.GameId)
            .subscribe(
            (data) => {
                this.HumanUpdate();
                this.BotsUpdate();
                this.DealerFirstPhaseUpdate();
                this.GameViewModel.Stage = 1;
                this.GamePlayInitializer();
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    SecondPhase() {
        this._httpService.SecondPhase(this.GameId)
            .subscribe(
            (data) => {
                this.HumanUpdate();
                this.BotsUpdate();
                this.DealerSecondPhaseUpdate();
                this.GameViewModel.Stage = 2;
                this.GamePlayInitializer();
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    HumanUpdate() {
        this._httpService.GetGamePlayer(this.GameViewModel.Human.GamePlayerId)
            .subscribe(
            (data) => {
                let name: string = this.GameViewModel.Human.Name;
                this.GameViewModel.Human = deserialize(PlayerViewModel, data);
                this.GameViewModel.Human.Name = name;
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    DealerFirstPhaseUpdate() {
        this._httpService.GetDealerFirstPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(
            (data) => {
                let name: string = this.GameViewModel.Dealer.Name;
                this.GameViewModel.Dealer = deserialize(PlayerViewModel, data);
                this.GameViewModel.Dealer.Name = name;
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    DealerSecondPhaseUpdate() {
        this._httpService.GetDealerSecondPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(
            (data) => {
                let name: string = this.GameViewModel.Dealer.Name;
                this.GameViewModel.Dealer = deserialize(PlayerViewModel, data);
                this.GameViewModel.Dealer.Name = name;
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    BotsUpdate() {
        this.GameViewModel.Bots.forEach(bot => {
            this._httpService.GetGamePlayer(bot.GamePlayerId)
                .subscribe(
                (data) => {
                    let inBot = deserialize(PlayerViewModel, data);
                    bot.Bet = inBot.Bet;
                    bot.Score = inBot.Score;
                    bot.RoundScore = inBot.RoundScore;
                    bot.Cards = inBot.Cards;
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
                );
        });
    }

    GamePlayBetInput() {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    }

    GamePlayTakeCard() {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    }

    GamePlayBlackJackDangerChoice() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
    }

    GamePlayEndRound() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
    }

    Reload() {
        this.GetGame();
    }
}
