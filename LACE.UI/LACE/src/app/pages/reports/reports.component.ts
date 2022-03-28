import { Component, OnInit } from '@angular/core';
import { basename } from 'path';
import { BaseComponent } from 'src/shared/components/base-component';
import { DTO_ExamReportPublic } from 'src/shared/models/dto-exam-report-public';
import { ReportsService } from 'src/shared/services/reports.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent extends BaseComponent implements OnInit {


  public reports:DTO_ExamReportPublic[] = [];

  constructor(private reportService:ReportsService) {
    super();
   }

  ngOnInit(): void {

    this.reportService.getBySession().subscribe(response => {
      this.ShowNotifications(response);

      if(response.isValid){
        this.reports = <DTO_ExamReportPublic[]> response.responseData;
      }
    });

    for(let i = 0; i < 5; i++){
      let report = new DTO_ExamReportPublic();
      report.examName = "Biopsia " + i;
      report.examDate = new Date();
      report.uploadDate = new Date();
      this.reports.push(report);
    }
  }

}
