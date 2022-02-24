import { Injectable } from '@angular/core';

@Injectable()
export class UserService {
  private loggedIn = false;

  get isLoggedIn(): boolean {
    return this.loggedIn;
  }
}
