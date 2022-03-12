import { DTO_ExamReportPublic } from "./dto-exam-report-public";

export class ExamReport extends DTO_ExamReportPublic {
    userId:number = 0;
    filePath:string = '';
}
