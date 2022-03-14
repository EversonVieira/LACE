import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';
import { AuthUser, DTO_AuthUser_Register } from 'src/shared/models/auth-user';
import { UserService } from 'src/shared/services/user.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent extends BaseComponent implements OnInit {

  public userForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    cpf: new FormControl(''),
    rg: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl('')
  });

  constructor(private _userService: UserService) {
    super(false);
  }

  ngOnInit(): void {
  }

  onSubmit() {
    const authUser: DTO_AuthUser_Register = new DTO_AuthUser_Register();
    this.formGroupToModel(authUser);

    if (authUser.password !== authUser.confirmPassword) {
      this._toastrService.warning("Senhas não correspondem.");
      return;
    }

    this._userService.create(authUser).subscribe(response => {
      if (response.isValid) {
        this._toastrService.info("Usuário cadastrado com sucesso.");
      }
      this.ShowNotifications(response);
    }, error => {
      this.handleHttpError();
    });

  }

  private formGroupToModel(authUser: DTO_AuthUser_Register) {
    authUser.name = this.userForm.get('name')?.value;
    authUser.cpf = this.userForm.get('cpf')?.value;
    authUser.rg = this.userForm.get('rg')?.value;
    authUser.email = this.userForm.get('email')?.value;
    authUser.password = this.userForm.get('password')?.value;
    authUser.confirmPassword = this.userForm.get('confirmPassword')?.value;
  }
}
