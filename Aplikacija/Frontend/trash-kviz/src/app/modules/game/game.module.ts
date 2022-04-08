import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule } from './game-routing.module';
import { GamePageComponent } from './pages/game-page/game-page.component';
import { GameUserComponent } from './components/game-user/game-user.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/core/modules/material/material.module';
import { AssociationGameComponent } from './components/association-game/association-game.component';
import { MultipleChoiceColumnComponent } from './components/association-column/association-column.component';
import { GameService } from './services/game-service/game.service';
import { FormsModule } from '@angular/forms';
import { StepByStepGameComponent } from './components/step-by-step-game/step-by-step-game.component';
import { ColumnComponent } from './components/column/column.component';
import { BaseGameComponent } from './components/base-game/base-game.component';
import { InfoComponent } from './components/info/info.component';
import { QuestionPanelComponent } from './components/question-panel/question-panel.component';
import { MultipleChoiceGameComponent } from './components/multiple-choice-game/multiple-choice-game.component';
import { ClosestNumberGameComponent } from './components/closest-number-game/closest-number-game.component';
import { GameEndComponent } from './components/game-end/game-end.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  providers: [GameService],
  declarations: [
    GamePageComponent,
    GameUserComponent,
    AssociationGameComponent,
    MultipleChoiceColumnComponent,
    StepByStepGameComponent,
    ColumnComponent,
    InfoComponent,
    QuestionPanelComponent,
    MultipleChoiceGameComponent,
    ClosestNumberGameComponent,
    GameEndComponent,
  ],
  imports: [
    CommonModule,
    GameRoutingModule,
    SharedModule,
    MaterialModule,
    FormsModule,
  ],
})
export class GameModule {}
