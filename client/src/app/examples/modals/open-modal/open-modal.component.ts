import { Component } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ExampleModalComponent } from '../example-modal/example-modal.component';

@Component({
  selector: 'app-open-modal',
  templateUrl: './open-modal.component.html',
  styleUrls: ['./open-modal.component.css']
})
export class OpenModalComponent {
  modalRef: BsModalRef | undefined;
  constructor(private modalService: BsModalService, toastr: ToastrService) {}

  open() {
    this.modalRef = this.modalService.show(ExampleModalComponent);
    return this.modalRef.onHidden!.subscribe((result) => {
      console.log(result);
      if (this.modalRef?.content.result === true) {
        console.log(result);
      }
    });
  }
}
