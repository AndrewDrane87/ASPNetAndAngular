import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef,  } from 'ngx-bootstrap/modal';
import { ItemPhoto } from '../_models/itemPhoto';
import { ItemService } from '../_services/items/item.service';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css'],
})
export class ImageSelectorComponent implements OnInit {
  @Input() itemType = '';
  itemPhotos: ItemPhoto[] | undefined;
  selectedPhoto: ItemPhoto | undefined;
  result = false;

  constructor(
    public bsModalRef: BsModalRef,
    private itemService: ItemService
  ) {}

  ngOnInit(): void {
    if (this.itemService.checkIfPhotosLoaded() === false) {
      this.itemService
        .loadItemPhotos()
        .subscribe({ next: (photos) => (this.itemPhotos = photos) });
    } else {
      this.itemPhotos = this.itemService.getItemPhotos('');
    }
  }

  photoSelected(photo: ItemPhoto) {
    this.selectedPhoto = photo;
    this.result = true;
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
