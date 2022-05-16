export interface CourseCardModel {
  id?: string;
  title: string;
  description: string;
  thumbnail?: string;
  author: string;
  tags?: string[];
  externalLink?: string;
  internalLink?: string;
  hiddenInSearches?: boolean;
}
