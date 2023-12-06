import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  if (typeof localStorage !== 'undefined' && localStorage.getItem('token')) {
    return true;
  } else {
    const router = inject(Router);
    return router.navigate(['login']);
  }
};
