import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminAdventure } from 'src/app/_models/Adventure';
import { Adventure } from 'src/app/_models/AdventureSave';
import { AdventureService } from 'src/app/_services/adventures/adventureService';

@Component({
  selector: 'app-adventure-selection',
  templateUrl: './adventure-selection.component.html',
  styleUrls: ['./adventure-selection.component.css']
})
export class AdventureSelectionComponent implements OnInit{

  constructor(public adventureService: AdventureService,
    private router : Router){}

  ngOnInit(): void {
    if (this.adventureService.playerAdventures.length === 0) {
      this.adventureService.loadAdventures().subscribe({
        next: (values) => {
          console.log(values);
          this.adventureService.playerAdventures = values;
        },
      });
    }
  }

  selectAdventure(adventure: Adventure){
    this.router.navigate(['player/adventure/' + adventure.id]);
  }
}
