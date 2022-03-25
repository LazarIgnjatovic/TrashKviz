import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoomRoutingModule } from './room-routing.module';
import { RoomPageComponent } from './pages/room-page/room-page.component';
import { RoomUserListComponent } from './components/room-user-list/room-user-list.component';
import { RoomUserComponent } from './components/room-user/room-user.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MaterialModule } from 'src/app/core/modules/material/material.module';
import { RoomSettingsFormComponent } from './components/room-settings-form/room-settings-form.component';
import { RoomService } from './service/room-service/room.service';

@NgModule({
  providers: [RoomService],
  declarations: [
    RoomPageComponent,
    RoomUserListComponent,
    RoomUserComponent,
    RoomSettingsFormComponent,
  ],
  imports: [CommonModule, RoomRoutingModule, SharedModule, MaterialModule],
})
export class RoomModule {}
