import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { delay, switchMap } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { Login } from '../../dtos/login.dto';

@Component({
  selector: 'app-create-user-page',
  templateUrl: './create-user-page.component.html',
  styleUrls: ['./create-user-page.component.scss'],
})
export class CreateUserPageComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}
  onCreateUser(formGroup: FormGroup): void {
    console.log(formGroup.value);
    this.authService
      .register(formGroup.value)
      .pipe(
        delay(1000),
        switchMap((_) => this.authService.login(formGroup.value))
      )
      .subscribe((_) => this.router.navigate(['/lobby']));
  }

  ngOnInit(): void {}
}
