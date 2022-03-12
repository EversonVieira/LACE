import { HttpHeaders } from '@angular/common/http';
import { Enviroment } from "./Enviroment";


export class HttpConfiguration {
  AccessControlAllowOrigin = '*';
  AccessControlAllowMethods = 'GET,POST,OPTIONS,DELETE,PUT';
  AccessControlAllowHeaders = "Origin, X-Requested-With, Content-Type, Accept, x-client-key, x-client-token, x-client-secret, Authorization";
  ContentType = "application/json";
  NDAuth = sessionStorage.getItem('NDAuth') || '';
  enviroment: Enviroment = new Enviroment();
}
