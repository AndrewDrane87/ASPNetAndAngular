import { Component, Input } from '@angular/core';
import { Rule } from '../rules';

@Component({
  selector: 'app-rule-display',
  templateUrl: './rule-display.component.html',
  styleUrls: ['./rule-display.component.css']
})
export class RuleDisplayComponent {
@Input() rule : Rule |undefined
}
