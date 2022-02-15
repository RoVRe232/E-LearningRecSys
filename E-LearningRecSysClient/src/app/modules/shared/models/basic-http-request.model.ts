export interface BasicHttpRequest {
  path?: string;
  domain: string;
  queryParams?: { [key: string]: string };
  segment?: string;
  body?: string;
}
