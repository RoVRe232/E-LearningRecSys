import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private snackBar: MatSnackBar,
  ) {}

  loginData = this.fb.group({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [Validators.required]),
  });
  hidePassword = true;

  ngOnInit(): void {
    this.loginData.valueChanges.subscribe(() => {
      const invalidControls = [];
      for (const control in this.loginData.controls) {
        if ((this.loginData.controls[control] as FormControl).invalid) {
          invalidControls.push(control);
        }
      }
    });
  }

  getErrorMessage(control: FormControl) {
    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('invalid') ? 'Not a valid input' : '';
  }

  get email(): FormControl {
    return this.loginData.controls['email'] as FormControl;
  }

  get password(): FormControl {
    return this.loginData.controls['password'] as FormControl;
  }

  onSubmit() {
    this.accountService.login(this.email.value, this.password.value);
  }
}
