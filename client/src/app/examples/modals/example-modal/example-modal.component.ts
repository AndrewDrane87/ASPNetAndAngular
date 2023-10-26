import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ItemService } from 'src/app/_services/items/item.service';

@Component({
  selector: 'app-example-modal',
  templateUrl: './example-modal.component.html',
  styleUrls: ['./example-modal.component.css']
})
export class ExampleModalComponent {
  //@Input() itemType = '';
  result = false;
  
  constructor(
    public bsModalRef: BsModalRef,
    private itemService: ItemService
  ) {}

  submit() {
    this.result = true;
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
