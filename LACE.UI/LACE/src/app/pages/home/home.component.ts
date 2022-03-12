import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BaseComponent } from 'src/shared/components/base-component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit  {

  public userForm:FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  

  constructor() {
    super(false);
   }

  ngOnInit(): void {
  }

  
}
