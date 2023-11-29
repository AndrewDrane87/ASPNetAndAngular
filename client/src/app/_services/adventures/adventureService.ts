import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AdminAdventure,
  AdminAdventureLocation,
} from 'src/app/_models/Adventure';
import { Adventure, AdventureLocation, Interaction } from 'src/app/_models/AdventureSave';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdventureService {
  baseUrl = environment.apiUrl;
  adminAdventures: AdminAdventure[] = [];
  adminAdventure: AdminAdventure | undefined;
  playerAdventures: Adventure[] = [];
  playerAdventure: Adventure | undefined;

  constructor(private http: HttpClient) {}

  loadAdventures() {
    var url = this.baseUrl + 'adventures/get-available';
    return this.http.get<Adventure[]>(url);
  }

  getAdventurePlayer(adventureId: number) {
    var url = this.baseUrl + `adventures/get-adventure-save?id=${adventureId}`;
    return new Promise<Adventure>((resolve, reject) => {
      this.http.get<Adventure>(url).subscribe({
        next: (result) => {
          if (result) {
            this.playerAdventure = result;
            resolve(result);
          }
        },
      });
    });
  }

  createAdventureSave() {}

  dealDamage(damageAmount: number, enemyId: number) {
    var url = this.baseUrl + 'adventures/deal-damage';
    return this.http.put(url, { 'damageAmount': damageAmount, 'enemyId': enemyId });
  }

updateInteractionSave(interaction: Interaction){
  var url = this.baseUrl + 'adventures/update-interaction-save'
  return this.http.put(url, interaction);
}

  reset(adventureSaveId: number) {
    return this.http.put(this.baseUrl + `adventures/reset?adventureSaveId=${adventureSaveId}`, {});
  }
}
