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

    protected _needUser: boolean = true;

    public _currentUser: AuthUser = CurrentUser.getUser();
    public isLogged: boolean = false;


    constructor(public needUser: boolean = true) {
        const injector = AppInjector.getInjector()

        this._toastrService = injector.get(ToastrService);
        this._loginService = injector.get(AuthService);
        this._router = injector.get(Router);
        this._needUser = needUser;


        this.validateUser();
        setInterval(() => {
            this.isLogged = CurrentUser.getUser().id > 0;
        }, 100);
    }


    async validateUser(forceCheck: boolean = false) {
        if (this._needUser || forceCheck) {

            try {
                let session = localStorage.getItem('Session');

                if (!!session) {

                    const validateSession = await this._loginService.validate().toPromise();

                    if (this.invalidResponse(<BaseResponse<boolean>>validateSession)) {
                        this._router.navigateByUrl('/home');
                        return;
                    }

                    const userResponse = await this._loginService.getSessionUser().toPromise();
                    if (this.invalidResponse(<BaseResponse<AuthUser>>userResponse)) {
                        this._router.navigateByUrl('/home');
                        return;
                    }

                    CurrentUser.setUser(<AuthUser>userResponse?.responseData);
                    this._currentUser  = CurrentUser.getUser();

                    this.isLogged = CurrentUser.getUser().id > 0;

                    if (!this.isLogged)
                        this._router.navigateByUrl('/home');
                }
                else {
                    this._router.navigateByUrl('/home');
                }
            }
            catch (ex) {
                CurrentUser.setUser(new AuthUser());
                this._router.navigateByUrl('/home');
                this.handleHttpError();
            }
        }
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
