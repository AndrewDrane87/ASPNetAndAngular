import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AdminAdventure, AdminAdventureLocation } from 'src/app/_models/Adventure';
import { AdventureService } from 'src/app/_services/adventures/adventureService';
import { CreateNameDescriptionComponent } from '../../modals/create-name-description/create-name-description.component';
import { NameDescription } from 'src/app/_models/Generics/NameDescription';
import { CreateLocationLinkComponent } from '../../modals/create-location-link/create-location-link.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adventure-admin',
  templateUrl: './adventure-admin.component.html',
  styleUrls: ['./adventure-admin.component.css'],
})
export class AdventureAdminComponent implements OnInit {
  modalRef: BsModalRef | undefined;

  constructor(
    public adventureService: AdventureService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    console.log('Length: ' + this.adventureService.adminAdventures.length);
    if (this.adventureService.adminAdventures.length === 0) {
      this.adventureService.loadAdminAdventures().subscribe({
        next: (values) => {
          console.log(values);
          this.adventureService.adminAdventures = values;
        },
      });
    }
  }

  editAdventure(adventure: AdminAdventure) {
    this.adventureService.adminAdventure = adventure;
    this.router.navigate(['admin/adventure/' + adventure.id]);
  }
}
