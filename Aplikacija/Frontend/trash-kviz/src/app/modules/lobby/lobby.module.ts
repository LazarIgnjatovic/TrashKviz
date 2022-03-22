import { NgModule } from '@angular/core';
import { LobbyRoutingModule } from './lobby-routing.module';
import { LobbyPageComponent } from './pages/lobby-page/lobby-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { JoinFormComponent } from './components/join-form/join-form.component';
import { PublicRoomListComponent } from './components/public-room-list/public-room-list.component';
import { LobbyButtonGroupComponent } from './components/lobby-button-group/lobby-button-group.component';

@NgModule({
  declarations: [
    LobbyPageComponent,
    JoinFormComponent,
    PublicRoomListComponent,
    LobbyButtonGroupComponent,
  ],
  imports: [SharedModule, LobbyRoutingModule],
})
export class LobbyModule {}
