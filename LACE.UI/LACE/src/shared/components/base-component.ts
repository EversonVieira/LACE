import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { AuthUser } from "../models/auth-user";
import { BaseResponse } from "../models/base-response";
import { MessageTypeEnum } from "../models/message-type-enum";
import { AppInjector } from "../services/app-injector.service";
import { AuthService } from "../services/auth-service.service";
export class BaseComponent {

    public _toastrService: ToastrService
    public _loginService: AuthService;
    public _router: Router;

    public _currentUser: AuthUser = CurrentUser.getUser();
    public isLogged: boolean = false;


    constructor() {
        const injector = AppInjector.getInjector()

        this._toastrService = injector.get(ToastrService);
        this._loginService = injector.get(AuthService);
        this._router = injector.get(Router);
    }


    

    invalidResponse<T>(response: BaseResponse<T>): boolean {
        this.ShowNotifications(response);
        return !(response.isValid);
    }

    ShowNotifications<T>(response: BaseResponse<T>) {
        response.messages.forEach(element => {

            switch (element.messageType) {
                case MessageTypeEnum.Information:
                    this._toastrService.info(element.text);
                    break;
                case MessageTypeEnum.Caution:
                case MessageTypeEnum.Validation:
                case MessageTypeEnum.Warning:
                    this._toastrService.warning(element.text);
                    break;

                case MessageTypeEnum.Error:
                case MessageTypeEnum.Exception:
                case MessageTypeEnum.FatalError:
                    this._toastrService.error(element.text);
                    break;

                default:
                    this._toastrService.show(element.text);
            }
        });
    }

    handleHttpError() {
        this._toastrService.error("Não foi possível estabelecer conexão com o serviço, tente novamente mais tarde.");
    }

}

export class CurrentUser {

    private static authUser: AuthUser = new AuthUser();


    static setUser(user: AuthUser) {
        Object.assign(this.authUser, user);
    }

    static getUser():AuthUser {
        return this.authUser;
    }
}
