import { Component, OnInit } from '@angular/core';
import { BackApiHttpRequest } from '../../shared/models/back-api-http-request.model';
import { CourseCardModel } from '../../shared/models/course-card.model';
import { HttpService } from '../../shared/services/http.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-owned-courses-page',
  templateUrl: './owned-courses-page.component.html',
  styleUrls: ['./owned-courses-page.component.scss'],
})
export class OwnedCoursesPageComponent implements OnInit {
  public courses: CourseCardModel[] = [];
  constructor(private httpService: HttpService) {}

  ngOnInit(): void {
    const ownedCoursesRequest = new BackApiHttpRequest(
      'api/courses/owned-courses',
    );
    this.httpService
      .get(ownedCoursesRequest)
      .pipe(take(1))
      .subscribe((ownedCourses) => (this.courses = ownedCourses.result));
  }
}
