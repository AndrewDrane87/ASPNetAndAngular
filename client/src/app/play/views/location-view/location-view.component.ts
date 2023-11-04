import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AdminAdventureLocation, AdminContainer, AdminInteraction } from 'src/app/_models/Adventure';
import { AdventureLocation } from 'src/app/_models/AdventureSave';
import { NPC } from 'src/app/_models/npc';
import { LocationService } from 'src/app/_services/adventures/locationService';

@Component({
  selector: 'app-location-view',
  templateUrl: './location-view.component.html',
  styleUrls: ['./location-view.component.css'],
})
export class LocationViewComponent implements OnInit {
  @Input() location: AdventureLocation | undefined;
  @Output() locationSelectedEvent = new EventEmitter<AdventureLocation>();
  @Output() npcSelectedEvent = new EventEmitter<NPC>();
  @Output() containerSelectedEvent = new EventEmitter<AdminContainer>();
  @Output() interactionSelectedEvent = new EventEmitter<AdminInteraction>();
  constructor(private locationService: LocationService) {}
  ngOnInit(): void {}

  locationSelected(location: AdventureLocation) {
    this.locationSelectedEvent.emit(location);
  }

  npcSelected(npc: NPC) {
    this.npcSelectedEvent.emit(npc);
  }

  containerSelected(container: AdminContainer) {
    this.containerSelectedEvent.emit(container);
  }

  interactionSelected(interaction: AdminInteraction){
    this.interactionSelectedEvent.emit(interaction);
  }
}
