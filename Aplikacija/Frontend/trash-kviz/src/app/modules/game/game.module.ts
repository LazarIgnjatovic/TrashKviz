import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule } from './game-routing.module';
import { GamePageComponent } from './pages/game-page/game-page.component';
import { GameUserComponent } from './components/game-user/game-user.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/core/modules/material/material.module';

@NgModule({
  declarations: [GamePageComponent, GameUserComponent],
  imports: [CommonModule, GameRoutingModule, SharedModule, MaterialModule],
})
export class GameModule {}
