import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PlayerCharacter } from 'src/app/_models/playerCharacters/playerCharacter';
import { ItemSelectorComponent } from '../item-selector/item-selector.component';
import { ItemService } from 'src/app/_services/items/item.service';
import { ToastrService } from 'ngx-toastr';

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

  constructor(
    private modalService: BsModalService,
    private itemService: ItemService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.calculateTotalArmor();
  }

  setCharacter(character: PlayerCharacter) {
    this.character = character;
    this.calculateTotalArmor();
  }

  incrementHealth() {
    this.currentHealth = this.currentHealth + 1;
    if (this.currentHealth > this.totalHealth)
      this.currentHealth = this.totalHealth;
  }

  decrementHealth() {
    this.currentHealth = this.currentHealth - 1;
    if (this.currentHealth < 0) this.currentHealth = 0;
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

  itemSelector(itemType: string) {
    this.modalRef = this.modalService.show(ItemSelectorComponent);
    this.modalRef.content.character = this.character;
    this.modalRef.content.setItemType(itemType);
    return this.modalRef.onHidden!.subscribe((result) => {
      console.log(result);
      if (this.modalRef?.content.result === true) {
        switch (itemType) {
          case 'helmets':
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
            this.character!.feet = this.modalRef.content.selectedItem;
            break;
        }

        this.itemService
          .setCharacterItem(
            itemType,
            this.modalRef.content.selectedItem.id,
            this.character!.id
          )
          .subscribe({
            next: () => this.toastr.success('Item selection saved'),
            //error: (error) => this.toastr.error(error),
          });

        this.calculateTotalArmor();
      }
    });
  }
}
