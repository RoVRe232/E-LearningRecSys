import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { AuthenticatedUserModel } from '../../accounts/models/authenticated-user.model';
import { AccountService } from '../../accounts/services/account.service';

@Injectable()
export class BackapiOutgoingInterceptor implements HttpInterceptor {
  private activeAccount = new AuthenticatedUserModel(
    'DEFAULT',
    'DEFAULT',
    '',
    '',
    '',
    '',
  );
  private isLoggedIn = false;

  constructor(private accountService: AccountService) {
    accountService.account.pipe().subscribe((e) => (this.activeAccount = e));
    accountService.isLoggedIn.pipe().subscribe((e) => (this.isLoggedIn = e));
  }
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    console.log('interceptor!!');
    if (this.isLoggedIn && !!this.activeAccount.authToken?.token) {
      let token = this.activeAccount.authToken?.token;
      if (
        new Date(this.activeAccount.authToken?.expirationDate).getTime() <
        Date.now()
      ) {
        if (this.activeAccount.refreshToken?.token)
          token = this.activeAccount.refreshToken?.token;
      }
      if (
        req.url.endsWith('api/sessions/refreshToken') &&
        this.activeAccount.refreshToken?.token
      )
        token = this.activeAccount.refreshToken?.token;
      const authReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`),
      });

      return next.handle(authReq);
    }
    return next.handle(req);
  }
}
