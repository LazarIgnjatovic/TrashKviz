import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomPageComponent } from './pages/room-page/room-page.component';

const routes: Routes = [{ path: '', component: RoomPageComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RoomRoutingModule {}
