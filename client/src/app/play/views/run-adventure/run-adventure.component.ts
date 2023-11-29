import { Component, EventEmitter, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import {
  AdminAdventure,
  AdminAdventureLocation,
  AdminContainer,
  AdminInteraction,
} from 'src/app/_models/Adventure';
import { DialogueResponse, NPC } from 'src/app/_models/npc';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { NpcService } from 'src/app/_services/adventures/npc.service';
import { InteractionModalComponent } from '../../modals/interaction-modal/interaction-modal.component';
import { ActionTrigger } from 'src/app/_models/actionTrigger';
import { InformationModalComponent } from '../../modals/information-modal/information-modal.component';
import { Observable, config, take } from 'rxjs';
import { ChallengeModalComponent } from '../../modals/challenge-modal/challenge-modal.component';
import { TriggerService } from 'src/app/_services/trigger.service';
import {
  Adventure,
  AdventureLocation,
  Container,
  Enemy,
  Interaction,
} from 'src/app/_models/AdventureSave';
import { MonsterCombatComponent } from '../../modals/monster-combat/monster-combat.component';
import { EnemyAttackModalComponent } from '../../modals/enemy-attack-modal/enemy-attack-modal.component';
import { AvilableItemsComponent } from '../../modals/avilable-items/avilable-items.component';

@Component({
  selector: 'app-run-adventure',
  templateUrl: './run-adventure.component.html',
  styleUrls: ['./run-adventure.component.css'],
})
export class RunAdventureComponent implements OnInit {
  adventure: Adventure | undefined;

  location: AdventureLocation | undefined;
  newLocation: AdventureLocation | undefined;
  npc: NPC | undefined;
  container: Container | undefined;
  currentView = 'location';
  public bsModalRef: BsModalRef | undefined;

  modalConfig = {
    backdrop: false,
    ignoreBackdropClick: true,
  };

  constructor(
    private modalRef: BsModalRef,
    private modalService: BsModalService,
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private locationService: LocationService,
    private npcService: NpcService,
    private bsModalService: BsModalService,
    private triggerService: TriggerService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.adventure = data['adventure'];
        console.log(this.adventure);
        if (this.adventure!.currentLocation.locationId === 0) {
          this.toastr.error('CurrentLocationId == 0');
          //return;
        }
        this.locationService
          .getPlayerLocation(this.adventure!.currentLocation.locationId)
          .subscribe({
            next: (result) => {
              this.location = result;
              this.triggerService.checkTriggers(
                this.location.triggers,
                'enter'
              );
              console.log(this.location);
            },
            error: (error) => {
              this.toastr.error(error);
            },
          });
      },
    });
  }

  locationSelected(event: AdventureLocation) {
    this.currentView = 'location';
    this.newLocation = event;

    //Subscribe to the complete so we know we have finished the exit triggers
    if (this.location?.triggers) {
      this.triggerService.complete.pipe(take(1)).subscribe({
        next: () => {
          this.loadLocation(this.newLocation!.id);
        },
      });
      this.triggerService.checkTriggers(this.location?.triggers, 'exit');
    }
  }

  loadLocation(id: number) {
    this.locationService.getPlayerLocation(id).subscribe({
      next: (result) => {
        this.location = result;
        console.log(this.location);
        this.triggerService.checkTriggers(this.location.triggers, 'enter');
      },
      error: (error) => {
        this.toastr.error(error);
      },
    });
  }

  npcSelected(event: NPC) {
    this.currentView = 'npc';
    this.npcService.getNpcDetail(event.id).subscribe({
      next: (result) => {
        this.npc = result;
      },
      error: (error) => this.toastr.error(error),
    });
  }

  responseSelected(response: DialogueResponse) {
    this.npcService.getDialogueByResponseId(response.id).subscribe({
      next: (result) => {
        this.npc!.dialogue = result;
      },
      error: (error) => {
        this.toastr.error(error);
      },
    });
  }

  backToLocation() {
    this.currentView = 'location';
  }

  backtoMainDialogue(event: NPC) {
    this.currentView = 'npc';
    this.npcService.getNpcDetail(event.id).subscribe({
      next: (result) => {
        this.npc = result;
      },
      error: (error) => this.toastr.error(error),
    });
  }

  containerSelected(event: Container) {
    this.currentView = 'container';
    this.locationService.getPlayerContainer(event.id).subscribe({
      next: (result) => {
        console.log(`Selected Container: ${result}` )
        console.log(result.triggers)
        this.toastr.warning('Finish triggers');
        //Subscribe to the complete so we know we have finished the exit triggers
        if (result?.triggers) {
          this.triggerService.checkTriggers(
            result.triggers,
            'enter'
          );
          this.triggerService.complete.pipe(take(1)).subscribe({
            next: () => {
              this.container = result;
            },
          });
        }
        else{
          this.container = result;
        }
      },
      error: (error) => {
        this.toastr.error(error);
      },
    });
  }

  interactionSelected(event: Interaction) {
    //Subscribe to the complete so we know we have finished the exit triggers
    if (event.triggers.length > 0) {
      this.triggerService.checkTriggers(event.triggers, 'enter');
      this.triggerService.complete.pipe(take(1)).subscribe({
        next: () => {
          //Clear the events array now that we have processed them. This way we dont have to refresh the whole page or reload data.
          event.triggers = [];
          const result = this.triggerService.result;
          event.passed = result;

          this.displayInteraction(event);
        },
      });
    } else {
      this.displayInteraction(event);
    }
  }

  displayInteraction(event: Interaction) {
    this.modalRef = this.modalService.show(InteractionModalComponent);
    this.modalRef.content.setInteraction(event);
    return this.modalRef.onHidden!.subscribe((result) => {
      event.complete = true;
      this.adventureService.updateInteractionSave(event).subscribe({
        next: () => {
          console.log('Saved interaction: ' + event.id);
        },
        error: (error) => this.toastr.error(error),
      });
    });
  }

  enemySelected(event: Enemy) {
    console.log(event);
    this.bsModalRef = this.bsModalService.show(MonsterCombatComponent);
    this.bsModalRef.content.enemy = event;
    this.bsModalRef.onHidden?.subscribe({
      next: () => {
        if (this.bsModalRef?.content.result) {
          this.loadLocation(this.location!.locationId);
        }
      },
    });
  }

  enemyIndex = 0;
  enemyAttack() {
    if (this.location?.enemies) {
      if (this.location?.enemies.length > this.enemyIndex) {
        let modalConfig = {
          backdrop: false,
          ignoreBackdropClick: true,
        };
        this.bsModalRef = this.bsModalService.show(
          EnemyAttackModalComponent,
          modalConfig
        );
        this.bsModalRef!.content.enemy =
          this.location?.enemies[this.enemyIndex];
        this.bsModalRef.onHidden?.subscribe({
          next: () => {
            this.enemyIndex++;
            this.enemyAttack();
          },
        });
      } else {
        this.enemyIndex = 0;
      }
    }
  }
}
