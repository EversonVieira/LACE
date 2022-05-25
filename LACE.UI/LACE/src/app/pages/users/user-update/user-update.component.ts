import { AfterContentInit, AfterViewChecked, AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';
import { DTO_AuthUser_Register, DTO_AuthUser_Update } from 'src/shared/models/auth-user';
import { UserService } from 'src/shared/services/user.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent extends BaseComponent implements OnInit {

  public userForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    cpf: new FormControl(''),
    rg: new FormControl(''),
    sus: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    oldPassword: new FormControl('')
  });

  constructor(private _userService: UserService) {
    super();
  }

  ngOnInit(): void {
    this.init();
  }

  async init(){
    await this.validateUser();
    this.applyCurrentUserToFormGroup();
  }

  onSubmit() {
    const authUser: DTO_AuthUser_Update = new DTO_AuthUser_Update();
    this.formGroupToModel(authUser);


    if (authUser.password !== authUser.confirmPassword) {
      this._toastrService.warning("Senhas não correspondem");
      return;
    }

    this._userService.update(authUser).subscribe(response => {
      if (response.isValid) {
        this._toastrService.info("Usuário atualizado com sucesso");
      }
      this.ShowNotifications(response);
    }, error => {
      this.handleHttpError();
    });

  }

  async applyCurrentUserToFormGroup() {
    let applied:Boolean = false;

    while(!applied){
      this.userForm.get('name')?.setValue(CurrentUser.getUser().name);
      this.userForm.get('cpf')?.setValue(CurrentUser.getUser().cpf);
      this.userForm.get('rg')?.setValue(CurrentUser.getUser().rg);
      this.userForm.get('sus')?.setValue(CurrentUser.getUser().sus);
      this.userForm.get('email')?.setValue(CurrentUser.getUser().email);
      applied = true;
    }
  }

  private formGroupToModel(authUser: DTO_AuthUser_Update) {
    console.log(this.userForm);
    authUser.id = this._currentUser.id;
    authUser.name = this.userForm.get('name')?.value;
    authUser.cpf = this.userForm.get('cpf')?.value;
    authUser.rg = this.userForm.get('rg')?.value;
    authUser.sus = this.userForm.get('sus')?.value;
    authUser.email = this.userForm.get('email')?.value;
    authUser.oldPassword = this.userForm.get('oldPassword')?.value;
    authUser.password = this.userForm.get('password')?.value;
    authUser.confirmPassword = this.userForm.get('confirmPassword')?.value;
  }
}
