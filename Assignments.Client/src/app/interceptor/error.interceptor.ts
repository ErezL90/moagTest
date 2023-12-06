import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error) => {
      if ([401, 403].includes(error.status)) {
        console.log('Unauthorized / Forbidden request');
      }

      return throwError(() => error);
    })
  );
};
