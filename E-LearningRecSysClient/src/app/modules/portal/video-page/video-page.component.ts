import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { HttpService } from '../../shared/services/http.service';
import { take } from 'rxjs';
import { CourseModel } from '../models/course.model';
import { VideoModel } from '../models/video.model';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  public videoId: string;
  public videoMetadataId: string;
  public courseId: string;
  public parentCourse: CourseModel = null!;
  public videoMetadata: VideoModel = null!;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpService: HttpService,
  ) {
    this.videoId = route.snapshot.queryParamMap.get('id') || '';
    this.videoMetadataId = route.snapshot.queryParamMap.get('videoId') || '';
    this.courseId = route.snapshot.queryParamMap.get('courseId') || '';
    this.route.queryParams
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((e) => {
        if (e['videoId']) {
          const videoMetadataRequest = new BackApiHttpRequest('api/videos', {
            videoId: this.videoMetadataId,
          });
          this.httpService
            .get(videoMetadataRequest)
            .pipe(take(1))
            .subscribe((video) => {
              this.videoMetadata = { ...video.result } as VideoModel;
            });
        }
      });
  }

  get courseAuthorName() {
    return this.parentCourse?.account?.name || 'Unknown Author';
  }

  get videoCreationDate() {
    return this.videoMetadata.creationDate;
  }

  get videoShortDescription() {
    return this.videoMetadata.shortDescription;
  }

  get videoDescription() {
    return this.videoMetadata.description;
  }

  ngOnInit(): void {
    const courseRequest = new BackApiHttpRequest('api/courses/course', {
      courseId: this.courseId,
    });
    this.httpService
      .get(courseRequest)
      .pipe(take(1))
      .subscribe((course) => {
        this.parentCourse = { ...course.result } as CourseModel;
        if (this.parentCourse.owned == false) {
          this.router.navigate(['/', 'course', 'details'], {
            queryParams: {
              id: this.parentCourse.courseID,
            },
          });
        }
      });

    const videoMetadataRequest = new BackApiHttpRequest('api/videos', {
      videoId: this.videoMetadataId,
    });
    this.httpService
      .get(videoMetadataRequest)
      .pipe(take(1))
      .subscribe((video) => {
        this.videoMetadata = { ...video.result } as VideoModel;
      });
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }
}
