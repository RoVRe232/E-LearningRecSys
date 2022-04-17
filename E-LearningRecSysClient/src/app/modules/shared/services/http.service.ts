import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BasicHttpRequest } from '../models/basic-http-request.model';
import { catchError, retry, throwError } from 'rxjs';
import { BasicHttpResponse } from '../models/basic-http-response.model';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private http: HttpClient) {}

  public get(request: BasicHttpRequest) {
    console.log(this.buildRequestTargetUrl(request));
    return this.http
      .get<BasicHttpResponse>(this.buildRequestTargetUrl(request), {
        headers: request.headers,
      })
      .pipe(retry(1), catchError(this.handleError));
  }

  public post(request: BasicHttpRequest) {
    return this.http
      .post<BasicHttpResponse>(
        this.buildRequestTargetUrl(request),
        request.body,
        {
          headers: request.headers,
        },
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  public put(request: BasicHttpRequest) {
    return this.http
      .put<BasicHttpResponse>(
        this.buildRequestTargetUrl(request),
        request.body,
        {
          headers: request.headers,
        },
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  public delete(request: BasicHttpRequest) {
    return this.http
      .delete<BasicHttpResponse>(this.buildRequestTargetUrl(request), {
        headers: request.headers,
      })
      .pipe(retry(1), catchError(this.handleError));
  }

  private buildRequestTargetUrl(request: BasicHttpRequest): string {
    let result = request.domain;
    result = result.concat(`/${request.path}`);

    if (request.queryParams) {
      result = result.concat('?');
      for (const [key, value] of Object.entries(request.queryParams))
        result = result.concat(`${key}=${value}&`);
      result = result.substring(0, result.length - 1);
    }

    if (request.segment) result = result.concat(`#${request.segment}`);

    return result;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error,
      );
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something went wrong'));
  }
}
