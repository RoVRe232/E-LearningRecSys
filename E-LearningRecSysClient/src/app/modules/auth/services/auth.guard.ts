import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { AccountService } from '../../accounts/services/account.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private accountService: AccountService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    // if (this.accountService.accountValue.accountID == 'DEFAULT')
    //   this.accountService.restoreSession();
    if (this.accountService.accountValue.accountID == 'DEFAULT') {
      this.router.navigate(['/', 'accounts', 'login']);
      return false;
    }

    return true;
    // TODO check if route is restricted by role
    // if (route.data['roles'] && !route.data['roles'].includes(account.role)) {
    //     // role not authorized so redirect to home page
    //     this.router.navigate(['/']);
    //     return false;
    // }

    // authorized so return true
    // not logged in so redirect to login page with the return url
  }
}
