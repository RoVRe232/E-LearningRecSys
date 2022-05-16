import { CourseCardModel } from './course-card.model';

export interface SearchResultsBundle {
  keywords: string;
  tags?: string[];
  skip?: number;
  take?: number;
  totalCount?: number;
  results?: CourseCardModel[];
}
