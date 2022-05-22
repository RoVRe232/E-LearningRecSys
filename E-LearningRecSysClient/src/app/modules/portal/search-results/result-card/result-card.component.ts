import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CourseCardModel } from 'src/app/modules/shared/models/course-card.model';
import { VideoCardModel } from 'src/app/modules/shared/models/video-card.model';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss'],
})
export class ResultCardComponent {
  @Input() video: VideoCardModel = {
    videoID: 'null',
    title: 'Test',
    description: 'Test Description',
    thumbnail:
      'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png',
    author: 'Default author',
    duration: 0,
    hiddenInSearches: true,
  };

  @Input() course: CourseCardModel = {
    name: 'test-title1',
    author: 'test-author',
    smallDescription: 'test-description of video',
    largeDescription: 'test-large description of video',
    hours: 100,
    hiddenInSearches: true,
  };

  constructor(private router: Router) {}

  onRedirectToVideo() {
    console.log(`videoId: ${this.video.videoID}`);
    this.router.navigate(['/', 'videos', 'watch'], {
      queryParams: {
        id: this.video.internalId,
      },
    });
  }

  onRedirectToCourse() {
    this.router.navigate(['/', 'courses', 'details'], {
      queryParams: {
        id: this.course.courseId,
      },
    });
  }
}
