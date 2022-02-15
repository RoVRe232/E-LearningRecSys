import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { SearchResultModel } from '../../shared/models/search-result.model';
import { SearchTagModel } from '../../shared/models/search-tag.model';
import { SearchService } from '../../shared/services/search.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();

  public searchResults: SearchResultModel[] = [];
  public searchTags: SearchTagModel[] = [];
  public keywords = '';
  public showFiller = true;

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
}
