import { Component } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  public searchGroup = this.formBuilder.group({
    searchControl: [''],
  });

  constructor(private formBuilder: FormBuilder, private router: Router) {}

  public get searchControl(): FormControl {
    return this.searchGroup.get('searchControl') as FormControl;
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
