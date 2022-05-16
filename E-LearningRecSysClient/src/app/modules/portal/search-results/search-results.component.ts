import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { SearchFiltersModel } from '../../shared/models/search-filters.model';
import { SearchTagModel } from '../../shared/models/search-tag.model';
import {
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

  public searchResults: SearchResults | null = null;
  public searchTags: SearchTagModel[] = [];
  public keywords = '';
  public showFiller = true;
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

  constructor(private searchService: SearchService) {}

  ngOnInit(): void {
    this.searchService.searchResults
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => (this.searchResults = result));
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

  updateAllComplete(filters: SearchFiltersModel) {
    this.allComplete =
      filters.subtasks != null && filters.subtasks.every((t) => t.completed);
  }

  someComplete(filters: SearchFiltersModel): boolean {
    if (filters.subtasks == null) {
      return false;
    }
    return (
      filters.subtasks.filter((t) => t.completed).length > 0 &&
      !this.allComplete
    );
  }

  setAll(filters: SearchFiltersModel, completed: boolean) {
    this.allComplete = completed;
    if (filters.subtasks == null) {
      return;
    }
    filters.subtasks.forEach((t) => (t.completed = completed));
  }
}
