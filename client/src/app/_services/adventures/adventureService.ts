import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdminAdventure, AdminAdventureLocation } from 'src/app/_models/Adventure';
import { Adventure, AdventureLocation } from 'src/app/_models/AdventureSave';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdventureService {
  baseUrl = environment.apiUrl;
  adminAdventures: AdminAdventure[] = [];
  adminAdventure: AdminAdventure | undefined;
  playerAdventures: Adventure[] =[];
  playerAdventure: Adventure | undefined;

  constructor(private http: HttpClient) {}

  loadAdventures() {
    var url = this.baseUrl + 'adventures/get-available';
    return this.http.get<Adventure[]>(url);
  }

  loadAdminAdventures() {
    var url = this.baseUrl + 'adventures/get-available';
    return this.http.get<AdminAdventure[]>(url);
  }

  createAdventure(adventure: AdminAdventure) {
    var url = this.baseUrl + 'adventures/create-adventure';
    return this.http.post<AdminAdventure>(url, adventure);
  }

  getAdventureAdmin(adventureId: number) {
    var url = this.baseUrl + `admin/get-adventure?id=${adventureId}`;
    return new Promise<AdminAdventure>((resolve, reject) => {
      this.http.get<AdminAdventure>(url).subscribe({
        next: (result) => {
          if (result) {
            this.adminAdventure = result;
            resolve(result);
          }
        },
      });
    });
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

  deleteAdventure(adventure: AdminAdventure) {
    var url = this.baseUrl + 'adventures/delete?id=' + adventure.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.adminAdventures!.indexOf(adventure, 0);
          if (index > -1) {
            this.adminAdventures!.splice(index, 1);
          }
          resolve(true);
        },
        error: (error) => {
          console.log(error);
          resolve(false);
        },
      });
    });
  }

  createLocation(location: AdminAdventureLocation) {
    if (this.adminAdventure !== undefined) {
      var url =
        this.baseUrl +
        'adventures/create-location?adventureId=' +
        this.adminAdventure.id;

      return new Promise((resolve, reject) => {
        this.http.post<AdminAdventureLocation>(url, location).subscribe({
          next: (result: AdminAdventureLocation) => {
            // const indexA = this.adventures!.indexOf(this.adminAdventure!, 0);
            // if (this.adventures[indexA].locations === null)
            //   this.adventures[indexA].locations = [];
            // this.adventures[indexA].locations.push(result);
            
            this.adminAdventure?.locations.push(result);
            resolve(true);
          },
          error: (error) => {
            console.log(error);
            resolve(false);
          },
        });
      });
    }
    return new Promise((resolve, reject) => {
      resolve(false);
    });
  }
  
  createLocationLink(fromLocation: AdminAdventureLocation, toLocation: AdminAdventureLocation, linkMode: string){
    var url = this.baseUrl + `adventures/link-location?fromLocation=${fromLocation.id}&toLocation=${toLocation.id}&mode=${linkMode}`
    return this.http.post(url,
      {}
      );
  }

  deleteLocation(location: AdminAdventureLocation) {
    if (this.adminAdventure !== undefined) {
      var url =
        this.baseUrl +
        `adventures/delete-location?locationid=${location.id}&adventureId=${this.adminAdventure?.id}`;
      console.log(url);
      return new Promise((resolve, reject) => {
        this.http.delete(url).subscribe({
          next: result => {
            const indexB = this.adminAdventure!.locations.indexOf(location, 0);
            if (indexB > -1) {
              this.adminAdventure!.locations.splice(indexB, 1);
            }
            resolve(true);
          },
          error: (error) => {
            console.log(error);
            resolve(false);
          },
        });
      });
    }

    return new Promise((resolve, reject) => {
      resolve(false);
    });
  }

  createAdventureSave(){}

  reset(){
    return this.http.put(this.baseUrl + 'adventures/reset',{});
  }
}
