import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { SearchFiltersModel } from '../../shared/models/search-filters.model';
import { SearchTagModel } from '../../shared/models/search-tag.model';
import {
  IntervalSearchFilter,
  SearchFilter,
  SearchFilterType,
  SearchResults,
  SearchService,
} from '../../shared/services/search.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  public searchFilters: SearchFilter[] = [];
  public searchResults: SearchResults | null = null;
  public searchTags: SearchTagModel[] = [];
  public keywords = '';
  public showFiller = true;
  public searchFilterTypeEnum = SearchFilterType;
  public categories: Array<SearchFiltersModel> = [
    {
      name: 'Java',
      completed: false,
      color: 'primary',
      subtasks: [
        { name: 'Spring boot', completed: false, color: 'primary' },
        { name: 'Jenkins', completed: false, color: 'primary' },
      ],
    },
  ];

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
        this.searchFilters = result.filters;
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
    filtersGroup.filters != null &&
      filtersGroup.filters.every((t) => t.checked);
  }

  someComplete(filtersGroup: SearchFilter): boolean {
    if (filtersGroup.filters == null) {
      return false;
    }
    return (
      filtersGroup.filters.filter((t) => t.checked).length > 0 &&
      !filtersGroup.allCompleted
    );
  }

  setAll(filtersGroup: SearchFilter, checked: boolean) {
    filtersGroup.allCompleted = checked;
    if (filtersGroup.filters == null) {
      return;
    }
    filtersGroup.filters.forEach((t) => (t.checked = checked));
  }

  asIntervalSearchFilter(searchFilter: SearchFilter): IntervalSearchFilter {
    return searchFilter as IntervalSearchFilter;
  }

  initializeSearchFilters() {
    this.searchFilters = [
      {
        name: 'Courses',
        type: SearchFilterType.CHECKBOX_ALL,
        description: '(246)',
        checked: false,
        color: 'primary',
        filters: [
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
        filters: [
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
        filters: [
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
        filters: [
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
