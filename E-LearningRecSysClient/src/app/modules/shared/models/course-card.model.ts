export interface CourseCardModel {
  courseId?: string;
  name: string;
  smallDescription: string;
  largeDescription: string;
  thumbnailImage?: string;
  hours: number;
  tags?: string[];
  externalLink?: string;
  internalLink?: string;
  hiddenInSearches?: boolean;
  author: string;
}
