import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Adventure, AdventureLocation } from 'src/app/_models/Adventure';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdventureService {
  baseUrl = environment.apiUrl;
  adventures: Adventure[] = [];
  adminAdventure: Adventure | undefined;
  constructor(private http: HttpClient) {}

  loadAdventures() {
    var url = this.baseUrl + 'adventures/get-available';
    return this.http.get<Adventure[]>(url);
  }

  createAdventure(adventure: Adventure) {
    var url = this.baseUrl + 'adventures/create-adventure';
    return this.http.post<Adventure>(url, adventure);
  }

  getAdventureAdmin(adventureId: number) {
    var url = this.baseUrl + `admin/get-adventure?id=${adventureId}`;
    return new Promise<Adventure>((resolve, reject) => {
      this.http.get<Adventure>(url).subscribe({
        next: (result) => {
          if (result) {
            this.adminAdventure = result;
            resolve(result);
          }
        },
      });
    });
  }

  deleteAdventure(adventure: Adventure) {
    var url = this.baseUrl + 'adventures/delete?id=' + adventure.id;

    return new Promise((resolve, reject) => {
      this.http.delete(url).subscribe({
        next: (result) => {
          console.log(result);
          const index = this.adventures!.indexOf(adventure, 0);
          if (index > -1) {
            this.adventures!.splice(index, 1);
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

  createLocation(location: AdventureLocation) {
    if (this.adminAdventure !== undefined) {
      var url =
        this.baseUrl +
        'adventures/create-location?adventureId=' +
        this.adminAdventure.id;

      return new Promise((resolve, reject) => {
        this.http.post<AdventureLocation>(url, location).subscribe({
          next: (result: AdventureLocation) => {
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
  createLocationLink(fromLocation: AdventureLocation, toLocation: AdventureLocation, linkMode: string){
    var url = this.baseUrl + `adventures/link-location?fromLocation=${fromLocation.id}&toLocation=${toLocation.id}&mode=${linkMode}`
    return this.http.post(url,
      {}
      );
  }

  deleteLocation(location: AdventureLocation) {
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
}
