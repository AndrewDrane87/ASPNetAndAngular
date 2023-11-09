import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Enemy } from 'src/app/_models/AdventureSave';
import { AdventureService } from 'src/app/_services/adventures/adventureService';

@Component({
  selector: 'app-monster-combat',
  templateUrl: './monster-combat.component.html',
  styleUrls: ['./monster-combat.component.css'],
})
export class MonsterCombatComponent {
  damage = 0;
  enemy: Enemy | undefined;
  result = false;
  damageAfterArmor = 0;

  constructor(
    public bsModalRef: BsModalRef,
    private toastr: ToastrService,
    private adventureService: AdventureService
  ) {}

  increment(value: number) {
    this.damage += value;
    if(this.damage < 0){
      this.damage =0;
    }
    if(this.damage - this.enemy!.armorValue > 0){
      this.damageAfterArmor = this.damage - this.enemy!.armorValue;
    }
    else{
      this.damageAfterArmor = 0;
    }
  }

  dealDamage() {
    this.adventureService.dealDamage(this.damage, this.enemy!.id).subscribe({
      next: (result) => {
        this.toastr.success('Bam!');
        this.result = true;
        this.bsModalRef.hide();
      },
      error: (error) => this.toastr.error(error),
    });
  }
}
