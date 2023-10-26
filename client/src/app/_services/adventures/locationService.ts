import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdventureLocation, Container } from 'src/app/_models/Adventure';
import { NPC } from 'src/app/_models/npc';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getLocationDetail(locationId: number){
    return this.http.get<AdventureLocation>(this.baseUrl + 'adventures/get-location?id=' + locationId);
  }

  createNPC(npc:NPC, locationId: number){
    var url = this.baseUrl + 'npc/create';
    return this.http.post<NPC>(url,{'locationId' : locationId,'name': npc.name, 'caption': npc.caption})
  }

  deleteNpc(npc: NPC){
    var url = this.baseUrl + `npc/delete?id=${npc.id}`;
    return this.http.delete(url);
  }

  createContainer(container: Container, locationId: number){
    var url = this.baseUrl + 'adventures/create-container';
    return this.http.post<Container>(url,{'locationId' : locationId,'name': container.name, 'description': container.description})
  }

  deleteContainer(container:Container){
    var url = this.baseUrl + `adventures/delete-container?containerId=${container.id}`
    return this.http.delete(url);
  }
}
