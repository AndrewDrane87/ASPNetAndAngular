import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { ItemPhoto } from 'src/app/_models/itemPhoto';

@Component({
  selector: 'app-item-photo-uploader',
  templateUrl: './item-photo-uploader.component.html',
  styleUrls: ['./item-photo-uploader.component.css'],
})
export class ItemPhotoUploaderComponent {
  @Input() member: Member | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;
  itemType= '';
  itemTypeSelected = false;

  imgUrl = '';
  @Output() newImage = new EventEmitter<ItemPhoto>();
  constructor(
    private accountService: AccountService,
    private memberService: MembersService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'items/add-item-photo/',
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      //Required for cors
      file.withCredentials = false;
      console.log('Adding file');
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      console.log(this.uploader?.options.url);
      if (response) {
        const photo = JSON.parse(response);
        this.imgUrl = photo.url;
        this.newImage.emit(photo);
      }
    };
  }

  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe({
      next: () => {
        if (this.member) {
          this.member.photos = this.member.photos.filter(
            (x) => x.id != photoId
          );
        }
      },
    });
  }

  setItemType(event: any) {
    this.itemTypeSelected = true;
    this.itemType = event;
    this.uploader!.setOptions({
      url: this.baseUrl + 'items/add-item-photo?itemType=' + this.itemType,
    });
    console.log(this.uploader!.options.url);
  }
}
