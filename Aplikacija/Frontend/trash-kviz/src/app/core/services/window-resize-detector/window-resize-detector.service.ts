import { Injectable, OnDestroy } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { DeviceDetectorService } from 'ngx-device-detector';
import {
  BehaviorSubject,
  debounce,
  debounceTime,
  fromEvent,
  map,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';

@Injectable()
export class WindowResizeDetectorService implements OnDestroy {
  private _unsubscriber$: Subject<any> = new Subject();
  public screenOrientationCorrect$: BehaviorSubject<boolean>;
  public screenHeight$: BehaviorSubject<number>;
  public screenWidth$: BehaviorSubject<number>;
  private initialScreenWidth!: number;
  private initialScreenHeight!: number;
  private isKeyboardOpen: boolean = false;

  constructor(
    private meta: Meta,
    private deviceDetector: DeviceDetectorService
  ) {
    this.screenHeight$ = new BehaviorSubject<number>(window.innerHeight);
    this.screenWidth$ = new BehaviorSubject<number>(window.innerWidth);
    this.screenOrientationCorrect$ = new BehaviorSubject<boolean>(
      deviceDetector.isDesktop() ||
        window.screen.orientation.type == 'portrait-primary'
    );
    fromEvent(window, 'resize')
      .pipe(takeUntil(this._unsubscriber$))
      .subscribe((evt: any) => {
        if (!this.isKeyboardOpen) {
          this.nextWindowSizes();
        } else {
          meta.updateTag({
            name: 'viewport',
            content: `width=device-width, height=${this.initialScreenHeight}, initial-scale=1.0, user-scalable=no, maximum-scale=1.0`,
          });
        }
      });

    fromEvent(window, 'orientationchange')
      .pipe(
        map(
          (_) =>
            deviceDetector.isDesktop() ||
            window.screen.orientation.type == 'portrait-primary'
        )
      )
      .subscribe((_) => this.screenOrientationCorrect$.next(_));
  }

  ngOnDestroy() {
    this._unsubscriber$.next(null);
    this._unsubscriber$.complete();
  }

  nextWindowSizes() {
    this.screenHeight$.next(window.innerHeight);
    this.screenWidth$.next(window.innerWidth);
  }

  setInitialWindowSizes() {
    this.initialScreenHeight = window.innerHeight;
    this.initialScreenWidth = window.innerWidth;
  }

  setKeyboardOpen(isOpen: boolean) {
    this.isKeyboardOpen = isOpen;
  }
}
