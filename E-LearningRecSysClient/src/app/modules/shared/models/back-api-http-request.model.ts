import { BasicHttpRequest } from './basic-http-request.model';

export class BackApiHttpRequest implements BasicHttpRequest {
  public path?: string | undefined;
  public domain: string;
  public queryParams?: { [key: string]: string } | undefined;
  public segment?: string | undefined;
  public body?: string | undefined;

  constructor(
    path: string,
    queryParams?: { [key: string]: string } | undefined,
    body?: string | undefined,
    segment?: string | undefined,
  ) {
    this.domain = 'https://localhost:44348';
    this.path = path;
    this.queryParams = queryParams;
    this.body = body;
    this.segment = segment;
  }
}
