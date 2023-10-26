import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NPC } from 'src/app/_models/npc';

@Component({
  selector: 'app-create-npc',
  templateUrl: './create-npc.component.html',
  styleUrls: ['./create-npc.component.css']
})
export class CreateNpcComponent implements OnInit{
  @Input() header :string = '';
  form: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  value:NPC |undefined;
  result = false;

  constructor(public bsModalRef: BsModalRef, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      caption: ['', Validators.required],
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
