export interface SearchResultModel {
  id?: string;
  title: string;
  description: string;
  thumbnail?: string;
  author: string;
  tags?: string[];
  externalLink?: string;
  internalLink?: string;
}
