import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpConfiguration } from '../models/HttpConfiguration';
@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl: string = "";

  constructor(private httpClient: HttpClient,public httpConfiguration: HttpConfiguration) {
    this.baseUrl = httpConfiguration.enviroment.baseUrl;
  }

  getHeaders(){
      let headers:HttpHeaders = new HttpHeaders();

      headers = headers.set('Access-Control-Allow-Origin', this.httpConfiguration.AccessControlAllowOrigin);
      headers = headers.set('Access-Control-Allow-Methods', this.httpConfiguration.AccessControlAllowMethods);
      headers = headers.set("Access-Control-Allow-Headers", this.httpConfiguration.AccessControlAllowHeaders);
      headers = headers.set('Content-Type', this.httpConfiguration.ContentType);
      headers = headers.set('NDToken', localStorage.getItem('NDToken') || '');

      return headers;
  }
  post<T>(url: string, data: any) {
    return this.httpClient.post<T>(this.baseUrl + url, data, {
        headers: this.getHeaders()
    });
  }
  get<T>(url: string) {
    return this.httpClient.get<T>(this.baseUrl + url, {
        headers: this.getHeaders()
    });
  };
  patch<T>(url: string, data: any) {
    return this.httpClient.patch<T>(this.baseUrl + url, data,  {
        headers: this.getHeaders()
    });
  }
  put<T>(url: string, data: any) {
    return this.httpClient.put<T>(this.baseUrl + url, data,  {
        headers: this.getHeaders()
    });
  }
  delete<T>(url: string) {
    return this.httpClient.delete<T>(`${this.baseUrl}${url}`, {
        headers: this.getHeaders()
    });
  }
}

