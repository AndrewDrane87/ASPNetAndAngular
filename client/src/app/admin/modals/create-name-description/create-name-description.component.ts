import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NameDescription } from 'src/app/_models/Generics/NameDescription';

@Component({
  selector: 'app-create-name-description',
  templateUrl: './create-name-description.component.html',
  styleUrls: ['./create-name-description.component.css'],
})
export class CreateNameDescriptionComponent implements OnInit {
  @Input() header :string = '';
  form: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  value:NameDescription |undefined;
  result = false;

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit() {
    this.value = this.form.value;
    console.log(this.form.value);
    this.result = true;
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
