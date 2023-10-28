import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AdventureLocation } from 'src/app/_models/Adventure';
import { AdventureService } from 'src/app/_services/adventures/adventureService';

@Component({
  selector: 'app-create-location-link',
  templateUrl: './create-location-link.component.html',
  styleUrls: ['./create-location-link.component.css'],
})
export class CreateLocationLinkComponent implements OnInit {
  locations: AdventureLocation[] = [];
  fromLocation: AdventureLocation | undefined;
  toLocation: AdventureLocation | undefined;
  linkMode = 'Two-Way';

  constructor(
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private bsModalRef: BsModalRef
  ) {}
  ngOnInit(): void {
    this.locations = this.adventureService.adminAdventure!.locations;
  }

  setFromLocation(l: AdventureLocation) {
    this.fromLocation = l;
  }

  setToLocation(l: AdventureLocation) {
    this.toLocation = l;
  }

  submit() {
    console.log(
      `submit: ${this.linkMode}; from: ${this.fromLocation!.name}; to: ${
        this.toLocation!.name
      }`
    );
    this.adventureService
      .createLocationLink(this.fromLocation!, this.toLocation!, this.linkMode)
      .subscribe({
        next: () => {
            this.toastr.success('Created location link');
            this.bsModalRef.hide();
        },
        error: (error) => this.toastr.error("failed to create link")
      });
  }
}