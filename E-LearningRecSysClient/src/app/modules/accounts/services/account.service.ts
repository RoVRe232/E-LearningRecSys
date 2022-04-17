import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize, take } from 'rxjs/operators';

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
  private refreshTokenTimeout: unknown;

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
            // this.startRefreshTokenTimer();
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
        new Date(account.authToken.expirationDate) <= new Date(Date.now())
      ) {
        if (
          account.refreshToken &&
          new Date(account.refreshToken?.expirationDate) > new Date(Date.now())
        ) {
          this.refreshToken();
        } else {
          this.accountSubject.next(this.defaultAccount);
          localStorage.removeItem('user');
        }
      }
      console.log(account);
      this.accountSubject.next(account);
    }
  }

  private startRefreshTokenTimer() {
    if (this.accountValue.authToken) {
      const token = JSON.parse(
        atob(this.accountValue.authToken.token.split('.')[1]),
      );

      const expires = new Date(token.exp * 1000);
      const timeout = expires.getTime() - Date.now() - 60 * 1000;
      this.refreshTokenTimeout = setTimeout(() => this.refreshToken(), timeout);
    }
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.refreshTokenTimeout as NodeJS.Timeout);
  }

  // register(account: AuthenticatedUserModel) {
  //   return this.http.post(`${baseUrl}/register`, account);
  // }

  // verifyEmail(token: string) {
  //   return this.http.post(`${baseUrl}/verify-email`, { token });
  // }

  // forgotPassword(email: string) {
  //   return this.http.post(`${baseUrl}/forgot-password`, { email });
  // }

  // validateResetToken(token: string) {
  //   return this.http.post(`${baseUrl}/validate-reset-token`, { token });
  // }

  // resetPassword(token: string, password: string, confirmPassword: string) {
  //   return this.http.post(`${baseUrl}/reset-password`, {
  //     token,
  //     password,
  //     confirmPassword,
  //   });
  // }

  // getAll() {
  //   return this.http.get<AuthenticatedUserModel[]>(baseUrl);
  // }

  // getById(id: string) {
  //   return this.http.get<AuthenticatedUserModel>(`${baseUrl}/${id}`);
  // }

  // create(params: any) {
  //   return this.http.post(baseUrl, params);
  // }

  // update(id: string, params: any) {
  //   return this.http.put(`${baseUrl}/${id}`, params).pipe(
  //     map((account: any) => {
  //       // update the current account if it was updated
  //       if (account.id === this.accountValue.id) {
  //         // publish updated account to subscribers
  //         account = { ...this.accountValue, ...account };
  //         this.accountSubject.next(account);
  //       }
  //       return account;
  //     }),
  //   );
  // }

  // delete(id: string) {
  //   return this.http.delete(`${baseUrl}/${id}`).pipe(
  //     finalize(() => {
  //       // auto logout if the logged in account was deleted
  //       if (id === this.accountValue.id) this.logout();
  //     }),
  //   );
  // }

  // // helper methods

  // private refreshTokenTimeout: NodeJS.Timeout = new NodeJS.Timeout();
}
