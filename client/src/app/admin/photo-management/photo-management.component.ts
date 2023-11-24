import { Component, OnInit } from '@angular/core';
import { ItemPhoto } from 'src/app/_models/itemPhoto';
import { ItemService } from 'src/app/_services/items/item.service';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {
  itemPhotos: ItemPhoto[] | undefined;

  constructor(private itemService: ItemService){}
  
  ngOnInit(): void {
    if (this.itemService.checkIfPhotosLoaded() === false) {
      this.itemService
        .loadItemPhotos()
        .subscribe({ next: (photos) => (this.itemPhotos = photos) });
    } else {
      this.itemPhotos = this.itemService.getItemPhotos('');
    }
  }

  newImage(newPhoto: ItemPhoto) {
    this.itemPhotos?.push(newPhoto);
  }

  deletePhoto(photo: ItemPhoto) {
    this.itemService.deletePhoto(photo.id).subscribe({
      next: () =>
        this.itemPhotos!.forEach((item, index) => {
          if (item === photo) this.itemPhotos!.splice(index, 1);
        }),
    });
  }

  refreshImages() {}
}
