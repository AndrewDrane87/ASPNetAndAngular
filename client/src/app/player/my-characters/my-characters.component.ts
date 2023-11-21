import { Component, OnInit } from '@angular/core';
import { PlayerCharacter } from 'src/app/_models/playerCharacters/playerCharacter';
import { PlayerCharactersService } from 'src/app/_services/player-characters.service';

@Component({
  selector: 'app-my-characters',
  templateUrl: './my-characters.component.html',
  styleUrls: ['./my-characters.component.css'],
})
export class MyCharactersComponent implements OnInit {
  myCharacters: PlayerCharacter[] | undefined;
  createMode = false;
  selectedCharacter: PlayerCharacter | undefined;
  displayPlayerCharacter = false;

  constructor(private playerCharacterService: PlayerCharactersService) {}
  ngOnInit(): void {
    this.loadPlayerCharacters();
  }

  loadPlayerCharacters() {
    this.playerCharacterService.getPlayerCharacters().subscribe({
      next: (characters) => {
        this.myCharacters = characters;
        this.playerSelected(this.myCharacters[0].id);
      },
    });
  }

  playerSelected(id: number) {
    this.playerCharacterService.getPlayerCharacter(id).subscribe({
      next: (character) => {
        if (character) {
          this.selectedCharacter = character;
          for (let i = 0; i < 10; i++) {
            if (this.selectedCharacter.backPack === undefined) {
              this.selectedCharacter.backPack = [];
            }
            if (i >= this.selectedCharacter.backPack?.length)
              this.selectedCharacter.backPack?.push(undefined);
          }

          this.displayPlayerCharacter = true;
          console.log(character);
        }
      },
    });
  }

  createCharacter() {
    console.log('create new pressed');
    this.createMode = !this.createMode;
  }

  cancelCreateMode(event: boolean) {
    this.createMode = event;
  }

  characterCreated(event: boolean) {
    this.createMode = event;
    this.loadPlayerCharacters();
  }

  backToCharacterSelection() {
    this.displayPlayerCharacter = false;
  }
}
