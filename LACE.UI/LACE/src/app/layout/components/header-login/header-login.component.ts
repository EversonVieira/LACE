import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/shared/components/base-component';

@Component({
  selector: 'app-header-login',
  templateUrl: './header-login.component.html',
  styleUrls: ['./header-login.component.scss']
})
export class HeaderLoginComponent extends BaseComponent implements OnInit {

  constructor() {
    super(false);
   }

  ngOnInit(): void {
  }

  doLogout(){
    
  }
}
