import { HttpInterceptorFn } from '@angular/common/http';

export const loggerInterceptor: HttpInterceptorFn = (req, next) => {
  const localToken = localStorage.getItem('token');
  if (localToken) {
    req = req.clone({
      headers: req.headers.set('Authorization', 'bearer ' + localToken),
    });
  }

  return next(req);
};
