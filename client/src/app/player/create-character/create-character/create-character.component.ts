import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { setFullYear } from 'ngx-bootstrap/chronos/utils/date-setters';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';
import { PlayerCharactersService } from 'src/app/_services/player-characters.service';

@Component({
  selector: 'app-create-character',
  templateUrl: './create-character.component.html',
  styleUrls: ['./create-character.component.css'],
})
export class CreateCharacterComponent {
  @Output() cancelCreateMode = new EventEmitter();
  @Output() characterCreated = new EventEmitter();
  registerForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  constructor(
    private playerCharacterService: PlayerCharactersService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      photoUrl: [''],
    });
  }

  register() {
    const values = this.registerForm.value;
    this.playerCharacterService.createPlayerCharacter(values).subscribe({
      next: () => {
        this.characterCreated.emit(false);
      },
      error: (error) => {
        this.validationErrors = error;
        console.log(error);
      },
    });
  }

  cancel() {
    this.cancelCreateMode.emit(false);
  }
}
