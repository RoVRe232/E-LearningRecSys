import { SearchResultModel } from './search-result.model';

export interface SearchResultsBundle {
  keywords: string;
  tags?: string[];
  skip?: number;
  take?: number;
  totalCount?: number;
  results?: SearchResultModel[];
}
