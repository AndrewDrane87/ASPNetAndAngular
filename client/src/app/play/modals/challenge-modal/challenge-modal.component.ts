import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-challenge-modal',
  templateUrl: './challenge-modal.component.html',
  styleUrls: ['./challenge-modal.component.css'],
})
export class ChallengeModalComponent {
  challengeText = '';
  challengeSkill = '';
  challengeMinimum = '';
  showMinimum = '';
  public playerScore = 0;
  result = false;

  constructor(public bsModalRef: BsModalRef) {}

  setActionData(actionData: string) {
    var values = actionData.split('|');
    this.challengeText = values[0];
    this.challengeSkill = values[1];
    this.challengeMinimum = values[2];
    this.showMinimum = values[3];
  }

  submit() {
    if (this.playerScore > Number(this.challengeMinimum)) {
      this.result = true;
    }
    this.bsModalRef.hide();
  }
}
