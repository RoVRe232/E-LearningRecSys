import { VideoModel } from './video.model';

export interface SectionModel {
  title?: string;
  description?: string;
  thumbnailImage?: string;
  videos?: VideoModel[];
}
