import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from '../../services/auth/auth.service';

@Injectable()
export class AuthGuard implements CanLoad {
  constructor(private authService: AuthService, private router: Router) {}
  canLoad(
    route: Route,
    segments: UrlSegment[]
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    const userLoggedIn$: Observable<boolean> = this.authService
      .isLoggedIn()
      .pipe(
        tap((userLoggedIn) => {
          if (!userLoggedIn) this.router.navigate(['/users']);
        })
      );

    return userLoggedIn$;
  }
}
