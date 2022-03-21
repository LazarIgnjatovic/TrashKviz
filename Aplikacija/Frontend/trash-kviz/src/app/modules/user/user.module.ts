import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from 'src/app/core/modules/material/material.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginFormComponent } from './components/login-form/login-form.component';
import { CreateUserPageComponent } from './pages/create-user-page/create-user-page.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { UserService } from './services/user.service';
import { UserRoutingModule } from './user-routing.module';
import { RegisterFormComponent } from './components/register-form/register-form.component';

@NgModule({
  declarations: [LoginPageComponent, CreateUserPageComponent, LoginFormComponent, RegisterFormComponent],
  imports: [CommonModule, UserRoutingModule, MaterialModule, SharedModule],
  providers: [UserService]
})
export class UserModule {}
