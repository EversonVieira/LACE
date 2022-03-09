import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'sm-item-button',
  templateUrl: './item-button.component.html',
  styleUrls: ['./item-button.component.scss']
})
export class ItemButtonComponent implements OnInit {

  @Input()
  public Text:string = '';

  @Input()
  public TextVisible:boolean = false;

  @Input()
  public Route:string|undefined = '';

  @Input()
  public Class:string = '';

  @Input()
  public RouteActive:string= 'active';

  @Input()
  public IgnoreRoute:boolean = false;

  @Output()
  public OnClick:EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit(): void {
  }

  onClick(){
    this.OnClick.emit(true);
  }
}
