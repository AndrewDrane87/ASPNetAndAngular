import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {
  HandItem,
  Helmet,
  PlayerCharacter,
} from 'src/app/_models/playerCharacters/playerCharacter';
import { ItemService } from 'src/app/_services/items/item.service';

@Component({
  selector: 'app-item-selector',
  templateUrl: './item-selector.component.html',
  styleUrls: ['./item-selector.component.css'],
})
export class ItemSelectorComponent implements OnInit {
  availableItems: any[] | undefined;
  result = false;
  character: PlayerCharacter | undefined;
  selectedItem: any | undefined;

  constructor(
    private itemService: ItemService,
    public bsModalRef: BsModalRef
  ) {}

  ngOnInit(): void {}

  setItemType(type: string) {
    switch (type) {
      case 'helmets':
        this.itemService
          .getAvailableHelmets(this.character!)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
        case 'leftHand':
          this.itemService
          .getAvailableHandItems(this.character!)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
        case 'rightHand':
          this.itemService
          .getAvailableHandItems(this.character!)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
        case 'armor':
          this.itemService
          .getAvailableArmor(this.character!)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
        case 'boots':
          this.itemService
          .getAvailableBoots(this.character!)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
    }
  }

  selectItem(item: any) {
    this.selectedItem = item;
    this.result = true;
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
