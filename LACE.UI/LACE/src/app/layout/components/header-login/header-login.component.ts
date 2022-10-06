import { Component, OnInit } from '@angular/core';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';
import { AuthUser } from 'src/shared/models/auth-user';

@Component({
  selector: 'app-header-login',
  templateUrl: './header-login.component.html',
  styleUrls: ['./header-login.component.scss']
})
export class HeaderLoginComponent extends BaseComponent implements OnInit {

  constructor() {
    super();
   }

  ngOnInit(): void {
  }

  async doLogout(){


    try{
      localStorage.removeItem("NDToken");
      CurrentUser.setUser(new AuthUser());
    }
    catch{
      this.handleHttpError();
    }
  }
}
