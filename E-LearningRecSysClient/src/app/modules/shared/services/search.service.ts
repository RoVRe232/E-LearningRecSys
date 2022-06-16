import { Injectable } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { BehaviorSubject, map, take } from 'rxjs';
import { BackApiHttpRequest } from '../models/back-api-http-request.model';
import { CourseCardModel } from '../models/course-card.model';
import { SearchFiltersModel } from '../models/search-filters.model';
import { SearchTagModel } from '../models/search-tag.model';
import { VideoCardModel } from '../models/video-card.model';
import { HttpService } from './http.service';

export enum SearchFilterType {
  CHECKBOX_ALL = 0,
  CHECKBOX = 1,
  INTERVAL = 2,
}

export interface SearchResults {
  courses?: CourseCardModel[];
  videos?: VideoCardModel[];
  filters?: { [key: string]: IntervalSearchFilter[] };
}

export interface SearchFilter {
  name: string;
  value?: string;
  type: SearchFilterType;
  description?: string;
  allCompleted?: boolean;
  checked?: boolean;
  color: ThemePalette;
  filters?: SearchFilter[];
}

export interface IntervalSearchFilter extends SearchFilter {
  lowerBound: number;
  upperBound: number;
  lowValue: number;
  highValue: number;
}

@Injectable({ providedIn: 'root' })
export class SearchService {
  private testData: SearchResults = {
    courses: [],
    videos: [],
  };

  public searchAutocompleteOptions: BehaviorSubject<Array<string>>;
  public searchResults: BehaviorSubject<SearchResults>;
  public searchTags: BehaviorSubject<SearchTagModel[]>;
  public keywords: BehaviorSubject<string>;

  constructor(private httpService: HttpService) {
    this.searchAutocompleteOptions = new BehaviorSubject<Array<string>>([]);
    this.searchResults = new BehaviorSubject<SearchResults>(this.testData);
    this.searchTags = new BehaviorSubject<SearchTagModel[]>([]);
    this.keywords = new BehaviorSubject<string>('');
  }

  public clearData(): void {
    this.searchAutocompleteOptions.next([]);
    this.searchResults.next(this.testData);
    this.searchTags.next([]);
  }

  public performAnonymousSearch(keywords: string, tags: SearchTagModel[]) {
    //TODO Add support for reloading previous queries
    this.searchAutocompleteOptions
      .pipe(take(1))
      .subscribe((previousSearches) => {
        if (previousSearches.indexOf(keywords) == -1) {
          previousSearches.push(keywords);
        }
        previousSearches.reduce((acc, e) => {
          return acc + e;
        });
        this.searchAutocompleteOptions.next(previousSearches);
      });

    this.keywords.next(keywords);

    const concatKeywords = this.searchTags.value.reduce(
      (acc: string, e: SearchTagModel) => (acc += e.value + ' '),
      '',
    );

    if (concatKeywords != null && concatKeywords != undefined) {
      this.httpService
        .post(
          new BackApiHttpRequest(
            'api/search/query',
            {},
            {
              keyPhrases: [concatKeywords],
              filters: [],
              paginationOptions: {
                take: 0,
                skip: 0,
              },
            },
          ),
        )
        .pipe(
          take(1),
          map((response) => {
            return response.result;
          }),
        )
        .subscribe((searchResults: SearchResults) => {
          console.log(`searchResults ${searchResults}`);
          this.searchResults.next(searchResults);
        });
    } else {
      this.searchResults.next({ courses: [], videos: [] } as SearchResults);
    }
  }

  public storeQueryKeywordsToStorage() {
    localStorage.setItem(
      'user_last_query',
      JSON.stringify(this.searchTags.value),
    );
  }

  public retrieveQueryKeywordsFromStorage() {
    const user_last_query = localStorage.getItem('user_last_query');
    if (user_last_query) {
      this.searchTags = JSON.parse(user_last_query);
      this.performAnonymousSearch('', JSON.parse(user_last_query));
    }
  }
}
