import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { AuthenticatedUserModel } from '../../accounts/models/authenticated-user.model';
import { AccountService } from '../../accounts/services/account.service';
import { UserService } from '../../auth/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();

  public searchGroup = this.formBuilder.group({
    searchControl: [''],
  });

  public account: AuthenticatedUserModel = new AuthenticatedUserModel(
    'DEFAULT',
    '',
    '',
    '',
    '',
    'user',
  );

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    public accountService: AccountService,
  ) {
    accountService.account
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((e) => {
        this.account = e;
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }

  public get searchControl(): FormControl {
    return this.searchGroup.get('searchControl') as FormControl;
  }

  public logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }

  public searchAutocompleteOptions(
    currentInput: string,
  ): Observable<Array<string>> {
    return new Observable<Array<string>>((observer) => {
      if (currentInput) observer.next([currentInput]);
      else observer.next([]);
    });
  }
}
