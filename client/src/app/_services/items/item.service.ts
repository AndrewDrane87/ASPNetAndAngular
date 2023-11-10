import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ItemPhoto } from '../../_models/itemPhoto';
import { Observable, Subscriber, of } from 'rxjs';
import {
  NewArmor,
  NewBoots,
  NewHandItem,
  NewHelmet,
} from '../../_models/newItems/newItems';
import {
  Armor,
  Boots,
  HandItem,
  Helmet,
  PlayerCharacter,
} from '../../_models/playerCharacters/playerCharacter';
import { Item } from 'src/app/_models/item';

@Injectable({
  providedIn: 'root',
})
export class ItemService {
  baseUrl = environment.apiUrl;
  itemPhotos: ItemPhoto[] | undefined;
  items: Item[] = [];
  helmets: Item[] = [];
  armor: Item[] = [];
  boots: Item[] = [];
  constructor(private http: HttpClient) {}

  checkIfPhotosLoaded() {
    return this.itemPhotos !== undefined ? true : false;
  }

  checkIfItemsLoaded() {
    return this.items.length > 0 ? true : false;
  }

  checkIfHelmetsLoaded() {
    return this.helmets.length > 0 ? true : false;
  }

  checkIfArmorLoaded() {
    return this.armor.length > 0 ? true : false;
  }

  checkIfBootsLoaded() {
    return this.boots.length > 0 ? true : false;
  }

  loadItemPhotos() {
    var url = this.baseUrl + 'items/get-item-photos';
    return this.http.get<ItemPhoto[]>(url);
  }

  loadHelmets() {
    const url = this.baseUrl + 'items/get?itemType=helmet';
    return this.http.get<Item[]>(url);
  }

  getItems() {
    return this.items;
  }

  loadItems() {
    const url = this.baseUrl + 'items/get?itemType=hand';
    console.log('loading items');
    return this.http.get<Item[]>(url);
  }

  loadArmor() {
    const url = this.baseUrl + 'items/get?itemType=armor';
    return this.http.get<Item[]>(url);
  }

  loadBoots() {
    const url = this.baseUrl + 'items/get?itemType=boots';
    return this.http.get<Item[]>(url);
  }

  getItemPhotos(itemType: string) {
    return this.itemPhotos;
  }

  deletePhoto(id: number) {
    var url = this.baseUrl + 'items/delete-item-photo/' + id;
    return this.http.delete(url);
  }

  createHelmet(item: NewHelmet) {
    var url = this.baseUrl + 'items/create';
    console.log('Create Helmet: ' + item);
    return this.http.post<Item>(url, item);
  }

  createHandItem(item: NewHandItem) {
    var url = this.baseUrl + 'items/create-hand-item';
    if (item.attackValue === null) item.attackValue = 0;
    if (item.armorValue === null) item.armorValue = 0;
    console.log(item);
    return this.http.post<Item>(url, item);
  }

  createArmor(item: NewArmor) {
    var url = this.baseUrl + 'items/create';
    item.itemType = 'armor';
    console.log('Create Armor: ' + item);
    return this.http.post<Item>(url, item);
  }

  createBoots(item: NewBoots) {

    var url = this.baseUrl + 'items/create';
    item.itemType = 'boots';
    console.log('Create boots: ' + item);
    return this.http.post<Item>(url, item);
  }

  deleteHelmet(helmet: Item): Promise<Boolean> {
    var url = this.baseUrl + 'items/delete-helmet/' + helmet.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.helmets!.indexOf(helmet, 0);
          if (index > -1) {
            this.helmets!.splice(index, 1);
          }
          resolve(true);
        },
        error: (error) => {
          console.log(error);
          resolve(false);
        },
      });
    });
  }

  deleteArmor(armor: Item): Promise<Boolean> {
    var url = this.baseUrl + 'items/delete-armor/' + armor.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.armor!.indexOf(armor, 0);
          if (index > -1) {
            this.armor!.splice(index, 1);
          }
          resolve(true);
        },
        error: (error) => {
          console.log(error);
          resolve(false);
        },
      });
    });
  }

  deleteBoot(boot: Item): Promise<Boolean> {
    var url = this.baseUrl + 'items/delete-boots/' + boot.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.boots!.indexOf(boot, 0);
          if (index > -1) {
            this.boots!.splice(index, 1);
          }
          resolve(true);
        },
        error: (error) => {
          console.log(error);
          resolve(false);
        },
      });
    });
  }

  deleteHandItem(item: Item): Promise<Boolean> {
    var url = this.baseUrl + 'items/delete-hand-item/' + item.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.items!.indexOf(item, 0);
          if (index > -1) {
            this.items!.splice(index, 1);
          }
          resolve(true);
        },
        error: (error) => {
          console.log(error);
          resolve(false);
        },
      });
    });
  }

  getAvailableHelmets(character: PlayerCharacter) {
    var url =
      this.baseUrl +
      `playercharacters/get-available-items?type=helmets&characterId=${character.id}`;
    return this.http.get<Item[]>(url);
  }

  getAvailableHandItems(character: PlayerCharacter) {
    var url =
      this.baseUrl +
      `playercharacters/get-available-items?type=hand-items&characterId=${character.id}`;
    return this.http.get<Item[]>(url);
  }
  getAvailableArmor(character: PlayerCharacter) {
    var url =
      this.baseUrl +
      `playercharacters/get-available-items?type=armor&characterId=${character.id}`;
    return this.http.get<Item[]>(url);
  }
  getAvailableBoots(character: PlayerCharacter) {
    var url =
      this.baseUrl +
      `playercharacters/get-available-items?type=boots&characterId=${character.id}`;
    return this.http.get<Item[]>(url);
  }

  setCharacterItem(itemType: string, itemId: number, charcaterId: number) {
    var body = { itemType: itemType, itemId: itemId, characterId: charcaterId };
    console.log(body);
    var url = this.baseUrl + `playercharacters/set-character-item`;
    return this.http.put(url, body);
  }
}
