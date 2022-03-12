import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent extends BaseComponent implements OnInit {

  public userForm:FormGroup = new FormGroup({
    name: new FormControl(''),
    cpf: new FormControl(''),
    rg: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassowrd: new FormControl('')
  });

  constructor() { 
    super(false);
  }

  ngOnInit(): void {
  }

}
