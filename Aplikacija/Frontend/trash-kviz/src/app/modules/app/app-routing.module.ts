import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth/auth.guard';
import { NoAuthGuard } from 'src/app/core/guards/no-auth/no-auth.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/users/login',
    pathMatch: 'full',
  },
  {
    path: 'users',
    canLoad: [NoAuthGuard],
    loadChildren: () =>
      import('../user/user.module').then((module) => module.UserModule),
  },
  {
    path: 'lobby',
    canLoad: [AuthGuard],
    loadChildren: () =>
      import('../lobby/lobby.module').then((module) => module.LobbyModule),
  },
  {
    path: 'room',
    canLoad: [AuthGuard],
    loadChildren: () =>
      import('../room/room.module').then((module) => module.RoomModule),
  },
  {
    path: 'game',
    //canLoad: [AuthGuard],
    loadChildren: () =>
      import('../game/game.module').then((module) => module.GameModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
