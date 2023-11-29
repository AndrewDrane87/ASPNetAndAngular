import { Component, Input } from '@angular/core';
import { Rule } from '../rules';
import { RulesServiceService } from 'src/app/_services/rules-service.service';

@Component({
  selector: 'app-rules-display',
  templateUrl: './rules-display.component.html',
  styleUrls: ['./rules-display.component.css'],
})
export class RulesDisplayComponent {
  constructor(public rules : RulesServiceService){}
}
