import { Injectable } from '@angular/core';
import { BehaviorSubject, map, take } from 'rxjs';
import { BackApiHttpRequest } from '../models/back-api-http-request.model';
import { CourseCardModel } from '../models/course-card.model';
import { SearchTagModel } from '../models/search-tag.model';
import { VideoCardModel } from '../models/video-card.model';
import { HttpService } from './http.service';

export interface SearchResults {
  courses?: CourseCardModel[];
  videos?: VideoCardModel[];
}

@Injectable({ providedIn: 'root' })
export class SearchService {
  private testData: SearchResults = {
    courses: [
      {
        title: 'test-title1',
        author: 'test-author',
        description: 'test-description of video',
        thumbnail:
          'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png',
      },
      {
        title: 'test-title2',
        author: 'test-author',
        description: 'test-description of video',
        thumbnail:
          'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png',
      },
    ],
    videos: [
      {
        videoId: 'testid',
        sectionId: 'string',
        title: 'string',
        description: 'string',
        source: {
          videoSourceId: 'string',
          type: 'string',
          location: 'string',
        },
        keywords: 'string',
        thumbnail:
          'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png',
        slides: [
          {
            videoSlidesId: 'string',
            mimetype: 'string',
            url: 'string',
          },
        ],
        hidden: false,
        creationDate: new Date(Date.now()),
        language: 'string',
        hiddenInSearches: false,
        duration: 10,
        transcription: 'string',
        author: 'string',
      },
    ],
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
    return {};
  }
}
