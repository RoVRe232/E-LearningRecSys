import { VideoModel } from './video.model';
import { CourseModel } from './course.model';

export interface SectionModel {
  title?: string;
  description?: string;
  thumbnailImage?: string;
  videos?: VideoModel[];
  course?: CourseModel;
}
