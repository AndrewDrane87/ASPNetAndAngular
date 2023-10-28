import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Adventure } from 'src/app/_models/Adventure';
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
    if (this.adventureService.adventures.length === 0) {
      this.adventureService.loadAdventures().subscribe({
        next: (values) => {
          console.log(values);
          this.adventureService.adventures = values;
        },
      });
    }
  }

  selectAdventure(adventure: Adventure){
    this.router.navigate(['adventure/' + adventure.id]);
  }
}
