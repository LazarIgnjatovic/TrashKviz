import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal, ComponentType } from '@angular/cdk/portal';
import { Injectable, InjectionToken, Injector } from '@angular/core';

export const COMPONENT_DATA = new InjectionToken<any>('COMPONENT_DATA');

@Injectable()
export class OverlayService {
  private overlayRef!: OverlayRef;
  overlayConfig: OverlayConfig;

  constructor(private overlay: Overlay) {
    this.overlayConfig = new OverlayConfig({
      positionStrategy: this.overlay
        .position()
        .global()
        .centerHorizontally()
        .centerVertically(),
      hasBackdrop: true,
      backdropClass: 'dark-backdrop',
    });
  }

  private createInjector(dataToPass: any) {
    return Injector.create({
      providers: [{ provide: COMPONENT_DATA, useValue: dataToPass }],
    });
  }

  show(componentToShow: ComponentType<any>, dataToInject: any = null) {
    this.overlayRef = this.overlay.create(this.overlayConfig);

    const overlayPortal = new ComponentPortal(
      componentToShow,
      null,
      this.createInjector(dataToInject)
    );

    this.overlayRef.attach(overlayPortal);
  }

  hide() {
    if (this.overlayRef) this.overlayRef.dispose();
  }
}
