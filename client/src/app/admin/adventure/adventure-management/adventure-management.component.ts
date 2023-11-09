import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AdminAdventure, AdminAdventureLocation } from 'src/app/_models/Adventure';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { CreateNameDescriptionComponent } from '../../modals/create-name-description/create-name-description.component';
import { CreateLocationLinkComponent } from '../../modals/create-location-link/create-location-link.component';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AdventureAdminService } from 'src/app/_services/adventure-admin.service';

@Component({
  selector: 'app-adventure-management',
  templateUrl: './adventure-management.component.html',
  styleUrls: ['./adventure-management.component.css'],
})
export class AdventureManagementComponent implements OnInit {
  adventure: AdminAdventure | undefined;
  constructor(
    private modalRef: BsModalRef,
    private modalService: BsModalService,
    private adventureService: AdventureAdminService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.adventure = data['adventure'];
        console.log(this.adventure);
      },
    });
  }

  deleteAdventure() {
    this.adventureService.deleteAdventure(this.adventure!).then((result) => {
      if (result) {
        this.toastr.success('Deleted adventure');
      } else {
        this.toastr.warning('Failed to delete adventure');
      }
    });
  }

  createLocation(adventure: AdminAdventure) {
    this.modalRef = this.modalService.show(CreateNameDescriptionComponent);
    this.modalRef.content.header = 'Create location';
    return this.modalRef.onHidden!.subscribe(() => {
      if (this.modalRef?.content.result === true) {
        var l = this.modalRef.content.value as AdminAdventureLocation;
        this.adventureService.createLocation(l);
        this.router.navigate(['admin/adventure/' + adventure.id]);
        console.log(l);
      }
    });
  }

  createLocationLink() {
    this.modalRef = this.modalService.show(CreateLocationLinkComponent);
    // this.modalRef.content.header = 'Create location';
    return this.modalRef.onHidden!.subscribe(() => {
      if (this.modalRef?.content.result === true) {
        // var l =this.modalRef.content.value as AdventureLocation;
        // this.adventureService.createLocation(l,adventure)
        // console.log(l);
      }
    });
  }

  editLocation(locationId: Number) {
    this.router.navigate(['admin/location/' + locationId]);
  }

  deleteLocation(location: AdminAdventureLocation, adventure: AdminAdventure) {
    this.adventureService.deleteLocation(location).then((result) => {
      if (result) {
        this.toastr.success('Deleted location');
        this.router.navigate(['admin/adventure/' + adventure.id]);
      } else {
        this.toastr.warning('Failed to delete location');
      }
    });
  }
}
