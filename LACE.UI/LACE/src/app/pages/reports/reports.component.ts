import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/shared/components/base-component';
import { DTO_ExamReportPublic } from 'src/shared/models/auth-user';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent extends BaseComponent implements OnInit {


  public reports:DTO_ExamReportPublic[] = [];

  constructor() {
    super();
   }

  ngOnInit(): void {
    for(let i = 0; i < 5; i++){
      let report = new DTO_ExamReportPublic();
      report.fileName = "Biopsia " + i;
      report.examDate = new Date();
      report.uploadDate = new Date();
      this.reports.push(report);
    }
  }

}
