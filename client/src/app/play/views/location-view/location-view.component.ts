import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import {
  AdminAdventureLocation,
  AdminContainer,
  AdminInteraction,
} from 'src/app/_models/Adventure';
import { AdventureLocation, Container, Enemy, Interaction } from 'src/app/_models/AdventureSave';
import { NPC } from 'src/app/_models/npc';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { AvilableItemsComponent } from '../../modals/avilable-items/avilable-items.component';

@Component({
  selector: 'app-location-view',
  templateUrl: './location-view.component.html',
  styleUrls: ['./location-view.component.css'],
})
export class LocationViewComponent implements OnInit {
  @Input() location: AdventureLocation | undefined;
  @Output() locationSelectedEvent = new EventEmitter<AdventureLocation>();
  @Output() npcSelectedEvent = new EventEmitter<NPC>();
  @Output() containerSelectedEvent = new EventEmitter<Container>();
  @Output() interactionSelectedEvent = new EventEmitter<Interaction>();
  @Output() enemySelectedEvent = new EventEmitter<Enemy>();
  @Output() enemyAttackEvent = new EventEmitter();
isCollapsed = true;
  constructor(
    private bsModalRef: BsModalRef,
    private bsModalService: BsModalService,
    private locationService: LocationService
  ) {}
  ngOnInit(): void {}

  locationSelected(location: AdventureLocation) {
    this.locationSelectedEvent.emit(location);
  }

  npcSelected(npc: NPC) {
    this.npcSelectedEvent.emit(npc);
  }

  containerSelected(container: Container) {
    this.containerSelectedEvent.emit(container);
  }

  interactionSelected(interaction: Interaction) {
    this.interactionSelectedEvent.emit(interaction);
  }

  enemySelected(enemy: Enemy) {
    console.log('Location view: ' + enemy);
    this.enemySelectedEvent.emit(enemy);
  }

  enemyAttack() {
    this.enemyAttackEvent.emit();
  }

  getAvailableItems() {
    this.locationService.getAvailableItems(this.location!.id).subscribe({
      next: (items) => {
        this.bsModalRef = this.bsModalService.show(AvilableItemsComponent,{ class: 'modal-lg' });
        this.bsModalRef.content.items = items;
        this.bsModalRef.onHidden?.subscribe();
      },
    });
  }
}
