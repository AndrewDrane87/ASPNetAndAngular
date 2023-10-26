import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NameDescription } from 'src/app/_models/Generics/NameDescription';

@Component({
  selector: 'app-create-container',
  templateUrl: './create-container.component.html',
  styleUrls: ['./create-container.component.css']
})
export class CreateContainerComponent {
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
