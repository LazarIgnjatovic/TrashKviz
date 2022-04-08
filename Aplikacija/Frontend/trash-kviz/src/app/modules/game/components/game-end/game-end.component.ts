import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { slideInOutWithFade } from 'src/app/core/animations/animations';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';

import { Player } from '../../models/player.model';

@Component({
  selector: 'app-game-end',
  templateUrl: './game-end.component.html',
  styleUrls: ['./game-end.component.scss'],
  animations: [slideInOutWithFade],
})
export class GameEndComponent implements OnInit {
  @Input() standings: Player[] = [];
  constructor(
    private router: Router,
    private signalRService: SignalrGeneralService
  ) {}

  ngOnInit(): void {}

  onClick() {
    this.signalRService.endConnection().subscribe(() => {
      this.router.navigate(['/lobby']);
    });
  }
}
