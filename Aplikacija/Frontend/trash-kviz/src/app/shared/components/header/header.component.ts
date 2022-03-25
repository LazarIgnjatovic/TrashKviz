import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private authService: AuthService,
    private router: Router,
    private signalRService: SignalrGeneralService
  ) {}

  ngOnInit(): void {}

  onLogout() {
    this.authService.logout().subscribe((_) => {
      this.signalRService
        .endConnection()
        .subscribe((_) => this.router.navigate(['/users']));
    });
  }
}
