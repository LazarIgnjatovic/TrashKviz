import { Injectable, OnDestroy } from '@angular/core';
import {
  BehaviorSubject,
  debounceTime,
  fromEvent,
  Subject,
  takeUntil,
} from 'rxjs';

@Injectable()
export class WindowResizeDetectorService implements OnDestroy {
  private _unsubscriber$: Subject<any> = new Subject();
  public screenHeight$: BehaviorSubject<number>;

  constructor() {
    this.screenHeight$ = new BehaviorSubject<number>(window.innerHeight);
    fromEvent(window, 'resize')
      .pipe(debounceTime(500), takeUntil(this._unsubscriber$))
      .subscribe((evt: any) => this.screenHeight$.next(evt.target.innerHeight));
  }

  ngOnDestroy() {
    this._unsubscriber$.next(null);
    this._unsubscriber$.complete();
  }
}
