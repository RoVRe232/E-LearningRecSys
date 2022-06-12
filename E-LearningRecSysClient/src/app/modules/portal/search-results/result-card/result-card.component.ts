import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CourseCardModel } from 'src/app/modules/shared/models/course-card.model';
import { VideoCardModel } from 'src/app/modules/shared/models/video-card.model';
import { CartService } from 'src/app/modules/shared/services/cart.service';
import { SearchService } from 'src/app/modules/shared/services/search.service';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss'],
})
export class ResultCardComponent {
  @Input() isCartSummary = false;
  @Input() video: VideoCardModel = {
    videoID: 'null',
    title: 'Test',
    description: 'Test Description',
    thumbnail:
      'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png',
    account: {
      accountID: 'test-authorId',
      name: 'test-author-name',
    },
    duration: 0,
    hiddenInSearches: true,
  };

  @Input() course: CourseCardModel = {
    name: 'test-title1',
    account: {
      accountID: 'test-author-id',
      name: 'author-name',
    },
    smallDescription: 'test-description of video',
    largeDescription: 'test-large description of video',
    hours: 100,
    hiddenInSearches: true,
  };

  get courseAuthorName() {
    return this.course.account?.name || 'Unknown Author';
  }

  get videoAuthorName() {
    return this.video.account?.name || 'Unknown Author';
  }

  get formattedPrice() {
    if (this.course.price) {
      return `${this.course.price.amount} ${this.course.price.currency}`;
    }
    return 'Owned';
  }

  constructor(
    private router: Router,
    private searchService: SearchService,
    private cartService: CartService,
  ) {}

  onRedirectToVideo() {
    this.searchService.storeQueryKeywordsToStorage();
    this.router.navigate(['/', 'videos', 'watch'], {
      queryParams: {
        id: this.video.internalId,
        videoId: this.video.videoID,
        courseId: this.video.section?.courseID,
      },
    });
  }

  onRedirectToCourse() {
    this.searchService.storeQueryKeywordsToStorage();
    this.router.navigate(['/', 'courses', 'details'], {
      queryParams: {
        id: this.course.courseID,
      },
    });
  }

  onAddToCart() {
    this.cartService.addCourseToCardContent(this.course);
  }

  onRemoveFromCart(courseId: string | undefined) {
    console.log(courseId);
    if (courseId) this.cartService.removeCourseFromCardContent(courseId);
  }

  get videoDurationHHMMSS() {
    return new Date(this.video.duration * 1000).toISOString().substr(11, 8);
  }

  get courseDurationHours() {
    return this.course.hours;
  }
}
