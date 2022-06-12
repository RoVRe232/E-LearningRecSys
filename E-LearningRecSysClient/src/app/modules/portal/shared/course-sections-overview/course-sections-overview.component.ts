import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { VideoCardModel } from 'src/app/modules/shared/models/video-card.model';
import { CourseModel } from '../../models/course.model';
import { VideoModel } from '../../models/video.model';

@Component({
  selector: 'app-course-sections-overview',
  templateUrl: './course-sections-overview.component.html',
  styleUrls: ['./course-sections-overview.component.scss'],
})
export class CourseSectionsOverviewComponent {
  @Input() course!: CourseModel;

  get sections() {
    return this.course.sections || [];
  }

  mapVideosToVideoCards(videos: VideoModel[] | undefined): VideoCardModel[] {
    if (videos) return videos.map((e) => this.mapVideoToVideoCard(e));
    return [];
  }

  mapVideoToVideoCard(video: VideoModel): VideoCardModel {
    return {
      ...video,
    } as VideoCardModel;
  }
}
