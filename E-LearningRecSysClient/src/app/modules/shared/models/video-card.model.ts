import { SectionModel } from '../../portal/models/section.model';
import { AccountModel } from '../../portal/models/account.model';
export interface VideoCardModel {
  videoID: string;
  sectionId?: string;
  internalId?: string;
  title: string;
  description: string;
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
  account?: AccountModel;
  section?: SectionModel;
}
