import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-damage-calculator',
  templateUrl: './damage-calculator.component.html',
  styleUrls: ['./damage-calculator.component.css'],
})
export class DamageCalculatorComponent {
  radioModel = 'd4';

  public minAttack = 5;
  public maxAttack = 20;
  public minHealth = 5;
  public maxHealth = 20;
  public minArmor = 2;
  public maxArmor = 20;

  damageData: DamageData[] = [];
  constructor(private formBuilder: UntypedFormBuilder) {}

  ngOnInit() {}

  submit() {
    console.log(this.minAttack);
    this.damageData = [];
    for (let baseAttack = this.minAttack; baseAttack <= this.maxAttack; baseAttack++) {
      for (let defenderHealth = this.minHealth; defenderHealth <= this.maxHealth; defenderHealth++) {
        for (let defenderArmor = this.minArmor; defenderArmor <= this.maxArmor; defenderArmor++) {
          let numRounds: number[] = [];
          for (let iterations = 0; iterations < 100; iterations++) {
            numRounds = [];
            let dh = defenderHealth;
            let currentRoundCount = 0;
            while (dh > 0 && currentRoundCount < 6) {
              currentRoundCount++;
              let a = this.attack(baseAttack, defenderArmor);
              dh = dh - a;
            }
            numRounds.push(currentRoundCount);
          }
          const average = numRounds.reduce((a, b) => a + b) / numRounds.length;
          if (average < 6) {
            const data: DamageData = {
              baseAttack: baseAttack,
              defenderHealth: defenderHealth,
              defenderArmor: defenderArmor,
              averageRounds: average,
            };
            this.damageData.push(data);
          }
        }
      }
    }
  }

  attack(baseAttack: number, armorValue: number) {
    let attackValue = baseAttack + this.diceRoll();
    attackValue -= armorValue;
    return attackValue > 0 ? attackValue : 0;
  }

  diceRoll() {
    switch (this.radioModel) {
      case 'd4':
        return Math.floor(Math.random() * 5);
      case 'd6':
        return Math.floor(Math.random() * 7);
      case 'd8':
        return Math.floor(Math.random() * 9);
      case 'd10':
        return Math.floor(Math.random() * 11);
      case 'd12':
        return Math.floor(Math.random() * 13);
      default:
        return 0;
    }
  }
}

interface DamageData {
  baseAttack: number;
  defenderHealth: number;
  defenderArmor: number;
  averageRounds: number;
}
