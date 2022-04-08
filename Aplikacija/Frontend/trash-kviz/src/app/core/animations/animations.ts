import {
  animate,
  keyframes,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

export const slideInOutWithFade = trigger('slideInOutWithFade', [
  transition('void => flyFromLeft', [
    animate(
      '1000ms ease-in',
      keyframes([
        style({ transform: 'translateX(-100%)', opacity: 0 }),
        style({ transform: 'translateX(0)', opacity: 1 }),
      ])
    ),
  ]),
  transition('void => flyFromRight', [
    animate(
      '1000ms ease-in',
      keyframes([
        style({ transform: 'translateX(100%)', opacity: 0 }),
        style({ transform: 'translateX(0)', opacity: 1 }),
      ])
    ),
  ]),
]);

