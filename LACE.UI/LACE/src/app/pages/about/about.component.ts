import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/shared/components/base-component';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent extends BaseComponent implements OnInit {

  constructor() {
    super(false);
   }

  ngOnInit(): void {
  }

}
