export interface CourseCardModel {
  courseID?: string;
  name: string;
  smallDescription: string;
  largeDescription: string;
  thumbnailImage?: string;
  hours: number;
  tags?: string[];
  externalLink?: string;
  internalLink?: string;
  hiddenInSearches?: boolean;
  owned?: boolean;
  account?: {
    accountID?: string;
    name?: string;
  };
  price?: {
    amount: number;
    currency: string;
  };
}
