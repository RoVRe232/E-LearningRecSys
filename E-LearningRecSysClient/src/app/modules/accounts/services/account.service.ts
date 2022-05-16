import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subscription, timer } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { AuthenticatedUserModel } from '../models/authenticated-user.model';
import { HttpService } from '../../shared/services/http.service';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private defaultAccount = new AuthenticatedUserModel(
    'DEFAULT',
    'DEFAULT',
    '',
    '',
    '',
    '',
  );
  private accountSubject: BehaviorSubject<AuthenticatedUserModel>;
  private refreshTokenTimeout!: Subscription;

  public account: Observable<AuthenticatedUserModel>;
  public isLoggedIn: Observable<boolean>;

  constructor(private router: Router, private httpService: HttpService) {
    this.accountSubject = new BehaviorSubject<AuthenticatedUserModel>(
      this.defaultAccount,
    );
    this.account = this.accountSubject.asObservable();
    this.isLoggedIn = this.accountSubject.asObservable().pipe(
      map((e) => {
        console.log(e);
        return e.accountId !== 'DEFAULT';
      }),
    );
    this.restoreSession();
  }

  public get accountValue(): AuthenticatedUserModel {
    return this.accountSubject.value;
  }

  login(email: string, password: string) {
    const request = new BackApiHttpRequest(
      'api/sessions/login',
      {},
      JSON.stringify({ email, password }),
      new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    );
    this.httpService
      .post(request)
      .pipe(take(1))
      .subscribe((response) => {
        if (response.errors?.length === 0 && response.result) {
          const account = response.result;
          this.accountSubject.next(response.result as AuthenticatedUserModel);
          localStorage.setItem('user', JSON.stringify(account));
          this.router.navigate(['/']);
          this.startRefreshTokenTimer();
        }
      });
  }

  logout() {
    this.accountSubject.next(this.defaultAccount);
    this.router.navigate(['/account/login']);
    localStorage.setItem('user', JSON.stringify(this.defaultAccount));
    this.stopRefreshTokenTimer();
  }

  refreshToken() {
    this.account.pipe(take(1)).subscribe((account) => {
      if (account !== this.defaultAccount) {
        const refreshTokenReq = new BackApiHttpRequest(
          'api/sessions/refreshToken',
          {},
          JSON.stringify(account),
        );
        this.httpService
          .post(refreshTokenReq)
          .pipe(take(1))
          .subscribe((response) => {
            this.accountSubject.next(JSON.parse(response.result));
            this.startRefreshTokenTimer();
          });
      }
    });
  }

  restoreSession() {
    if (localStorage.getItem('user')) {
      const account: AuthenticatedUserModel = JSON.parse(
        localStorage.getItem('user') || '',
      );
      if (
        account.authToken &&
        new Date(account.authToken.expirationDate).getTime() <= Date.now()
      ) {
        if (
          account.refreshToken &&
          new Date(account.refreshToken?.expirationDate).getTime() > Date.now()
        ) {
          this.refreshToken();
        } else {
          this.accountSubject.next(this.defaultAccount);
          localStorage.removeItem('user');
          return;
        }
      }
      if (account.userId) {
        this.accountSubject.next(account);
        this.startRefreshTokenTimer();
      }
    }
  }

  private startRefreshTokenTimer() {
    if (this.accountValue.authToken) {
      const token = JSON.parse(
        atob(this.accountValue.authToken.token.split('.')[1]),
      );

      const reloadInterval = new Date(token.exp).getTime() - Date.now() / 1000;
      if (reloadInterval < 10800) this.refreshToken();
      this.refreshTokenTimeout = timer(10800000, 10800000).subscribe((_) =>
        this.refreshToken(),
      );
    }
  }

  private stopRefreshTokenTimer() {
    this.refreshTokenTimeout.unsubscribe();
  }
}
