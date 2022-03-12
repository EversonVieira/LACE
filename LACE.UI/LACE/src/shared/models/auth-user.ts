import { BaseModel } from "./base-model";

export class AuthUser extends BaseModel {
    name:string = '';
    email:string = '';
    password:string = '';
    cpf:string = '';
    rg:string = '';
}

export class DTO_ExamReportPublic {
    sourcePatientId: string = '';
    sourceExamId: string = '';
    patientCpf: string = '';
    patientRG: string = '';
    fileName: string = '';
    fileExtension: string = '';
    fileSource: string = '';
    examDate:Date = new Date();
    uploadDate:Date = new Date();
}