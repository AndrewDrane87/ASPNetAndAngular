import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { NpcService } from 'src/app/_services/adventures/npc.service';
import { CreateSingleTextComponent } from '../../modals/create-single-text/create-single-text.component';
import { Dialogue, DialogueResponse } from 'src/app/_models/npc';

@Component({
  selector: 'app-dialogue-editor',
  templateUrl: './dialogue-editor.component.html',
  styleUrls: ['./dialogue-editor.component.css'],
})
export class DialogueEditorComponent {
  modalRef: BsModalRef | undefined;
  @Input() dialogue: Dialogue | undefined;
  @Output() addResponseRequest = new EventEmitter();
  @Output() deleteResponseRequest = new EventEmitter<DialogueResponse>();
  @Output() responseSelectedEvent = new EventEmitter<DialogueResponse>();
  @Output() backRequest = new EventEmitter();

  constructor(
    private locationService: LocationService,
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private npcService: NpcService
  ) {}

  addResponse() {
    this.addResponseRequest.emit();
  }

  deleteResponse(response: DialogueResponse) {
    this.deleteResponseRequest.emit(response);
  }

  responseSelected(response: DialogueResponse) {
    this.responseSelectedEvent.emit(response);
  }

  back() {
    this.backRequest.emit();
  }
}
