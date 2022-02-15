import { Component, OnDestroy, OnInit } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, take, takeUntil } from 'rxjs';
import { SearchTagModel } from '../models/search-tag.model';
import { SearchService } from '../services/search.service';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss'],
})
export class SearchBarComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  public searchGroup: FormGroup;
  public searchTags: SearchTagModel[] = [];
  public addOnBlur = true;

  constructor(
    public searchService: SearchService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) {
    this.searchGroup = this.formBuilder.group({
      searchControl: new FormControl([''], Validators.minLength(1)),
    });
  }

  ngOnInit(): void {
    this.searchService.keywords.pipe(take(1)).subscribe((e) => {
      this.searchControl.setValue(e);
    });

    this.searchService.searchTags
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((e) => {
        this.searchTags = e;
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }

  public get searchControl(): FormControl {
    return this.searchGroup.get('searchControl') as FormControl;
  }

  public submitAnonymousSearch() {
    if (this.searchControl.valid) {
      this.searchService.performAnonymousSearch(
        this.searchControl.value,
        this.searchTags,
      );
      this.router.navigate(['/search-results'], {
        queryParams: { keywords: this.searchControl.value },
      });
    }
  }

  public submitAnonymousSearchTag(tag: string) {
    if (tag.length > 0) {
      this.searchService.performAnonymousSearch(tag, this.searchTags);
      this.router.navigate(['/search-results'], {
        queryParams: { keywords: this.searchControl.value },
      });
    }
  }

  public add(event: MatChipInputEvent): void {
    const chipValue = (event.value || '').trim();
    if (chipValue) {
      this.searchTags.push({ value: chipValue, active: true });
    }
    event.chipInput?.clear();

    this.submitAnonymousSearchTag(chipValue);
  }

  public remove(chip: SearchTagModel): void {
    const index = this.searchTags.indexOf(chip);
    this.submitAnonymousSearch();

    if (index >= 0) {
      this.searchTags.splice(index, 1);
    }
  }
}
