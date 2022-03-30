import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';
import { AuthSession } from 'src/shared/models/auth-session';
import { AuthUser } from 'src/shared/models/auth-user';
import { BaseResponse } from 'src/shared/models/base-response';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {

  public userForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });



  constructor() {
    super(false);
  }

  ngOnInit(): void {
  }

  async onSubmit() {
    const authUser: AuthUser = new AuthUser();
    authUser.email = this.userForm.get('email')?.value;
    authUser.password = this.userForm.get('password')?.value;

    if(CurrentUser.getUser().id > 0){
      this._toastrService.warning('Saia para entrar com outra conta.')
      return;
    }

    try {
      this._loginService.login(authUser).subscribe(async loginResponse => {
        if (this.invalidResponse(<BaseResponse<AuthSession>>loginResponse)) {
          return;
        }

        localStorage.setItem("Session", loginResponse?.responseData?.sessionKey || '');
        await this.validateUser(true);

        if (this.isLogged)
          this._router.navigateByUrl("/reports");
      }, err => {
        if (err.status = 401) {
          this.ShowNotifications(<BaseResponse<unknown>>err.error);
          return;
        }
        this.handleHttpError();

      })
    }
    catch {
      this.handleHttpError();
    }
  }
}
