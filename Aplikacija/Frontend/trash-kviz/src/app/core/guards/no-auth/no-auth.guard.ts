import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthService } from '../../services/auth/auth.service';

@Injectable()
export class NoAuthGuard implements CanLoad {
  constructor(private authService: AuthService, private router: Router) {}
  canLoad(
    route: Route,
    segments: UrlSegment[]
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    const userNotLoggedIn$ = this.authService.isLoggedIn().pipe(
      tap((userLoggedIn) => {
        if (userLoggedIn) this.router.navigate(['/lobby']);
      }),
      map((userLoggedIn: boolean) => !userLoggedIn)
    );

    return userNotLoggedIn$;
  }
}
