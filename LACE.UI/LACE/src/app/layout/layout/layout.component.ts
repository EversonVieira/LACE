import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  public isExpanded:boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

  toggleMenu(){
    this.isExpanded = !this.isExpanded;
  }

}
