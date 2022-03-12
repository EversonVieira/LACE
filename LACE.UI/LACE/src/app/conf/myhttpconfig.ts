import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpConfiguration } from "src/shared/models/HttpConfiguration";

@Injectable()
export class Myhttpconfig extends HttpConfiguration {
    constructor() {
        super();
        
        if (!environment.production) {
            this.enviroment.baseUrl = "https://localhost:7077/api/";
        }
        else {
            this.enviroment.baseUrl = "http://munisaude.eastus.cloudapp.azure.com:4040/api/";
        }

    }
}
