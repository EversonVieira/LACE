import { BaseModel } from "./base-model";
import { Role } from "./role";

export class AuthUser extends BaseModel {
    name:string = '';
    email:string = '';
    password:string = '';
    cpf:string = '';
    rg:string = '';
    sus: string = '';
    roles:Role[] = [];
    isActive:boolean = false;
    isLocked:boolean = false;
}

export class DTO_AuthUser_Register extends AuthUser{
    confirmPassword:string = '';
}

export class DTO_AuthUser_Update extends DTO_AuthUser_Register{
    oldPassword:string = '';
}

// export class DTO_ExamReportPublic {
//     sourcePatientId: string = '';
//     sourceExamId: string = '';
//     patientCpf: string = '';
//     patientRG: string = '';
//     fileName: string = '';
//     fileExtension: string = '';
//     fileSource: string = '';
//     examDate:Date = new Date();
//     uploadDate:Date = new Date();
// }
