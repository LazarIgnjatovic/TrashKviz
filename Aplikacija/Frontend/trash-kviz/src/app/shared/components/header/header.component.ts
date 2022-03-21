import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MaterialComponentsConfigProviderService } from 'src/app/core/services/material-components-config-provider/material-components-config-provider.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private materialComponentsConfigProvider: MaterialComponentsConfigProviderService
  ) {}

  ngOnInit(): void {}

  onLogout(): void {}
}
