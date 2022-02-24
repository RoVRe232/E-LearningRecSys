import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(private fb: FormBuilder) {}

  loginData = this.fb.group({
    email: this.fb.control('', [Validators.required, Validators.email]),
    password: this.fb.control('', [Validators.required]),
  });
  hidePassword = true;

  ngOnInit(): void {
    this.loginData.valueChanges.subscribe((e) => {
      console.log(this.loginData.valid);
    });
  }

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.email.hasError('email') ? 'Not a valid email' : '';
  }

  get email(): FormControl {
    return this.loginData.controls['email'] as FormControl;
  }

  get password(): FormControl {
    return this.loginData.controls['password'] as FormControl;
  }

  onSubmit() {
    //TODO send login request
  }
}
