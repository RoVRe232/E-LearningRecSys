import { HttpClient } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Editor, Validators } from 'ngx-editor';
import { BehaviorSubject, concatMap, filter, Subject, takeUntil } from 'rxjs';
import { SearchFiltersModel } from 'src/app/modules/shared/models/search-filters.model';
import { SearchTagModel } from 'src/app/modules/shared/models/search-tag.model';
import {
  IntervalSearchFilter,
  SearchFilter,
  SearchFilterType,
  SearchResults,
  SearchService,
} from 'src/app/modules/shared/services/search.service';

@Component({
  selector: 'app-courses-overview',
  templateUrl: './courses-overview.component.html',
  styleUrls: ['./courses-overview.component.scss'],
})
export class CoursesOverviewComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  public searchFilters: SearchFilter[] = [];
  public searchResults: SearchResults | null = null;
  public searchTags: SearchTagModel[] = [];
  public keywords = '';
  public showFiller = true;
  public searchFilterTypeEnum = SearchFilterType;

  constructor(private searchService: SearchService) {
    this.initializeSearchFilters();
  }

  ngOnInit(): void {
    this.searchService.performAnonymousSearch(
      this.searchService.keywords.value,
      [],
    );
    this.searchService.searchResults
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => {
        this.searchResults = result;
        this.searchFilters = result.filters!;
      });
    this.searchService.searchTags
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => (this.searchTags = result));
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
    this.searchService.clearData();
  }

  allComplete = false;

  updateAllComplete(filtersGroup: SearchFilter) {
    filtersGroup.allCompleted = true;
    if (filtersGroup.subFilters != null) {
      filtersGroup.subFilters.every((t) => t.checked);
      this.searchService.performAnonymousSearch(
        this.keywords,
        this.searchTags,
        this.searchFilters,
      );
    }
  }

  someComplete(filtersGroup: SearchFilter): boolean {
    if (filtersGroup.subFilters == null) {
      return false;
    }
    return (
      filtersGroup.subFilters.filter((t) => t.checked).length > 0 &&
      !filtersGroup.allCompleted
    );
  }

  setAll(filtersGroup: SearchFilter, checked: boolean) {
    filtersGroup.allCompleted = checked;
    if (filtersGroup.subFilters == null) {
      return;
    }
    filtersGroup.subFilters.forEach((t) => (t.checked = checked));
  }

  asIntervalSearchFilter(searchFilter: SearchFilter): IntervalSearchFilter {
    return searchFilter as IntervalSearchFilter;
  }

  sliderValueChanged() {
    this.searchService.performAnonymousSearch(
      this.keywords,
      this.searchTags,
      this.searchFilters,
    );
  }

  initializeSearchFilters() {
    this.searchFilters = [
      {
        name: 'Courses',
        type: SearchFilterType.CHECKBOX_ALL,
        description: '(246)',
        checked: false,
        color: 'primary',
        subFilters: [
          {
            name: 'Java',
            type: SearchFilterType.CHECKBOX,
            description: '(123)',
            checked: false,
            color: 'primary',
          },
          {
            name: 'C#',
            type: SearchFilterType.CHECKBOX,
            description: '(123)',
            checked: false,
            color: 'primary',
          },
        ],
      },
      {
        name: 'Authors',
        type: SearchFilterType.CHECKBOX_ALL,
        description: '(246)',
        checked: false,
        color: 'primary',
        subFilters: [
          {
            name: 'Author1',
            type: SearchFilterType.CHECKBOX,
            description: '(123)',
            checked: false,
            color: 'primary',
          },
          {
            name: 'Author2',
            type: SearchFilterType.CHECKBOX,
            description: '(123)',
            checked: false,
            color: 'primary',
          },
        ],
      },
      {
        name: 'Price',
        type: SearchFilterType.INTERVAL,
        description: '(500)',
        checked: false,
        color: 'primary',
        subFilters: [
          {
            name: 'Course price',
            type: SearchFilterType.INTERVAL,
            description: '(123)',
            checked: false,
            color: 'primary',
            lowerBound: 10,
            upperBound: 70,
            lowValue: 10,
            highValue: 70,
          } as IntervalSearchFilter,
        ],
      },
      {
        name: 'Duration',
        type: SearchFilterType.INTERVAL,
        description: '(minutes)',
        checked: false,
        color: 'primary',
        subFilters: [
          {
            name: 'Duration (in minutes)',
            type: SearchFilterType.INTERVAL,
            description: '(123)',
            checked: false,
            color: 'primary',
            lowerBound: 20,
            upperBound: 50,
            lowValue: 20,
            highValue: 50,
          } as IntervalSearchFilter,
        ],
      },
    ];
  }
}
