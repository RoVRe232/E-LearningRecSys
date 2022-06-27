import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CourseCardModel } from 'src/app/modules/shared/models/course-card.model';
import { VideoCardModel } from 'src/app/modules/shared/models/video-card.model';
import { CartService } from 'src/app/modules/shared/services/cart.service';
import { SearchService } from 'src/app/modules/shared/services/search.service';
import { NotificationService } from '../../../shared/services/notification.service';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss'],
})
export class ResultCardComponent {
  @Input() isCartSummary = false;
  @Input() isOwnedItem = false;
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
    if (this.course.price && !this.course.owned) {
      return `${this.course.price.amount} ${this.course.price.currency}`;
    }
    return 'Owned';
  }

  get owned() {
    return this.course.owned;
  }

  constructor(
    private router: Router,
    private searchService: SearchService,
    private cartService: CartService,
    private notificationService: NotificationService,
  ) {}

  onRedirectToVideo() {
    this.searchService.storeQueryKeywordsToStorage();
    this.router.navigate(['/', 'videos', 'watch'], {
      queryParams: {
        id: this.video.internalId,
        videoId: this.video.videoID,
        courseId: this.video.section?.course?.courseID,
      },
    });
  }

  onRedirectToCourse() {
    this.searchService.storeQueryKeywordsToStorage();
    this.router.navigate(['/', 'course', 'details'], {
      queryParams: {
        id: this.course.courseID,
      },
    });
  }

  onAddToCart() {
    this.cartService.addCourseToCardContent(this.course);
    this.notificationService.showSuccessNotification('Item added to cart');
  }

  onRemoveFromCart(courseId: string | undefined) {
    if (courseId) this.cartService.removeCourseFromCardContent(courseId);
  }

  get videoDurationHHMMSS() {
    return new Date(this.video.duration * 1000).toISOString().substr(11, 8);
  }

  get courseDurationHours() {
    return this.course.hours;
  }
}
