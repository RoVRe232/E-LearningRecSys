import { SectionModel } from './section.model';
import { AccountModel } from './account.model';
export interface VideoModel {
  videoID: string;
  sectionId?: string;
  internalId?: string;
  title: string;
  description: string;
  shortDescription?: string;
  source?: {
    videoSourceId: string;
    type: string;
    location: string;
  };
  keywords?: string;
  thumbnail: string;
  slides?: Array<{
    videoSlidesId: string;
    mimetype?: string;
    url: string;
  }>;
  hidden?: boolean;
  creationDate?: Date;
  language?: string;
  hiddenInSearches?: boolean;
  duration: number;
  transcription?: string;
  authorName?: string;
  account?: AccountModel;
  section?: SectionModel;
}
