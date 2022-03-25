import { NgModule } from '@angular/core';
import { LobbyRoutingModule } from './lobby-routing.module';
import { LobbyPageComponent } from './pages/lobby-page/lobby-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { JoinFormComponent } from './components/join-form/join-form.component';
import { PublicRoomListComponent } from './components/public-room-list/public-room-list.component';
import { LobbyButtonGroupComponent } from './components/lobby-button-group/lobby-button-group.component';
import { LobbyService } from './services/lobby.service';
import { MaterialModule } from 'src/app/core/modules/material/material.module';

@NgModule({
  declarations: [
    LobbyPageComponent,
    JoinFormComponent,
    PublicRoomListComponent,
    LobbyButtonGroupComponent,
  ],
  imports: [SharedModule, LobbyRoutingModule, MaterialModule],
  providers: [LobbyService],
})
export class LobbyModule {}
