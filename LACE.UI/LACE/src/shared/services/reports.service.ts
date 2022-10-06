import { Injectable } from '@angular/core';
import { BaseResponse } from '../models/base-response';
import { DTO_ExamReportPublic } from "src/shared/models/dto-exam-report-public";
import { ExamReport } from 'src/shared/models/exam-report';
import { ListResponse } from 'src/shared/models/list-response';
import { HttpService } from './http.service';
import { DocumentTypeEnum } from '../models/enums/document-type-enum';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private httpService: HttpService) { }

  getBySession(){
    return this.httpService.get<ListResponse<DTO_ExamReportPublic>>('examReport');
  }

  create(report:DTO_ExamReportPublic){
    return this.httpService.post<DTO_ExamReportPublic>('examReport', report);
  }

  downloadByExam(examId:string, document:string, documentType:DocumentTypeEnum){
    return this.httpService.get<ListResponse<DTO_ExamReportPublic>>(`examReport/exam?examId=${examId}&document=${document}&documentType=${documentType}`)
  }
}
