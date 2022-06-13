import { Component, Input } from '@angular/core';
import { CourseModel } from '../../models/course.model';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.scss'],
})
export class CourseDetailsComponent {
  @Input() course!: CourseModel;

  get courseAuthorName() {
    return this.course.account?.name || 'Unknown Author';
  }
}
