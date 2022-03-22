import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, EMPTY, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class HttpGeneralService {
  private url: string;
  private headers: HttpHeaders;
  constructor(public httpClient: HttpClient) {
    this.url = environment.serverAddress;
    this.headers = new HttpHeaders();
  }

  post<ModelType>(apiRoute: string, body: any): Observable<HttpResponse<ModelType>> {
    return this.httpClient
      .post<ModelType>(`${this.url + apiRoute}`, body, {
        headers: this.headers,
        withCredentials: true,
        observe: 'response',
      })
      .pipe(catchError(() => EMPTY));
  }

  get<ModelType>(apiRoute: string): Observable<HttpResponse<ModelType>> {
    return this.httpClient
      .get<ModelType>(`${this.url + apiRoute}`, {
        headers: this.headers,
        withCredentials: true,
        observe: 'response',
      })
      .pipe(catchError(() => EMPTY));
  }

  put<ModelType>(apiRoute: string, body: any): Observable<HttpResponse<ModelType>> {
    return this.httpClient
      .put<ModelType>(`${this.url + apiRoute}`, body, {
        headers: this.headers,
        withCredentials: true,
        observe: 'response',
      })
      .pipe(catchError(() => EMPTY));
  }

  delete<ModelType>(apiRoute: string): Observable<HttpResponse<ModelType>> {
    return this.httpClient
      .delete<ModelType>(`${this.url + apiRoute}`, {
        headers: this.headers,
        withCredentials: true,
        observe: 'response',
      })
      .pipe(catchError(() => EMPTY));
  }

  setHttpHeader(name: string, value: string | string[]) {
    this.headers.set(name, value);
  }
}
