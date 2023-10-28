import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogueResponse, NPC } from 'src/app/_models/npc';

@Component({
  selector: 'app-npc-view',
  templateUrl: './npc-view.component.html',
  styleUrls: ['./npc-view.component.css']
})
export class NpcViewComponent {
@Input() npc : NPC | undefined
@Output() responseSelectedEvent = new EventEmitter<DialogueResponse>();
@Output() backToLocationEvent = new EventEmitter();

responseSelected(response: DialogueResponse){
  this.responseSelectedEvent.emit(response);
}

backToLocation(){
  this.backToLocationEvent.emit();
}

}
