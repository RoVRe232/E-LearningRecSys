import { HttpHeaders } from '@angular/common/http';

export interface BasicHttpRequest {
  path?: string;
  domain: string;
  queryParams?: { [key: string]: string | number };
  segment?: string;
  body?: string | object;
  headers?: HttpHeaders;
}
