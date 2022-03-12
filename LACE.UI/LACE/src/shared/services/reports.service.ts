import { Injectable } from '@angular/core';
import { ExamReport } from '../models/exam-report';
import { ListResponse } from '../models/list-response';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private httpService: HttpService) { }

  getBySession(){
    return this.httpService.get<ListResponse<ExamReport>>('report');
  }

}
