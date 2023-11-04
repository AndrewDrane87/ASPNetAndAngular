import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AdventureService } from 'src/app/_services/adventures/adventureService';

@Component({
  selector: 'app-create-adventure-form',
  templateUrl: './create-adventure-form.component.html',
  styleUrls: ['./create-adventure-form.component.css'],
})
export class CreateAdventureFormComponent implements OnInit {
  createAdventureForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  constructor(
    private adventureService: AdventureService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.createAdventureForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit() {
    this.adventureService
      .createAdventure(this.createAdventureForm.value)
      .subscribe({
        next: (value) => {
          this.toastr.success('Adventure Created successfully');
          this.adventureService.adminAdventures?.push(value);
          this.resetForm();
        },
        error: (error) => {
          this.toastr.error('Failed to create new adventure');
          console.log(error);
        },
      });
  }

  cancel() {
    this.createAdventureForm.reset();
    //this.cancelCreateMode.emit(false);
  }

  resetForm() {
    this.createAdventureForm.reset();
  }
}
