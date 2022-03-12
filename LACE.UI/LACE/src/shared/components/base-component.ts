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
    public _hasLoggedUser: boolean = false;

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


    validateUser() {
        if (this._needUser) {

            let session = localStorage.getItem('Session');

            if (!!session) {

                this._loginService.validate().subscribe(response => {

                    if (this.invalidResponse(response)) {
                        this._router.navigateByUrl('/home');
                        CurrentUser.setUser(new AuthUser());
                        return;
                    }

                    this._loginService.getSessionUser().subscribe(userResponse => {
                        if (this.invalidResponse(response)) {
                            this._router.navigateByUrl('/home');
                            CurrentUser.setUser(new AuthUser());
                            return;
                        }

                        if (userResponse.isValid) {
                            CurrentUser.setUser(<AuthUser>userResponse.responseData);
                            this._hasLoggedUser = true;
                        }
                        else {
                            this._router.navigateByUrl('/home');
                            CurrentUser.setUser(new AuthUser());
                            return;
                        }
                    });

                });
            }
            else {
                this._router.navigateByUrl('/home');
            }
        }
    }

    invalidResponse<T>(response: BaseResponse<T>): boolean {
        if (response.isValid) return false;

        this.ShowNotifications(response);

        return true;
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

}

export class CurrentUser {

    private static authUser: AuthUser = new AuthUser();


    static setUser(user: AuthUser) {
        Object.assign(this.authUser, user);
    }

    static getUser() {
        return this.authUser;
    }
}
