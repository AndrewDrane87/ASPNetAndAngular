import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Enemy } from 'src/app/_models/AdventureSave';

@Component({
  selector: 'app-enemy-attack-modal',
  templateUrl: './enemy-attack-modal.component.html',
  styleUrls: ['./enemy-attack-modal.component.css'],
})
export class EnemyAttackModalComponent {
  public enemy: Enemy | undefined;
  constructor(public bsModalRef: BsModalRef) {}
  canComplete = false;
  totalDamage = -1;
  diceSides = 4;
  calculateDamage(baseDamage: number) {
    if (this.canComplete) return;
    this.totalDamage =
      baseDamage + Math.floor(Math.random() * this.diceSides) + 1;
    this.canComplete = true;
  }

  complete() {
    this.bsModalRef.hide();
  }
}
