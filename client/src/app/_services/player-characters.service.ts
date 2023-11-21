import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PlayerCharacter } from '../_models/playerCharacters/playerCharacter';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class PlayerCharactersService {
  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}

  getPlayerCharacters() {
    const headers = new Headers({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.accountService.currentUser$}`,
    });
    return this.http.get<PlayerCharacter[]>(this.baseUrl + 'playercharacters');
  }
  
  getPlayerCharacter(id: number) {
    const headers = new Headers({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${this.accountService.currentUser$}`,
    });
    return this.http.get<PlayerCharacter>(this.baseUrl + 'playercharacters/get-player-character/' + id);
  }

  createPlayerCharacter(values: any) {
    console.log('Values: ' + values);
    return this.http.post(this.baseUrl + 'playercharacters/create-player-character', values);
  }

  useItem(playerCharacterId: number, itemId: number){
    return this.http.put(this.baseUrl + `playercharacters/use-item?playerCharacterId=${playerCharacterId}&itemId=${itemId}`,{});
  }
}
