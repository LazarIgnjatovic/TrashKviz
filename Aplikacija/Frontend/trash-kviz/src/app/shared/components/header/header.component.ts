import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth/auth.service';
import { MaterialComponentsConfigProviderService } from 'src/app/core/services/material-components-config-provider/material-components-config-provider.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private materialComponentsConfigProvider: MaterialComponentsConfigProviderService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onLogout() {
    this.authService
      .logout()
      .subscribe((_) => this.router.navigate(['/users']));
  }
}
