import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { RulesServiceService as RulesService } from 'src/app/_services/rules-service.service';
import { RulesDisplayComponent } from 'src/app/rules/rules-display/rules-display.component';

@Component({
  selector: 'app-challenge-modal',
  templateUrl: './challenge-modal.component.html',
  styleUrls: ['./challenge-modal.component.css'],
})
export class ChallengeModalComponent {
  challengeText = '';
  challengeSkill = '';
  modifier = '';
  showMinimum = '';
  public playerScore = 0;
  result = false;
  toolTipContent = '';

  constructor(private rules: RulesService, public bsModalRef: BsModalRef) {
    this.toolTipContent = rules.challenge.text;
  }

  setActionData(actionData: string) {
    var values = actionData.split('|');
    this.challengeText = values[0];
    this.challengeSkill = values[1];
    this.modifier = values[2];
    this.showMinimum = values[3];
  }

  pass() {
    this.result = true;
    this.bsModalRef.hide();
  }
  fail(){
    this.result = false;
    this.bsModalRef.hide();
  }
}
