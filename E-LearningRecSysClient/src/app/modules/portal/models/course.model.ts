import { AccountModel } from './account.model';
import { SectionModel } from './section.model';

export interface CourseModel {
  courseID: string;
  name?: string;
  smallDescription?: string;
  largeDescription?: string;
  keywords?: string[];
  thumbnailImage?: string;
  hours?: number;
  accountID?: string;
  account?: AccountModel;
  owned?: boolean;
  price?: {
    amount: number;
    currency: string;
  };
  sections?: SectionModel[];
}
