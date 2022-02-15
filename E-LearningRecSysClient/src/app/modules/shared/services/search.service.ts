import { Injectable } from '@angular/core';
import { BehaviorSubject, map, take } from 'rxjs';
import { BackApiHttpRequest } from '../models/back-api-http-request.model';
import { SearchResultModel } from '../models/search-result.model';
import { SearchTagModel } from '../models/search-tag.model';
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class SearchService {
  private testData: SearchResultModel[] = [
    {
      title: 'test-title1',
      author: 'test-author',
      description: 'test-description of video',
    },
    {
      title: 'test-title2',
      author: 'test-author',
      description: 'test-description of video',
    },
    {
      title: 'test-title3',
      author: 'test-author',
      description: 'test-description of video',
    },
    {
      title: 'test-title4',
      author: 'test-author',
      description: 'test-description of video',
    },
    {
      title: 'test-title5',
      author: 'test-author',
      description: 'test-description of video',
    },
  ];

  public searchAutocompleteOptions: BehaviorSubject<Array<string>>;
  public searchResults: BehaviorSubject<SearchResultModel[]>;
  public searchTags: BehaviorSubject<SearchTagModel[]>;
  public keywords: BehaviorSubject<string>;

  constructor(private httpService: HttpService) {
    this.searchAutocompleteOptions = new BehaviorSubject<Array<string>>([]);
    this.searchResults = new BehaviorSubject<SearchResultModel[]>(
      this.testData,
    );
    this.searchTags = new BehaviorSubject<SearchTagModel[]>([]);
    this.keywords = new BehaviorSubject<string>('');
  }

  public clearData(): void {
    this.searchAutocompleteOptions.next([]);
    this.searchResults.next(this.testData);
    this.searchTags.next([]);
  }

  public performAnonymousSearch(keywords: string, tags: SearchTagModel[]) {
    this.searchAutocompleteOptions
      .pipe(take(1))
      .subscribe((previousSearches) => {
        if (previousSearches.indexOf(keywords) == -1) {
          previousSearches.push(keywords);
        }
        this.searchAutocompleteOptions.next(previousSearches);
      });

    this.keywords.next(keywords);

    this.httpService
      .get(new BackApiHttpRequest('api/Videos', { keywords: keywords }))
      .pipe(
        take(1),
        map((response) => {
          if (!response.errors || response.errors.length === 0) {
            return this.mapSearchResultsDtoToSearchResultsModel(
              JSON.parse(response.result),
            );
          } else return [];
        }),
      )
      .subscribe((searchResults) => {
        this.searchResults.next(searchResults);
      });
  }

  private mapSearchResultsDtoToSearchResultsModel(searchResultsDto: object) {
    const result: SearchResultModel[] = [];
    //TODO mapping
    return result;
  }
}
