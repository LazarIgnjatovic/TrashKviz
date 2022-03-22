import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { HttpGeneralService } from 'src/app/core/services/http-general/http-general.service';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {}

  onLogin(formGroup: FormGroup): void {
    this.authService
      .login(formGroup.value)
      .subscribe((_) => this.router.navigate(['/lobby']));
  }
}
