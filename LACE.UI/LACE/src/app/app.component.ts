import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CurrentUser } from 'src/shared/components/base-component';
import { AuthUser } from 'src/shared/models/auth-user';
import { BaseResponse } from 'src/shared/models/base-response';
import { MessageTypeEnum } from 'src/shared/models/message-type-enum';
import { AppInjector } from 'src/shared/services/app-injector.service';
import { AuthService } from 'src/shared/services/auth-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'LACE';

  public _toastrService: ToastrService
  public _loginService: AuthService;
  public _router: Router;
  
  constructor() {
    const injector = AppInjector.getInjector()

    this._toastrService = injector.get(ToastrService);
    this._loginService = injector.get(AuthService);
    this._router = injector.get(Router);
  }
  ngOnInit(): void {
    if (localStorage.getItem('NDToken')) {
      this._loginService.getSessionUser().subscribe(response => {

          if (this.invalidResponse(response)) {
              localStorage.removeItem("NDToken");
              return;
          }

          CurrentUser.setUser(<AuthUser>response.responseData);

          console.log(CurrentUser.getUser());


      }, err => {
          if (err.status = 401) {
              this.ShowNotifications(<BaseResponse<unknown>>err.error);
              return;
          }
          this.handleHttpError();
      })
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
