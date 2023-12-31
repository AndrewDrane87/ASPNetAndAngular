import { Injectable } from '@angular/core';
import { AdminAdventure, AdminAdventureLocation } from '../_models/Adventure';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdventureAdminService {
  baseUrl = environment.apiUrl;
  adminAdventures: AdminAdventure[] = [];
  adminAdventure: AdminAdventure | undefined;

  constructor(private http: HttpClient) {}

  loadAdminAdventures() {
    var url = this.baseUrl + 'admin/get-available';
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

  createLocationLink(
    fromLocation: AdminAdventureLocation,
    toLocation: AdminAdventureLocation,
    linkMode: string
  ) {
    var url =
      this.baseUrl +
      `adventures/link-location?fromLocation=${fromLocation.id}&toLocation=${toLocation.id}&mode=${linkMode}`;
    return this.http.post(url, {});
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
      console.log('Create Location')
      var url =
        this.baseUrl +
        'adventures/create-location?adventureId=' +
        this.adminAdventure.id;

      return new Promise((resolve, reject) => {
        this.http.post<AdminAdventureLocation>(url, location).subscribe({
          next: (result: AdminAdventureLocation) => {
            console.log(`Adventure admin service, new location: ` + result);
            
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
}
