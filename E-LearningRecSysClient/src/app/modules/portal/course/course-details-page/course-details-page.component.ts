import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BackApiHttpRequest } from 'src/app/modules/shared/models/back-api-http-request.model';
import { HttpService } from 'src/app/modules/shared/services/http.service';
import { map, retry, take } from 'rxjs/operators';
import { CourseModel } from '../../models/course.model';

@Component({
  selector: 'app-course-details-page',
  templateUrl: './course-details-page.component.html',
  styleUrls: ['./course-details-page.component.scss'],
})
export class CourseDetailsPageComponent implements OnInit {
  private courseId = '';
  public course!: CourseModel;

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService,
  ) {}
  ngOnInit(): void {
    this.courseId = this.route.snapshot.queryParamMap.get('id') || '';
    const getCourseDetailsRequest = new BackApiHttpRequest(
      'api/courses/course',
      {
        courseId: this.courseId,
      },
    );
    this.httpService
      .get(getCourseDetailsRequest)
      .pipe(take(1), retry(2))
      .subscribe((e) => {
        this.course = e.result;
        this.course.sections?.forEach((section) => {
          section.videos?.forEach((video) => {
            section.course = this.course;
            video.section = section;
            video.account = this.course.account;
          });
        });
      });
  }
}
