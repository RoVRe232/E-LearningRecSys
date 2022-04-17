import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { take, takeUntil } from 'rxjs';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  public isLinear = true;

  constructor(private fb: FormBuilder, private httpService: HttpService) {}

  checkPasswords: ValidatorFn = (
    group: AbstractControl,
  ): ValidationErrors | null => {
    if (group != null) {
      const pass = group.parent?.get('password');
      const confirmPass = group.parent?.get('confirmPassword');
      if (pass && confirmPass) {
        return pass.value === confirmPass.value ? null : { notSame: true };
      }
      return null;
    }
    return null;
  };

  signupData = this.fb.group({
    loginData: this.fb.group({
      email: this.fb.control('', [
        Validators.required,
        Validators.email,
        Validators.max(128),
      ]),
      phone: this.fb.control('', [
        Validators.required,
        // Validators.pattern('^\\+?[0-9]{3}[0-9]{9}$|[0]{1}[0-9]{9}$'),
      ]),
      password: this.fb.control('', [
        Validators.required,
        Validators.min(4),
        Validators.max(512),
        Validators.pattern(
          '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&].{8,}',
        ),
      ]),
      confirmPassword: this.fb.control('', [Validators.required]),
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
      accountSignupInvitationToken: this.fb.control(''),
    }),
  });
  hidePassword = true;

  ngOnInit(): void {
    this.signupData.valueChanges.subscribe((e) => {
      const invalidControls = [];
      for (const control in this.loginData.controls) {
        if ((this.loginData.controls[control] as FormControl).invalid) {
          invalidControls.push(control);
        }
      }
    });

    this.confirmPassword.addValidators(this.checkPasswords);
  }

  getErrorMessage(control: FormControl) {
    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('invalid') ? 'Not a valid input' : '';
  }

  get loginData(): FormGroup {
    return this.signupData.get('loginData') as FormGroup;
  }

  get billingData(): FormGroup {
    return this.signupData.get('billingData') as FormGroup;
  }

  get accountData(): FormGroup {
    return this.signupData.get('accountData') as FormGroup;
  }

  get email(): FormControl {
    return this.loginData.controls['email'] as FormControl;
  }
  get password(): FormControl {
    return this.loginData.controls['password'] as FormControl;
  }
  get confirmPassword(): FormControl {
    return this.loginData.controls['confirmPassword'] as FormControl;
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
      'api/sessions/signup',
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
