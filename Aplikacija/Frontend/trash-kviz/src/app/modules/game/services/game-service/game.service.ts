import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, filter, Subject, switchMap } from 'rxjs';
import { OverlayService } from 'src/app/core/services/overlay-service/overlay.service';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';
import { GameType } from '../../enums/game-type.enum';
import { Answer } from '../../models/answer.model';
import { AssociationState } from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';
import { InfoState } from '../../models/info-state.model';

@Injectable()
export class GameService {
  private matchId: string = '';
  private gameState: BehaviorSubject<GameState> =
    new BehaviorSubject<GameState>({ type: -1, players: [], timerValue: 0 });
  private onTurn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  private globalTimer: BehaviorSubject<number> = new BehaviorSubject(0);
  private timerTick: BehaviorSubject<void> = new BehaviorSubject<void>(
    undefined
  );
  constructor(
    private signalRService: SignalrGeneralService,
    private router: Router,
    private overlay: OverlayService
  ) {}

  initConnection() {
    this.signalRService.createConnection('/match');

    this.addServerMethodHandlers();

    this.signalRService.startConnection();
  }

  private addServerMethodHandlers() {
    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'NoMatchFound',
        args: [],
      },
      () => {
        this.signalRService.endConnection().subscribe((_) => {
          this.matchId = '';
          this.router.navigate(['/lobby']);
        });
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'MatchFound',
        args: [],
      },
      (matchId: string) => {
        this.matchId = matchId;
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'Tick',
        args: [],
      },
      () => {
        this.timerTick.next();
        if (this.globalTimer.value > 0) {
          this.globalTimer.next(this.globalTimer.value - 1);
        }
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'OnTurn',
        args: [],
      },
      () => {
        this.onTurn.next(true);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'NotOnTurn',
        args: [],
      },
      () => {
        this.onTurn.next(false);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'MatchUpdate',
        args: [],
      },
      (gameState: GameState) => {
        console.log(gameState);
        this.gameState.next(gameState);
        this.globalTimer.next(gameState.timerValue);
      }
    );
  }

  getGameState() {
    return this.gameState;
  }

  getOnTurn() {
    return this.onTurn;
  }

  getTimerTick() {
    return this.timerTick;
  }

  getGlobalTimer() {
    return this.globalTimer;
  }

  submitAnswer<AnswerType extends Answer>(answer: AnswerType) {
    this.signalRService.sendMessageToServer({
      methodName: 'SubmitAnswer',
      args: [this.matchId, answer],
    });
  }
}
