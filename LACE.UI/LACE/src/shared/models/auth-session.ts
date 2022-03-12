export class AuthSession {
    id:number = 0;
    userId:number = 0;
    sessionKey:string = '';
    lastRenewDate:Date = new Date();
}
