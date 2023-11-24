import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { ItemPhoto } from 'src/app/_models/itemPhoto';
import { Pagination } from 'src/app/_models/pagination';
import {
} from 'src/app/_models/playerCharacters/playerCharacter';
import { ItemService } from 'src/app/_services/items/item.service';

@Component({
  selector: 'app-item-manager',
  templateUrl: './item-manager.component.html',
  styleUrls: ['./item-manager.component.css'],
})
export class ItemManagerComponent{
  
  pagination?: Pagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(public itemService: ItemService, private toastr: ToastrService) {}

  

  helmetsOpen(event: any) {
    if (event) {
      if (this.itemService.checkIfHelmetsLoaded() === false) {
        this.itemService
          .loadHelmets()
          .subscribe({
            next: (helmets) => (this.itemService.helmets = helmets),
          });
      }
    }
  }

  handItemsOpen(event: any) {
    if (this.itemService.checkIfItemsLoaded() === false) {
      this.itemService
        .loadItems()
        .subscribe({ next: (items) => (this.itemService.items = items) });
    }
  }

  armorOpen(event: any) {
    if (this.itemService.checkIfArmorLoaded() === false) {
      this.itemService
        .loadArmor()
        .subscribe({ next: (armor) => (this.itemService.armor = armor) });
    }
  }

  bootsOpen(event: any) {
    if (this.itemService.checkIfBootsLoaded() === false) {
      this.itemService
        .loadBoots()
        .subscribe({ next: (boots) => (this.itemService.boots = boots) });
    }
  }

  

  

  deleteHandItem(item: Item) {
    this.itemService.deleteHandItem(item).then((result) => {
      if (result) {
        this.toastr.success('Deleted Item');
      } else {
        this.toastr.warning('Failed to delete item');
      }
    });
  }

  deleteHelmet(helmet: Item) {
    this.itemService.deleteHelmet(helmet).then((result) => {
      if (result) {
        this.toastr.success('Deleted Item');
      } else {
        this.toastr.warning('Failed to delete item');
      }
    });
  }

  deleteArmor(armor: Item) {
    this.itemService.deleteArmor(armor).then((result) => {
      if (result) {
        this.toastr.success('Deleted Item');
      } else {
        this.toastr.warning('Failed to delete item');
      }
    });
  }

  deleteBoot(boot: Item) {
    this.itemService.deleteBoot(boot).then((result) => {
      if (result) {
        this.toastr.success('Deleted Item');
      } else {
        this.toastr.warning('Failed to delete item');
      }
    });
  }
}
