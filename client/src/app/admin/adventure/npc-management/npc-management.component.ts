import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Dialogue, DialogueResponse, NPC } from 'src/app/_models/npc';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { CreateSingleTextComponent } from '../../modals/create-single-text/create-single-text.component';
import { NpcService } from 'src/app/_services/adventures/npc.service';

@Component({
  selector: 'app-npc-management',
  templateUrl: './npc-management.component.html',
  styleUrls: ['./npc-management.component.css'],
})
export class NpcManagementComponent implements OnInit {
  npc: NPC | undefined;
  modalRef: BsModalRef | undefined;
  dialogue: Dialogue | undefined;
  constructor(
    private locationService: LocationService,
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private npcService: NpcService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.npc = data['npc'];
        this.dialogue = this.npc?.dialogue;
        console.log(data);
      },
    });
  }

  backToLocation() {
    this.router.navigate([
      `admin/location/${this.locationService.adminLocation?.id}`,
    ]);
  }

  addResponse() {
    this.modalRef = this.modalService.show(CreateSingleTextComponent);
    return this.modalRef.onHidden!.subscribe((result) => {
      //console.log(result);
      if (this.modalRef?.content.result === true) {
        this.npcService
          .createResponse(this.modalRef?.content.value, this.dialogue!.id)
          .subscribe({ next: (returnValue) => (this.dialogue = returnValue) });
      }
    });
  }

  deleteResponse(response: DialogueResponse) {
    this.npcService.deleteResponse(response.id).subscribe({
      next: (returnValue) => {
        this.toastr.success('Deleted response');
        console.log(returnValue);
        this.dialogue = returnValue;
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Failed to delete response');
      },
    });
  }

  responseSelected(response: DialogueResponse) {
    this.npcService.getDialogueByResponseId(response.id).subscribe({
      next: (returnValue) => {
        if (returnValue) {
          this.dialogue = returnValue;
        }
        else{
          this.addDialogue(response.id);
        }
      },
      error: (error) => {
        
      },
    });
  }

  addDialogue(responseId: number) {
    this.modalRef = this.modalService.show(CreateSingleTextComponent);
    return this.modalRef.onHidden!.subscribe((result) => {
      //console.log(result);
      if (this.modalRef?.content.result === true) {
        this.npcService
          .createChildDialogue(this.modalRef?.content.value, responseId)
          .subscribe({ next: (returnValue) => (this.dialogue = returnValue) });
      }
    });
  }

  backToPreviousDialogue(){
    this.npcService.getPreviousDialogue(this.dialogue!.id).subscribe({
      next: (returnValue) => {
        if (returnValue) {
          this.dialogue = returnValue;
        }
      },
      error: (error) => {
        
      },
    });
  }
}
