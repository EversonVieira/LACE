import { Injectable } from '@angular/core';
import { AuthUser } from '../models/auth-user';
import { BaseResponse } from '../models/base-response';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }

  create(user: AuthUser) {
    return this.httpService.post<BaseResponse<number>>('user', user);
  }

  update(user: AuthUser) {
    return this.httpService.put<BaseResponse<number>>('user', user);
  }

}
