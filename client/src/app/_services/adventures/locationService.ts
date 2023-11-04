import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdminAdventure, AdminAdventureLocation, AdminContainer } from 'src/app/_models/Adventure';
import { Dialogue, NPC } from 'src/app/_models/npc';
import { environment } from 'src/environments/environment';
import { AdventureService } from './adventureService';
import { AdventureLocation } from 'src/app/_models/AdventureSave';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
baseUrl = environment.apiUrl;
adminLocation : AdminAdventureLocation |undefined;

  constructor(private http: HttpClient, private adventureService : AdventureService) { }

  getLocationDetail(locationId: number){
    var url = this.baseUrl + `adventures/get-location?id=${locationId}`;

    return new Promise<AdminAdventureLocation>((resolve, reject) => {
      this.http.get<AdminAdventureLocation>(url).subscribe({
        next: (result) => {
          if (result) {
            this.adminLocation = result;
            resolve(result);
          }
        },
      });
    });
  }

  createContainer(container: AdminContainer, locationId: number){
    var url = this.baseUrl + 'adventures/create-container';
    return this.http.post<AdminContainer>(url,{'locationId' : locationId,'name': container.name, 'description': container.description})
  }

  deleteContainer(container:AdminContainer){
    var url = this.baseUrl + `adventures/delete-container?containerId=${container.id}`
    return this.http.delete(url);
  }

  getPlayerLocation(id: number){
    var url = this.baseUrl + `adventures/get-player-location?locationId=${id}&adventureSaveId=${this.adventureService.playerAdventure!.id}`;
    return this.http.get<AdventureLocation>(url);
  }

  getContainerById(id: number){
    var url = this.baseUrl + `adventures/get-container?id=${id}`;
    return this.http.get<AdminContainer>(url);
  }

  
}
