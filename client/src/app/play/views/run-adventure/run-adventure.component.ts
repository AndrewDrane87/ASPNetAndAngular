import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import {
  Adventure,
  AdventureLocation,
  Container,
} from 'src/app/_models/Adventure';
import { DialogueResponse, NPC } from 'src/app/_models/npc';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { NpcService } from 'src/app/_services/adventures/npc.service';

@Component({
  selector: 'app-run-adventure',
  templateUrl: './run-adventure.component.html',
  styleUrls: ['./run-adventure.component.css'],
})
export class RunAdventureComponent implements OnInit {
  adventure: Adventure | undefined;

  location: AdventureLocation | undefined;
  npc: NPC | undefined;
  container: Container | undefined;
  currentView = 'location';

  constructor(
    private modalRef: BsModalRef,
    private modalService: BsModalService,
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private locationService: LocationService,
    private npcService: NpcService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.adventure = data['adventure'];
        console.log(this.adventure);
        this.locationService
          .getPlayerLocation(this.adventure!.startingLocation!.id)
          .subscribe({ next: (result) => (this.location = result) });
      },
    });
  }

  locationSelected(event: AdventureLocation) {
    this.currentView = 'location'
    this.locationService.getPlayerLocation(event.id).subscribe({
      next: (result) => {
        this.location = result;
      },
      error: (error) => {
        this.toastr.error(error);
      },
    });
  }

  npcSelected(event: NPC) {
    this.currentView = 'npc'
    this.npcService.getNpcDetail(event.id).subscribe({
      next: result =>{
        this.npc = result;
      },
      error: error => this.toastr.error(error)
    });
  }

  responseSelected(response: DialogueResponse){
    this.npcService.getDialogueByResponseId(response.id).subscribe({
      next: result =>{ this.npc!.dialogue = result},
      error: error => {this.toastr.error(error)}
    })
  }

  backToLocation(){
    this.currentView = 'location';
  }

  containerSelected(event: Container) {
    this.currentView = 'container'
    this.locationService.getContainerById(event.id).subscribe({
      next:(result)=>{this.container = result},
      error: error => {this.toastr.error(error)}
    })
  }


}
