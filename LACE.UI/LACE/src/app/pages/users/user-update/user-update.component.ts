import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';

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

  constructor() {
    super();
   }

  ngOnInit(): void {
  }

}
