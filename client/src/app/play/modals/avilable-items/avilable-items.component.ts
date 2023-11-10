import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Item } from 'src/app/_models/item';

@Component({
  selector: 'app-avilable-items',
  templateUrl: './avilable-items.component.html',
  styleUrls: ['./avilable-items.component.css']
})
export class AvilableItemsComponent {
  items: Item[] = [];
constructor(public bsModalRef: BsModalRef){}
  
close(){
    this.bsModalRef.hide();
  }
}
