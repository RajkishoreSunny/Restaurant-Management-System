import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { pipe, take } from 'rxjs';
import { map } from 'rxjs/operators';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    const login = inject(LoginService);
    const router = inject(Router);

    return login.isManagerLoggedIn$.pipe(
      take(1),
      map(isLoggedIn => {
        if(isLoggedIn) {
          return true;
        } else {
          router.navigate(['mlogin']);
          return false;
        }
      })
    )
};
