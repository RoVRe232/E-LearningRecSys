import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { firstValueFrom, Observable, take } from 'rxjs';
import { AccountService } from '../../accounts/services/account.service';

@Injectable()
export class BackapiOutgoingInterceptor implements HttpInterceptor {
  constructor(
    private injector: Injector,
    private accountService: AccountService,
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    const account = this.accountService.accountValue; //this.injector.get(AccountService).accountValue;

    if (account.authToken?.token) {
      let token = account.authToken?.token;
      if (new Date(account.authToken?.expirationDate).getTime() < Date.now()) {
        if (account.refreshToken?.token) token = account.refreshToken?.token;
      }
      if (
        req.url.endsWith('api/sessions/refreshToken') &&
        account.refreshToken?.token
      )
        token = account.refreshToken?.token;
      const authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
      });

      return next.handle(authReq);
    }
    return next.handle(req);
  }
}
