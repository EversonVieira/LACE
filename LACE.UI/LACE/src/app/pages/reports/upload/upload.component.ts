import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';
import { DTO_ExamReportPublic } from 'src/shared/models/dto-exam-report-public';
import { ReportsService } from 'src/shared/services/reports.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent extends BaseComponent implements OnInit {

  public uploadForm: FormGroup = new FormGroup({
    sourcePatientId: new FormControl(''),
    sourceExamId: new FormControl(''),
    examName: new FormControl(''),
    patientCpf: new FormControl(''),
    patientRG: new FormControl(''),
    patientSUS: new FormControl(''),
    examDate: new FormControl(''),
  });

  private file?: File = undefined;
  private fileSource: string = '';
  constructor(private reportService:ReportsService) {
    super();
  }

  ngOnInit(): void {
  }

  selectFile(event: any) {
    
    this.file = <File>event.target.files[0];
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.fileSource = event.target.result;
      }
      reader.readAsDataURL(event.target.files[0]);

    }
  }

  uploadFile() {
    let dto_ExamReport:DTO_ExamReportPublic = this.fromGroup();


    this.reportService.create(dto_ExamReport).subscribe(r => {
      console.log(r);
    })
  }

  fromGroup():DTO_ExamReportPublic {
    const obj:DTO_ExamReportPublic = new DTO_ExamReportPublic();

    obj.fileExtension = this.file?.name?.split('.')[1] || '';
    obj.examName = this.uploadForm.get('examName')?.value;
    obj.fileSource = this.fileSource;
    obj.sourceExamId = this.uploadForm.get('sourceExamId')?.value;
    obj.sourcePatientId = this.uploadForm.get('sourcePatientId')?.value;
    obj.patientCpf = this.uploadForm.get('patientCpf')?.value;
    obj.patientRG = this.uploadForm.get('patientRG')?.value;
    obj.examDate = this.uploadForm.get('examDate')?.value;
    obj.uploadDate = new Date();

    return obj;
  }
}
