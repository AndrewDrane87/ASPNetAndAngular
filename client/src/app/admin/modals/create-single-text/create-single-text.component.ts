import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-single-text',
  templateUrl: './create-single-text.component.html',
  styleUrls: ['./create-single-text.component.css']
})
export class CreateSingleTextComponent {
  @Input() header :string = '';
  form: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  value = '';
  result = false;

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.form = this.fb.group({
      text: ['', Validators.required],
    });
  }

  onSubmit() {
    this.value = this.form.value;
    this.result = true;
    this.bsModalRef.hide();
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
