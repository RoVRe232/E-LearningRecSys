import { HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { concatMap, filter, Subject, take, takeUntil } from 'rxjs';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { HttpService } from '../../shared/services/http.service';
import { AuthenticatedUserModel } from '../models/authenticated-user.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss'],
})
export class MyAccountComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();

  constructor(
    private fb: FormBuilder,
    private httpService: HttpService,
    private accountService: AccountService,
  ) {}

  myAccountData = this.fb.group({
    loginData: this.fb.group({
      email: this.fb.control({ value: '', disabled: true }),
      phone: this.fb.control('', [
        Validators.required,
        // Validators.pattern('^\\+?[0-9]{3}[0-9]{9}$|[0]{1}[0-9]{9}$'),
      ]),
    }),
    billingData: this.fb.group({
      firstName: this.fb.control('', [Validators.required]),
      lastName: this.fb.control('', [Validators.required]),
      addressLine1: this.fb.control('', [Validators.required]),
      addressLine2: this.fb.control(''),
      country: this.fb.control('', [Validators.required]),
      city: this.fb.control('', [Validators.required]),
      state: this.fb.control('', [Validators.required]),
      postalCode: this.fb.control(''),
    }),
    accountData: this.fb.group({
      accountName: this.fb.control('', [Validators.required]),
    }),
  });
  hidePassword = true;

  ngOnInit(): void {
    this.myAccountData.valueChanges.subscribe((e) => {
      const invalidControls = [];
      for (const control in this.loginData.controls) {
        if ((this.loginData.controls[control] as FormControl).invalid) {
          invalidControls.push(control);
        }
      }
    });

    this.accountService.isLoggedIn
      .pipe(
        takeUntil(this.ngUnsubscribe),
        filter((e) => e == true),
        concatMap((e) => {
          console.log(this.accountService.accountValue);
          const userDataRequest = new BackApiHttpRequest(
            'api/sessions/authenticatedUserDetails',
            {},
            '',
            new HttpHeaders({
              'Content-Type': 'application/json',
            }),
          );
          return this.httpService.get(userDataRequest);
        }),
        take(1),
      )
      .subscribe((e) => {
        const accountInfo = e.result;
        this.loginData.patchValue({
          email: accountInfo.email,
          phone: accountInfo.phone,
        });
        this.billingData.patchValue({
          firstName: accountInfo.firstName,
          lastName: accountInfo.lastName,
          addressLine1: accountInfo.addressLine1,
          addressLine2: accountInfo.addressLine2,
          country: accountInfo.country,
          city: accountInfo.city,
          state: accountInfo.state,
          postalCode: accountInfo.postalCode,
        });
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }

  getErrorMessage(control: FormControl) {
    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('invalid') ? 'Not a valid input' : '';
  }

  get loginData(): FormGroup {
    return this.myAccountData.get('loginData') as FormGroup;
  }

  get billingData(): FormGroup {
    return this.myAccountData.get('billingData') as FormGroup;
  }

  get accountData(): FormGroup {
    return this.myAccountData.get('accountData') as FormGroup;
  }

  get email(): FormControl {
    return this.loginData.controls['email'] as FormControl;
  }
  get firstName(): FormControl {
    return this.billingData.controls['firstName'] as FormControl;
  }
  get lastName(): FormControl {
    return this.billingData.controls['lastName'] as FormControl;
  }
  get accountName(): FormControl {
    return this.accountData.controls['accountName'] as FormControl;
  }
  get accountSignupInvitationToken(): FormControl {
    return this.accountData.controls[
      'accountSignupInvitationToken'
    ] as FormControl;
  }
  get phone(): FormControl {
    return this.loginData.controls['phone'] as FormControl;
  }
  get addressLine1(): FormControl {
    return this.billingData.controls['addressLine1'] as FormControl;
  }
  get addressLine2(): FormControl {
    return this.billingData.controls['addressLine2'] as FormControl;
  }
  get country(): FormControl {
    return this.billingData.controls['country'] as FormControl;
  }
  get city(): FormControl {
    return this.billingData.controls['city'] as FormControl;
  }
  get state(): FormControl {
    return this.billingData.controls['state'] as FormControl;
  }
  get postalCode(): FormControl {
    return this.billingData.controls['postalCode'] as FormControl;
  }
  onSubmit() {
    const request = new BackApiHttpRequest(
      'api/sessions/authenticatedUserDetails',
      {},
      {
        ...this.loginData.value,
        ...this.billingData.value,
        ...this.accountData.value,
      },
    );
    console.log(request);

    this.httpService
      .post(request)
      .pipe(take(1))
      .subscribe((e) => {
        console.log(e);
      });
  }
}
