import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Adventure, AdventureLocation, Container } from 'src/app/_models/Adventure';
import { Dialogue, NPC } from 'src/app/_models/npc';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
baseUrl = environment.apiUrl;
adminLocation : AdventureLocation |undefined;

  constructor(private http: HttpClient) { }

  getLocationDetail(locationId: number){
    var url = this.baseUrl + `adventures/get-location?id=${locationId}`;

    return new Promise<AdventureLocation>((resolve, reject) => {
      this.http.get<AdventureLocation>(url).subscribe({
        next: (result) => {
          if (result) {
            this.adminLocation = result;
            resolve(result);
          }
        },
      });
    });
  }

  createContainer(container: Container, locationId: number){
    var url = this.baseUrl + 'adventures/create-container';
    return this.http.post<Container>(url,{'locationId' : locationId,'name': container.name, 'description': container.description})
  }

  deleteContainer(container:Container){
    var url = this.baseUrl + `adventures/delete-container?containerId=${container.id}`
    return this.http.delete(url);
  }

  getPlayerLocation(id: number){
    var url = this.baseUrl + `adventures/get-player-location?id=${id}`;
    return this.http.get<AdventureLocation>(url);
  }

  getContainerById(id: number){
    var url = this.baseUrl + `adventures/get-container?id=${id}`;
    return this.http.get<Container>(url);
  }

  
}
