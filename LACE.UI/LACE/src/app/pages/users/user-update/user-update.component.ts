import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';
import { DTO_AuthUser_Register, DTO_AuthUser_Update } from 'src/shared/models/auth-user';
import { UserService } from 'src/shared/services/user.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent extends BaseComponent implements OnInit {

  public userForm:FormGroup = new FormGroup({
    name: new FormControl(''),
    cpf: new FormControl(''),
    rg: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassowrd: new FormControl(''),
    oldPassowrd: new FormControl('')
  });

  constructor(private _userService: UserService) {
    super();
   }

  ngOnInit(): void {
  }

  onSubmit() {
    const authUser: DTO_AuthUser_Update = new DTO_AuthUser_Update();
    this.formGroupToModel(authUser);

    if (authUser.password !== authUser.confirmPassword) {
      this._toastrService.warning("Senhas não correspondem");
      return;
    }

    this._userService.create(authUser).subscribe(response => {
      if (response.isValid) {
        this._toastrService.info("Usuário cadastrado com sucesso");
      }
      this.ShowNotifications(response);
    }, error => {
      this.handleHttpError();
    });

  }

  private formGroupToModel(authUser: DTO_AuthUser_Update) {
    authUser.name = this.userForm.get('name')?.value;
    authUser.cpf = this.userForm.get('cpf')?.value;
    authUser.rg = this.userForm.get('rg')?.value;
    authUser.email = this.userForm.get('email')?.value;
    authUser.oldPassword = this.userForm.get('oldPassword')?.value;
    authUser.password = this.userForm.get('password')?.value;
    authUser.confirmPassword = this.userForm.get('confirmPassword')?.value;
  }
}
