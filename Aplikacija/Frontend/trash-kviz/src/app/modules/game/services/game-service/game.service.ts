import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, filter, Subject, switchMap } from 'rxjs';
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
    new BehaviorSubject<GameState>({ type: -1, players: [] });
  private onTurn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  constructor(
    private signalRService: SignalrGeneralService,
    private router: Router
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
        methodName: 'OnTurn',
        args: [],
      },
      () => {
        console.log('OnTurn');
        this.onTurn.next(true);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'NotOnTurn',
        args: [],
      },
      () => {
        console.log('NotOnTurn');
        this.onTurn.next(false);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'MatchUpdate',
        args: [],
      },
      (gameState: GameState) => {
        if (gameState.type == GameType.info) {
          console.log(gameState);
        } else this.gameState.next(gameState);
        console.log(gameState);
      }
    );
  }

  getGameState() {
    return this.gameState;
  }

  getOnTurn() {
    return this.onTurn;
  }

  submitAnswer<AnswerType extends Answer>(answer: AnswerType) {
    console.log(answer);
    this.signalRService.sendMessageToServer({
      methodName: 'SubmitAnswer',
      args: [this.matchId, answer],
    });
  }
}
