import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AdventureLocation, Container } from 'src/app/_models/Adventure';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { LocationService } from 'src/app/_services/adventures/locationService';
import { CreateNameDescriptionComponent } from '../../modals/create-name-description/create-name-description.component';
import { NPC } from 'src/app/_models/npc';
import { CreateNpcComponent } from 'src/app/admin/modals/create-npc/create-npc.component';
import { CreateContainerComponent } from '../../modals/create-container/create-container.component';

@Component({
  selector: 'app-location-management',
  templateUrl: './location-management.component.html',
  styleUrls: ['./location-management.component.css'],
})
export class LocationManagementComponent implements OnInit {
  location: AdventureLocation | undefined;
  modalRef: BsModalRef | undefined;

  constructor(
    private locationService: LocationService,
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe({
      next: (data) => {
        this.location = data['location'];
        console.log(data);
      },
    });

    // this.locationService.getLocationDetail(7).subscribe({next: result =>{this.location = result;     console.log(this.location);}})
  }

  backToAdventure() {
    this.router.navigate([
      'admin/adventure/' + this.adventureService.adminAdventure!.id,
    ]);
  }

  // createLocation() {
  //   this.modalRef = this.modalService.show(CreateNameDescriptionComponent);
  //   this.modalRef.content.header = 'Create location';
  //   return this.modalRef.onHidden!.subscribe(() => {
  //     if (this.modalRef?.content.result === true) {
  //       var l = this.modalRef.content.value as AdventureLocation;
  //       this.adventureService.createLocation(l);
  //       console.log(l);
  //     }
  //   });
  // }

  deleteLocation(locationId: number) {
    this.toastr.success('Delete: ' + locationId);
  }

  editLocation(locationId: number) {
    this.router.navigate(['admin/location/' + locationId]);
  }

  createNpc() {
    this.modalRef = this.modalService.show(CreateNpcComponent);
    this.modalRef.content.header = 'Create NPC';
    return this.modalRef.onHidden!.subscribe(() => {
      if (this.modalRef?.content.result === true) {
        var npc = this.modalRef.content.value as NPC;
        this.locationService.createNPC(npc, this.location!.id).subscribe({
          next: (result) => {
            this.location?.npCs.push(result);
          },
        });
        console.log(npc);
      }
    });
  }

  deleteNpc(npc: NPC) {
    this.locationService.deleteNpc(npc).subscribe({
      next: () => {
        this.toastr.success('Deleted npc');
        this.router.navigate(['admin/location/' + this.location!.id]);
      },
    });
  }

  createContainer() {
    this.modalRef = this.modalService.show(CreateContainerComponent);
    this.modalRef.content.header = 'Create Container';
    return this.modalRef.onHidden!.subscribe(() => {
      if (this.modalRef?.content.result === true) {
        var container = this.modalRef.content.value as Container;
        this.locationService
          .createContainer(container, this.location!.id)
          .subscribe({
            next: (result) => {
              this.location?.containers.push(result);
            },
          });
        console.log(container);
      }
    });
  }

  deleteContainer(container: Container) {
    console.log(container);
    this.locationService.deleteContainer(container).subscribe({
      next: () => {
        this.toastr.success('Deleted container');
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Failed to delete container.');
      },
    });
  }

  addItemToContainer(container: Container) {}
}
