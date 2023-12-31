import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PlayerCharacter } from 'src/app/_models/playerCharacters/playerCharacter';
import { ItemSelectorComponent } from '../item-selector/item-selector.component';
import { ItemService } from 'src/app/_services/items/item.service';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { BackpackComponent } from 'src/app/play/modals/backpack/backpack.component';
import { PlayerAttackComponent } from 'src/app/play/modals/player-attack/player-attack.component';
import { PlayerCharactersService } from 'src/app/_services/player-characters.service';
import { skip } from 'rxjs';

@Component({
  selector: 'app-character-display',
  templateUrl: './character-display.component.html',
  styleUrls: ['./character-display.component.css', '../../app.component.css'],
})
export class CharacterDisplayComponent implements OnInit {
  @Input() character: PlayerCharacter | undefined;
  totalArmor = 0;
  currentHealth = 10;
  totalHealth = 10;
  modalRef: BsModalRef | undefined;
  baseSkillStat = 25;
  fighterStat = 0;
  rogueStat = 0;
  healerStat = 0;
  mageStat = 0;
  bardStat = 0;

  constructor(
    private modalService: BsModalService,
    private pcService: PlayerCharactersService,
    private itemService: ItemService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.calculateTotalArmor();
    this.calculateStatModifiers();
  }

  setCharacter(character: PlayerCharacter) {
    this.character = character;
    this.calculateTotalArmor();
    this.calculateStatModifiers();
  }

  incrementHealth() {
    this.character!.currentHitpoints = this.character!.currentHitpoints + 1;
    if (this.character!.currentHitpoints > this.character!.maxHitpoints)
      this.character!.currentHitpoints = this.character!.currentHitpoints;
  }

  decrementHealth() {
    this.character!.currentHitpoints = this.character!.currentHitpoints - 1;
    if (this.character!.currentHitpoints < 0)
      this.character!.currentHitpoints = 0;
  }

  calculateTotalArmor() {
    this.totalArmor = 0;
    if (this.character?.helmet)
      this.totalArmor += this.character.helmet.armorValue;
    if (this.character?.leftHand)
      this.totalArmor += this.character.leftHand.armorValue;
    if (this.character?.body) this.totalArmor += this.character.body.armorValue;
    if (this.character?.rightHand)
      this.totalArmor += this.character.rightHand.armorValue;
    if (this.character?.feet) this.totalArmor += this.character.feet.armorValue;
  }

  calculateStatModifiers() {
    this.fighterStat = this.baseSkillStat;
    this.rogueStat = this.baseSkillStat;
    this.healerStat = this.baseSkillStat;
    this.mageStat = this.baseSkillStat;
    this.bardStat = this.baseSkillStat;

    if (this.character?.helmet) {
      this.processJsonModifier(this.character.helmet.statModifiers);
    }

    if (this.character?.leftHand) {
      this.processJsonModifier(this.character.leftHand.statModifiers);
    }
    if (this.character?.body) {
      this.processJsonModifier(this.character.body.statModifiers);
    }
    if (this.character?.rightHand) {
      this.processJsonModifier(this.character.rightHand.statModifiers);
    }
    if (this.character?.feet) {
      this.processJsonModifier(this.character.feet.statModifiers);
    }
  }

  processStatModifier(s: string) {
    let split = s.split(':');
    let stat = split[0];
    let value = Number(split[1]);
    switch (stat.toLowerCase()) {
      case 'fighter':
        this.fighterStat += value;
        break;
      case 'rogue':
        this.rogueStat += value;
        break;
      case 'healer':
        this.healerStat += value;
        break;
      case 'mage':
        this.mageStat += value;
        break;
      case 'bard':
        this.bardStat += value;
        break;
    }
  }

  processJsonModifier(s: string) {
    try {
      var json = JSON.parse(s);
      Object.keys(json).forEach((key) => {
        console.log(`${key}: ${json[key]}`);
        const value = json[key];
        switch (key.toLowerCase()) {
          case 'fighter':
            this.fighterStat += value;
            break;
          case 'rogue':
            this.rogueStat += value;
            break;
          case 'healer':
            this.healerStat += value;
            break;
          case 'mage':
            this.mageStat += value;
            break;
          case 'bard':
            this.bardStat += value;
            break;
        }
      });
    } catch {
      return;
    }
  }

  itemSelector(
    itemType: string,
    currentItemId: number = -1,
    backpackIndex: number = -1
  ) {
    this.modalRef = this.modalService.show(ItemSelectorComponent);
    this.modalRef.content.character = this.character;
    this.modalRef.content.currentItemId = currentItemId;
    this.modalRef.content.setItemType(itemType);
    return this.modalRef.onHidden!.subscribe((result) => {
      var selectedItem = this.modalRef?.content.selectedItem;
      let oldItem: Item | undefined;

      if (this.modalRef?.content.result === true) {
        console.log('Selected Item: ' + selectedItem.backpackIndex);
        switch (itemType) {
          case 'helmets':
            oldItem = this.character?.helmet;
            this.character!.helmet = this.modalRef.content.selectedItem;
            break;
          case 'leftHand':
            this.character!.leftHand = this.modalRef.content.selectedItem;
            break;
          case 'armor':
            this.character!.body = this.modalRef.content.selectedItem;
            break;
          case 'rightHand':
            this.character!.rightHand = this.modalRef.content.selectedItem;
            break;
          case 'boots':
            oldItem = this.character?.feet;
            this.character!.feet = this.modalRef.content.selectedItem;
            break;
          case 'backpack':
            for (let i = 0; i < this.character!.backPack!.length; i++) {
              if (
                this.character!.backPack![i] !== null &&
                this.character!.backPack![i] !== undefined
              ) {
                if (
                  this.character!.backPack![i]!.id ===
                  this.modalRef.content.selectedItem.id
                ) {
                  this.character!.backPack![i] = undefined;
                }
              }
            }
            this.character!.backPack![backpackIndex] =
              this.modalRef.content.selectedItem;
            break;
        }

        if (oldItem !== undefined) {
          if (selectedItem.storageIndex >= 0) {
            this.character!.backPack![selectedItem.storageIndex] = oldItem;
          }
        }

        this.itemService
          .setCharacterItem(
            itemType,
            this.modalRef.content.selectedItem.id,
            this.character!.id,
            backpackIndex, this.modalRef.content.selectedItem.cost
          )
          .subscribe({
            next: () => {this.toastr.success('Item selection saved');
            this.character!.gold -= this.modalRef!.content.selectedItem.cost;},
            //error: (error) => this.toastr.error(error),
          });

        this.calculateTotalArmor();
        this.calculateStatModifiers();
      }
    });
  }

  openItemView(item: Item) {
    this.modalRef = this.modalService.show(PlayerAttackComponent);
    this.modalRef.content.setItem(item);
    this.modalRef.content.disableAttack = true;
    this.modalRef.content?.useEvent.subscribe({
      next: (use: string) => {
        this.processItemUsage(item);
      },
    });
    this.modalRef.onHidden?.subscribe({});
  }

  processItemUsage(item: Item) {
    let skipUse = false;
    item.use.split('|').every((use) => {
      const split = use.split(':');
      const name = split[0];
      const value = split[1];
      switch (name) {
        case 'heal':
          if (
            this.character?.currentHitpoints == this.character?.maxHitpoints
          ) {
            this.toastr.warning('Already at max health');
            skipUse = true;
          } else {
            this.toastr.success(`You have been healed ${value} hp!`);
            this.character!.currentHitpoints += Number(value);
            if (this.character!.currentHitpoints > this.character!.maxHitpoints)
              this.character!.currentHitpoints = this.character!.maxHitpoints;
          }
          break;
      }
    });

    if (!skipUse) {
      this.pcService.useItem(this.character!.id, item.id).subscribe({
        next: () => {
          item.currentStackSize -= 1;
          if (item.currentStackSize <= 0)
            this.character!.backPack![item.storageIndex] = undefined;
        },
        error: (error) => this.toastr.error(error),
      });
    }
  }
}
