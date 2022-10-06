import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';
import { AuthUser } from 'src/shared/models/auth-user';
import { BaseResponse } from 'src/shared/models/base-response';
import { ReportsService } from 'src/shared/services/reports.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {

  public userForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    examId: new FormControl(''),
    document: new FormControl(''),
    documentType: new FormControl(0),
  });



  constructor(private reportsService:ReportsService) {
    super();
  }

  ngOnInit(): void {
    
  }

  async onDownload(){
    this.reportsService.downloadByExam(
      this.userForm.get('examId')?.value,
      this.userForm.get('document')?.value,
      this.userForm.get('documentType')?.value,
    ).subscribe(response => {

      if (response.responseData){
        let report = response.responseData?.length > 0 ? response.responseData[0] : null;

        if (report){
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
      
    })
  }
 
}
