import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, filter, map, Subscription } from 'rxjs';
import { GameType } from '../../enums/game-type.enum';
import { Answer } from '../../models/answer.model';
import {
  AssociationState,
  isAssociationState,
} from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';
import { TurnBased } from '../../models/turn-based-model';
import { GameService } from '../../services/game-service/game.service';

@Component({
  selector: 'app-game-page',
  templateUrl: './game-page.component.html',
  styleUrls: ['./game-page.component.scss'],
})
export class GamePageComponent implements OnInit {
  constructor(private gameService: GameService) {}
  ngOnInit(): void {
    this.gameService.initConnection();
  }

  getGameState(gameType: GameType) {
    return this.gameService
      .getGameState()
      .pipe(filter((gameState) => gameState.type == gameType));
  }

  getGameStateBehavior() {
    return this.gameService.getGameState();
  }

  onSubmitAnswer<AnswerType extends Answer>(answer: AnswerType) {
    this.gameService.submitAnswer<AnswerType>(answer);
  }

  getOnPlayerTurn(playerId: number) {
    return this.gameService.getGameState().pipe(
      filter((gameState) => gameState.type == GameType.association),
      map((gameState) => (gameState as TurnBased).onTurn == playerId)
    );
  }

  getTurnBasedGame() {
    return this.gameService
      .getGameState()
      .pipe(map((gameState) => gameState.type == GameType.association));
  }

  getTurnTimer(playerId: number) {
    return this.gameService.getGameState().pipe(
      filter(
        (gameState) =>
          gameState.type == GameType.association &&
          (gameState as TurnBased).onTurn == playerId
      ),
      map((gameState) => (gameState as AssociationState).turnTimerValue)
    );
  }

  getOnTurn() {
    return this.gameService.getOnTurn();
  }

  getOnTimerTick() {
    return this.gameService.getTimerTick();
  }

  getGlobalTimer() {
    return this.gameService.getGlobalTimer();
  }
}
