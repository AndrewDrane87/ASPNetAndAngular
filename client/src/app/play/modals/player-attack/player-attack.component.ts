import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Item } from 'src/app/_models/item';

@Component({
  selector: 'app-player-attack',
  templateUrl: './player-attack.component.html',
  styleUrls: ['./player-attack.component.css'],
})
export class PlayerAttackComponent {
  @Input() item: Item | undefined;
  diceValue = 1;
  damageValue = 0;
  modifiers: string[] = [];
  disableAttack = false;
  useEvent = new EventEmitter<string>();
  constructor(public bsModalRef: BsModalRef) {}

  setItem(item: Item) {
    this.item = item;
    this.updateDamage();
  }

  updateDamage() {
    this.item!.diceSides = 4;

    if (this.diceValue > this.item!.diceSides) {
      this.diceValue = this.item!.diceSides;
    }

    this.damageValue = this.diceValue + this.item!.attackValue;
  }

  use() {
    this.useEvent.emit();
    this.bsModalRef.hide();
  }
}
