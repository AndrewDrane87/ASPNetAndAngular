import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { initialState } from 'ngx-bootstrap/timepicker/reducer/timepicker.reducer';
import { Interaction } from 'src/app/_models/Adventure';

@Component({
  selector: 'app-information-modal',
  templateUrl: './interaction-modal.component.html',
  styleUrls: ['./interaction-modal.component.css']
})
export class InteractionModalComponent {
public interaction: Interaction | undefined;

  constructor(public bsModalRef: BsModalRef){
    
  }
  
  public setInteraction(i : Interaction){
    this.interaction = i;
  }

  ok(){
    this.bsModalRef.hide();
  }
}
