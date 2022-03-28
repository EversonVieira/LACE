import { DTO_ExamReportPublic } from "src/shared/models/dto-exam-report-public";

export class ExamReport extends DTO_ExamReportPublic {
    userId:number = 0;
    filePath:string = '';
}
