import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';
import { AuthUser } from 'src/shared/models/auth-user';
import { BaseResponse } from 'src/shared/models/base-response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

  public userForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    examId: new FormControl(''),
    document: new FormControl(''),
    documentType: new FormControl(0),
  });

  constructor(private router:Router) {
    super();
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
      this._loginService.login(authUser).subscribe( async loginResponse => {
        if (this.invalidResponse(<BaseResponse<string>>loginResponse)) {
          return;
        }

        localStorage.setItem("NDToken", loginResponse?.responseData || '');

        this._loginService.getSessionUser().subscribe(response => {

          if (this.invalidResponse(response)){
            localStorage.removeItem("NDToken");
            return;
          }

          CurrentUser.setUser(<AuthUser> response.responseData);

          this.router.navigateByUrl('home');

        }, err => {
          if (err.status = 401) {
            this.ShowNotifications(<BaseResponse<unknown>>err.error);
            return;
          }
          this.handleHttpError();
        })

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
