import { Component, OnInit } from '@angular/core';
import { basename } from 'path';
import { BaseComponent } from 'src/shared/components/base-component';
import { DTO_ExamReportPublic } from 'src/shared/models/dto-exam-report-public';
import { BlobToB64Service } from 'src/shared/services/blob-to-b64.service';
import { ReportsService } from 'src/shared/services/reports.service';
import { Buffer } from 'buffer';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent extends BaseComponent implements OnInit {


  public reports: DTO_ExamReportPublic[] = [];

  constructor(private reportService: ReportsService, private blobToB64service: BlobToB64Service) {
    super();
  }

  ngOnInit(): void {

    this.reportService.getBySession().subscribe(response => {
      this.ShowNotifications(response);
      if (response.isValid) {
        this.reports = <DTO_ExamReportPublic[]>response.responseData;
      }
    });
  }

  downloadFile(report: DTO_ExamReportPublic) {

    const byteCharacters = atob(report.fileSource);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const aux = new Uint8Array(byteNumbers);
    
    const data = new Blob([aux],
      { type: 'application/pdf' });

    let fileUrl = window.URL.createObjectURL(data);
    let a: any = document.createElement('a');


    a.href = fileUrl
    a.setAttribute('download', report.examName);
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(fileUrl);

  }
}
