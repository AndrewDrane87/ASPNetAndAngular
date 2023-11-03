import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-information-modal',
  templateUrl: './information-modal.component.html',
  styleUrls: ['./information-modal.component.css'],
})
export class InformationModalComponent {
  information = '';

  constructor(public bsModalRef: BsModalRef) {}

  ok() {
    this.bsModalRef.hide();
  }
}
