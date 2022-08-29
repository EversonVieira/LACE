import { Injectable } from '@angular/core';
import { AuthSession } from '../models/auth-session';
import { AuthUser } from '../models/auth-user';
import { BaseResponse } from '../models/base-response';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpService: HttpService) { }

  login(user: AuthUser) {
    return this.httpService.post<BaseResponse<AuthSession>>('login', user);
  }

  getSessionUser(){
    return this.httpService.get<BaseResponse<AuthUser>>('login/user');
  }
}
