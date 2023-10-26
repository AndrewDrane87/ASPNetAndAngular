import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-item-type-selector',
  templateUrl: './item-type-selector.component.html',
  styleUrls: ['./item-type-selector.component.css'],
})
export class ItemTypeSelectorComponent {
  @Output() setItemTypeEvent = new EventEmitter();
  itemType: string = 'Select Item Type';

  setItemType(itemType: string) {
    this.itemType = itemType;
    this.setItemTypeEvent.emit(this.itemType);
  }
}
