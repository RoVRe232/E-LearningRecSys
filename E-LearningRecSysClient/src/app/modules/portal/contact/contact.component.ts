import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { take } from 'rxjs';
import { AccountService } from '../../accounts/services/account.service';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
})
export class ContactComponent {
  constructor(private fb: FormBuilder, private httpService: HttpService) {
    this.findUserLocation();
  }

  contactData = this.fb.group({
    firstName: this.fb.control('', [Validators.required]),
    lastName: this.fb.control('', [Validators.required]),
    email: this.fb.control('', [Validators.required, Validators.email]),
    message: this.fb.control('', [Validators.required]),
  });

  getErrorMessage(control: FormControl) {
    if (control.hasError('required')) {
      return 'You must enter a value';
    }

    return control.hasError('invalid') ? 'Not a valid input' : '';
  }

  onSubmit() {
    const contactUsRequest = new BackApiHttpRequest(
      'portal/contact-us',
      {},
      this.contactData.value,
    );
    this.httpService
      .post(contactUsRequest)
      .pipe(take(1))
      .subscribe((e) => console.log(e));
  }

  findUserLocation() {
    const getIPRequest = {
      domain: 'https://api.ipify.org',
      path: '',
      queryParams: {
        format: 'json',
      },
    };

    this.httpService
      .get(getIPRequest)
      .pipe(take(1))
      .subscribe((response) => {
        const getLocationRequest = {
          domain: `https://ipapi.co/`,
          path: `${response.result.ip}/json/`,
          queryParams: {},
        };
        this.httpService
          .get(getLocationRequest)
          .pipe(take(1))
          .subscribe((e) => console.log(e));
      });
  }

  upload(event: Event) {
    console.log(event);
  }

  get email(): FormControl {
    return this.contactData.controls['email'] as FormControl;
  }

  get firstName(): FormControl {
    return this.contactData.controls['firstName'] as FormControl;
  }
  get lastName(): FormControl {
    return this.contactData.controls['lastName'] as FormControl;
  }
  get message(): FormControl {
    return this.contactData.controls['message'] as FormControl;
  }
}
