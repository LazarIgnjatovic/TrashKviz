import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { OverlayService } from 'src/app/core/services/overlay-service/overlay.service';
import { WindowResizeDetectorService } from 'src/app/core/services/window-resize-detector/window-resize-detector.service';
import { OverlayMessageComponent } from '../overlay-message/overlay-message.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  private correctScreenOrientationSubscription!: Subscription;
  constructor(
    private resizeDetectorService: WindowResizeDetectorService,
    private overlay: OverlayService
  ) {}

  ngOnInit() {
    
    this.resizeDetectorService.setInitialWindowSizes();
    this.correctScreenOrientationSubscription =
      this.resizeDetectorService.screenOrientationCorrect$.subscribe(
        (correctOrientation) =>
          this.showWrongOrientationMessage(correctOrientation)
      );
  }
  ngOnDestroy(): void {
    this.correctScreenOrientationSubscription.unsubscribe();
  }

  private showWrongOrientationMessage(correctOrientation: boolean) {
    this.overlay.overlayConfig.backdropClass = 'full-dark-backdrop';
    if (!correctOrientation) this.overlay.show(OverlayMessageComponent);
    else this.overlay.hide();
  }
}
