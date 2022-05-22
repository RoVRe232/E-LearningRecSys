import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { HttpService } from '../../shared/services/http.service';
import { AuthenticatedUserModel } from '../models/authenticated-user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private httpService: HttpService) {}

  getAuthToken(email: string, password: string) {
    const request = new BackApiHttpRequest(
      'api/sessions/login',
      {},
      JSON.stringify({ email, password }),
      new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    );

    return this.httpService.post(request).pipe(take(1));
  }

  getRefreshToken(account: AuthenticatedUserModel) {
    const refreshTokenReq = new BackApiHttpRequest(
      'api/sessions/refreshToken',
      {},
      JSON.stringify(account),
    );
    return this.httpService.post(refreshTokenReq).pipe(take(1));
  }
}
