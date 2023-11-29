import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PlayerCharacter } from 'src/app/_models/playerCharacters/playerCharacter';
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
  currentItemId = -1;
  cost = 0;

  constructor(
    private itemService: ItemService,
    public bsModalRef: BsModalRef,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  setItemType(type: string) {
    switch (type) {
      case 'helmets':
        this.itemService
          .getAvailableHelmets(this.character!, this.currentItemId)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
      case 'leftHand':
        this.itemService
          .getAvailableHandItems(this.character!, this.currentItemId)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
      case 'rightHand':
        this.itemService
          .getAvailableHandItems(this.character!, this.currentItemId)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
      case 'armor':
        this.itemService
          .getAvailableArmor(this.character!, this.currentItemId)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
      case 'boots':
        this.itemService
          .getAvailableBoots(this.character!, this.currentItemId)
          .subscribe({
            next: (results) => {
              this.availableItems = results;
              console.log(results);
            },
          });

        break;
      case 'backpack':
        this.itemService
          .getAvailable(this.character!, this.currentItemId)
          .subscribe({ next: (results) => (this.availableItems = results) });
        break;
    }
  }

  selectItem(item: any) {
    this.selectedItem = item;
    this.result = true;
    this.bsModalRef.hide();
  }

  purchaseItem(item: any) {
    console.log('Character gold: ' + this.character!.gold)
    if (this.character!.gold < item.cost) {
      this.toastr.warning("You dont have enough gold sucka!!!")
    } else {
      this.selectedItem = item;
      this.result = true;
      this.cost = item.cost;
      this.bsModalRef.hide();
    }
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
