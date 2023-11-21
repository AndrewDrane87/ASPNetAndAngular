import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { setFullYear } from 'ngx-bootstrap/chronos/utils/date-setters';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { PlayerCharactersService } from 'src/app/_services/player-characters.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ImageSelectorComponent } from 'src/app/image-selector/image-selector.component';
import { map } from 'rxjs';
import { ItemPhoto } from 'src/app/_models/itemPhoto';
import { ItemService } from 'src/app/_services/items/item.service';

@Component({
  selector: 'app-create-item-form',
  templateUrl: './create-item-form.component.html',
  styleUrls: ['./create-item-form.component.css'],
})
export class CreateItemFormComponent {
  itemType = '';
  itemTypeSelected = false;
  createItemForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  attackValueDisabled = true;
  armorValueDisabled = true;
  modalRef: BsModalRef | undefined;

  itemPhoto: ItemPhoto | undefined;

  constructor(
    private playerCharacterService: PlayerCharactersService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router,
    private modalService: BsModalService,
    private itemService: ItemService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.createItemForm = this.fb.group({
      name: ['', Validators.required],
      attackValue: [0],
      armorValue: [0],
      photoId: [-1],
    });
  }

  onSubmit() {
    switch (this.itemType) {
      case 'Helmet':
        this.createHelmet();
        break;
      case 'Sword':
        this.createHandItem();
        break;
      case 'Shield':
        this.createHandItem();
        break;
        case 'Armor':
        this.createArmor();
        break;
        case 'Boots':
        this.createBoots();
        break;
    }
  }

  createHelmet() {
    this.itemService.createHelmet(this.createItemForm.value).subscribe({
      next: (value) => {
        if (value) {
          this.toastr.success('Item Created Successfully');
          this.itemService.helmets?.push(value);
          this.resetForm();
        }
      },
      error: (error) => {
        this.toastr.error('Failed to create new item');
        console.log(error);
      },
    });
  }

  createHandItem() {
    this.itemService.createHandItem(this.createItemForm.value).subscribe({
      next: (value) => {
        if (value) {
          this.toastr.success('Item Created Successfully');
          this.itemService.items?.push(value);
          this.resetForm();
        }
      },
      error: (error) => {
        this.toastr.error('Failed to create new item');
        console.log(error);
      },
    });
  }

  createArmor() {
    this.itemService.createArmor(this.createItemForm.value).subscribe({
      next: (value) => {
        if (value) {
          this.toastr.success('Item Created Successfully');
          this.itemService.armor?.push(value);
          this.resetForm();
        }
      },
      error: (error) => {
        this.toastr.error('Failed to create new item');
        console.log(error);
      },
    });
  }

  createBoots() {
    this.itemService.createBoots(this.createItemForm.value).subscribe({
      next: (value) => {
        if (value) {
          this.toastr.success('Item Created Successfully');
          this.itemService.boots?.push(value);
          this.resetForm();
        }
      },
      error: (error) => {
        this.toastr.error('Failed to create new item');
        console.log(error);
      },
    });
  }

  resetForm(){
    this.createItemForm.reset();
    this.itemPhoto = undefined;
  }

  cancel() {
    this.createItemForm.reset();
    //this.cancelCreateMode.emit(false);
  }

  setItemType(event: string) {
    this.itemType = event;
    this.itemTypeSelected = true;
    switch (this.itemType) {
      case 'Helmet':
        this.disableAttackValue();
        break;
      case 'Sword':
        this.disableArmorValue();
        break;
      case 'Shield':
        this.disableAttackValue();
        break;
        case 'Armor':
        this.disableAttackValue();
        break;
        case 'Boots':
        this.disableAttackValue();
        break;
    }
  }

  disableAttackValue() {
    this.armorValueDisabled = false;
    this.attackValueDisabled = true;
    this.disableItemValidation('attackValue');
    this.enableItemValidation('armorValue');
  }

  disableArmorValue() {
    this.armorValueDisabled = true;
    this.attackValueDisabled = false;
    this.disableItemValidation('armorValue');
    this.enableItemValidation('attackValue');
  }

  disableItemValidation(control: string) {
    this.createItemForm.get(control)!.setValidators(null);
    this.createItemForm.get(control)!.updateValueAndValidity();
  }

  enableItemValidation(control: string) {
    this.createItemForm.get(control)!.setValidators([Validators.required]);
    this.createItemForm.get(control)!.updateValueAndValidity();
  }

  openImageSelector() {
    this.modalRef = this.modalService.show(ImageSelectorComponent);
    return this.modalRef.onHidden!.subscribe((result) => {
      console.log(result);
      if (this.modalRef?.content.result === true) {
        this.createItemForm.controls['photoId'].setValue(
          this.modalRef?.content?.selectedPhoto.id
        );
        this.itemPhoto = this.modalRef?.content?.selectedPhoto;
      }
    });
  }
}
