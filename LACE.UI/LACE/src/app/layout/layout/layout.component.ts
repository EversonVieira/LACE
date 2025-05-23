import { Component, OnInit } from '@angular/core';
import { BaseComponent, CurrentUser } from 'src/shared/components/base-component';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent extends BaseComponent implements OnInit {

  public isExpanded:boolean = false;
  constructor() {
    super();
  }

  ngOnInit(): void {
  }

  toggleMenu(){
    this.isExpanded = !this.isExpanded;
  }

}
